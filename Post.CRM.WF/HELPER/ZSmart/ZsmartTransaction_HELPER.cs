using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using Post.CRM.WF.MODEL.ZSMART;
using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.Text;

namespace Post.CRM.WF.HELPER.ZSMART
{
    public class ZsmartTransaction_HELPER
    {
        /// <summary>
        /// Organisation service used to communicate with D365
        /// </summary>
        private IOrganizationService _service;
        /// <summary>
        /// Tracing service used to store log on D365
        /// </summary>
        private ITracingService _tracingService;
        /// <summary>
        /// Converter Service used to convert a zsmart to an entity crm
        /// </summary>
        private ZsmartTransaction_CONVERTOR _converter;
        /// <summary>
        /// Entity Transaction
        /// </summary>
        private Entity _e_zSmartTransaction;
        /// <summary>
        /// Current ZSmart Transaction Reference
        /// </summary>
        private EntityReference _er_zSmartTransaction;
        /// <summary>
        /// D365 Entity Reference to process
        /// </summary>
        private EntityReference _er_entityToProcess;
        /// <summary>
        /// Field name use to store related entity on the transactions
        /// </summary>
        private string _sFieldLinkRecord;
        /// <summary>
        /// zsmdartid of the current transaction
        /// </summary>
        private string _sZsmartId;
        /// <summary>
        /// zsmdartfieldof the current transaction
        /// </summary>
        private string _sZsmartField;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="service">Organisation service used to communicate with D365</param>
        /// <param name="tracingService">Tracing service used to store log on D365</param>
        /// <param name="er_zSmartTransaction">Current ZSmart Transaction Reference</param>
        public ZsmartTransaction_HELPER(IOrganizationService service, ITracingService tracingService, EntityReference er_zSmartTransaction)
        {
            _service = service;
            _tracingService = tracingService;
            _er_zSmartTransaction = er_zSmartTransaction;

            // Retrieve transaction
            _e_zSmartTransaction = _service.Retrieve(_er_zSmartTransaction.LogicalName, _er_zSmartTransaction.Id, new ColumnSet("ainos_jsondata", "ainos_operation", "ainos_entitytype", "ainos_accountid", "ainos_contactid", "ownerid"));

            // Retrieve mappings
            QueryExpression oQuery = new QueryExpression("ainos_zsmarttransactionmapping");
            oQuery.ColumnSet = new ColumnSet(true);
            oQuery.Criteria = new FilterExpression();
            oQuery.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            EntityCollection ecMappings = _service.RetrieveMultiple(oQuery);

            _converter = new ZsmartTransaction_CONVERTOR(tracingService, ecMappings, _service);
        }

        /// <summary>
        /// Start the global process
        /// </summary>
        public void Start()
        {
            try
            {
                #region Check required
                //Check if json data is not empty
                if (!_e_zSmartTransaction.Attributes.Contains("ainos_jsondata") || _e_zSmartTransaction.Attributes["ainos_jsondata"] == null)
                {
                    setErrorTransaction(ErrorType.JSONEmpty, "JSON Data is null");
                    return;
                }

                //Check if mandatory data is not empty
                if (!_e_zSmartTransaction.Attributes.Contains("ainos_entitytype") || _e_zSmartTransaction.Attributes["ainos_entitytype"] == null
                    || !_e_zSmartTransaction.Attributes.Contains("ainos_operation") || _e_zSmartTransaction.Attributes["ainos_operation"] == null)
                {
                    setErrorTransaction(ErrorType.MissingValue, "Entity Type or Operation is empty on this transaction");
                    return;
                }
                #endregion

                #region Get Values

                OptionSetValue osv_entityType = _e_zSmartTransaction.GetAttributeValue<OptionSetValue>("ainos_entitytype");
                OptionSetValue osv_operation = _e_zSmartTransaction.GetAttributeValue<OptionSetValue>("ainos_operation");
                int i_operation = osv_operation.Value;
                EntityReference defaultOwnerRef = _e_zSmartTransaction.GetAttributeValue<EntityReference>("ownerid");
                string s_jsonData = _e_zSmartTransaction.GetAttributeValue<string>("ainos_jsondata");
                s_jsonData = s_jsonData.Replace(" \":", "\":");
                Entity e_entityToProcess = null;

                #endregion

                #region Convert Json to Object

                if (osv_entityType.Value == (int)EntityType.Account) //Account
                {
                    ZSmartAccount zs_account = new ZSmartAccount();
                    var ms = new MemoryStream(Encoding.UTF8.GetBytes(s_jsonData));
                    var ser = new DataContractJsonSerializer(zs_account.GetType());
                    zs_account = ser.ReadObject(ms) as ZSmartAccount;
                    ms.Close();

                    e_entityToProcess = _converter.convertZsmartAccountToEntity(zs_account, defaultOwnerRef);

                    QueryExpression oQuery = new QueryExpression("account");
                    oQuery.Criteria.AddCondition("ainos_masterzsmartreference", ConditionOperator.Equal, zs_account.zSmartId);
                    oQuery.ColumnSet = new ColumnSet("statecode", "statuscode");
                    EntityCollection ecAccounts = _service.RetrieveMultiple(oQuery);

                    if (ecAccounts != null && ecAccounts.Entities != null && ecAccounts.Entities.Count == 1)
                    {
                        e_entityToProcess.Id = ecAccounts.Entities[0].Id;
                        if (i_operation == (int)Operation.Create) i_operation = (int)Operation.Update;

                        if (ecAccounts.Entities[0].GetAttributeValue<OptionSetValue>("statecode").Value == 1)
                        {
                            Entity accountToUpdate = new Entity(e_entityToProcess.LogicalName, e_entityToProcess.Id);
                            accountToUpdate.Attributes.Add("statecode", new OptionSetValue(0));
                            accountToUpdate.Attributes.Add("statuscode", new OptionSetValue(1));
                            _service.Update(accountToUpdate);
                        }
                    }
                    else if (i_operation == (int)Operation.Delete)
                        throw new ReferenceNotFoundError("Account with zsmart id '"+ zs_account.zSmartId + "' not exists");

                    _sZsmartId = zs_account.zSmartId;
                    _sZsmartField = "ainos_masterzsmartreference";
                    _sFieldLinkRecord = "ainos_accountid";
                }
                else if (osv_entityType.Value == (int)EntityType.Contact) //Contact
                {
                    ZSmartContact zs_contact = new ZSmartContact();
                    var ms = new MemoryStream(Encoding.UTF8.GetBytes(s_jsonData));
                    var ser = new DataContractJsonSerializer(zs_contact.GetType());
                    zs_contact = ser.ReadObject(ms) as ZSmartContact;
                    ms.Close();

                    e_entityToProcess = _converter.convertZsmartContactToEntity(zs_contact, defaultOwnerRef);

                    QueryExpression oQuery = new QueryExpression("contact");
                    oQuery.Criteria.AddCondition("ainos_contactid", ConditionOperator.Equal, zs_contact.zSmartId);
                    oQuery.ColumnSet = new ColumnSet("statecode", "statuscode");
                    EntityCollection ecContacts = _service.RetrieveMultiple(oQuery);

                    if (ecContacts != null && ecContacts.Entities != null && ecContacts.Entities.Count == 1)
                    {
                        e_entityToProcess.Id = ecContacts.Entities[0].Id;
                        if (i_operation == (int)Operation.Create) i_operation = (int)Operation.Update;

                        if (ecContacts.Entities[0].GetAttributeValue<OptionSetValue>("statecode").Value == 1)
                        {
                            Entity contactToUpdate = new Entity(e_entityToProcess.LogicalName, e_entityToProcess.Id);
                            contactToUpdate.Attributes.Add("statecode", new OptionSetValue(0));
                            contactToUpdate.Attributes.Add("statuscode", new OptionSetValue(1));
                            _service.Update(contactToUpdate);
                        }
                    }
                    else if (i_operation == (int)Operation.Delete)
                        throw new ReferenceNotFoundError("Contact with zsmart id '" + zs_contact.zSmartId + "' not exists");

                    _sZsmartField = "ainos_contactid";
                    _sZsmartId = zs_contact.zSmartId;
                    _sFieldLinkRecord = "ainos_contactid";
                }

                #endregion

                #region D365 Operations

                if (i_operation == (int)Operation.Create)
                {
                    EntityReference existingEntity = _e_zSmartTransaction.GetAttributeValue<EntityReference>(_sFieldLinkRecord);

                    if (existingEntity == null)
                    {
                        Guid id = _service.Create(e_entityToProcess);
                        _er_entityToProcess = new EntityReference(e_entityToProcess.LogicalName, id);
                    } else
                    {
                        e_entityToProcess.Id = existingEntity.Id;
                        _service.Update(e_entityToProcess);
                        _er_entityToProcess = new EntityReference(e_entityToProcess.LogicalName, _sZsmartField, _sZsmartId);
                    }
                    setSuccessTransaction();
                }
                else if (i_operation == (int)Operation.Update)
                {
                    _service.Update(e_entityToProcess);
                    _er_entityToProcess = new EntityReference(e_entityToProcess.LogicalName, _sZsmartField, _sZsmartId);
                    setSuccessTransaction();
                }
                else if (i_operation == (int)Operation.Delete)
                {
                    e_entityToProcess.Attributes.Add("statecode", new OptionSetValue(1));
                    e_entityToProcess.Attributes.Add("statuscode", new OptionSetValue(2));
                    _service.Update(e_entityToProcess);
                    _er_entityToProcess = new EntityReference(e_entityToProcess.LogicalName, _sZsmartField, _sZsmartId);
                    setSuccessTransaction();
                }

                #endregion
            }
            catch (SerializationException e)
            {
                setErrorTransaction(ErrorType.JSONSyntaxError, e);
            }
            catch (DataTypeError e)
            {
                setErrorTransaction(ErrorType.WrongDataType, e);
            }
            catch (DataValueError e)
            {
                setErrorTransaction(ErrorType.WrongDataValue, e);
            }
            catch (MissingDataError e)
            {
                setErrorTransaction(ErrorType.MissingValue, e);
            }
            catch (FaultException<OrganizationServiceFault> e)
            {
                if (e.Message.ToLower().Contains("reference already exists"))
                    setErrorTransaction(ErrorType.DuplicateReferenceFound, e);
                else if (e.Message.ToLower().Contains("requires that this set of attributes contains unique values"))
                    setErrorTransaction(ErrorType.DuplicateReferenceFound, e);
                else if (e.Message.ToLower().Contains("record with the specified key values does not exist"))
                    setErrorTransaction(ErrorType.ReferenceNotFound, e);
                else
                    setErrorTransaction(ErrorType.UNKNOWERROR, e);
            }
            catch (ReferenceNotFoundError e)
            {
                setErrorTransaction(ErrorType.ReferenceNotFound, e);
            }
            catch (DuplicateReferenceFoundError e)
            {
                setErrorTransaction(ErrorType.DuplicateReferenceFound, e);
            }
            catch (Exception e)
            {
                setErrorTransaction(ErrorType.UNKNOWERROR, e);
            }
        }

        /// <summary>
        /// Set the current transaction on success on D365
        /// </summary>
        private void setSuccessTransaction()
        {
            Entity e_transactionToUpdate = new Entity("ainos_zsmarttransaction", _er_zSmartTransaction.Id);
            e_transactionToUpdate.Attributes.Add("ainos_transactionstatus", new OptionSetValue((int)TransactionStatus.Success));
            e_transactionToUpdate.Attributes.Add("ainos_zsmartid", _sZsmartId);
            e_transactionToUpdate.Attributes.Add(_sFieldLinkRecord, _er_entityToProcess);
            e_transactionToUpdate.Attributes.Add("ainos_errormessage", null);
            e_transactionToUpdate.Attributes.Add("ainos_errortype", null);
            _service.Update(e_transactionToUpdate);
        }

        /// <summary>
        /// Set the current transaction on error on D365
        /// </summary>
        /// <param name="errorType">Type of the error occured</param>
        /// <param name="message">Message stored on the field "Error Message"</param>
        private void setErrorTransaction(ErrorType errorType, string message)
        {
            if (message.Length > 4000) message = message.Substring(0, 4000);

            Entity e_transactionToUpdate = new Entity("ainos_zsmarttransaction", _er_zSmartTransaction.Id);
            e_transactionToUpdate.Attributes.Add("ainos_errormessage", message);
            e_transactionToUpdate.Attributes.Add("ainos_transactionstatus", new OptionSetValue((int)TransactionStatus.Error));
            e_transactionToUpdate.Attributes.Add("ainos_zsmartid", _sZsmartId);
            e_transactionToUpdate.Attributes.Add("ainos_errortype", new OptionSetValue((int)errorType));
            _service.Update(e_transactionToUpdate);
        }

        /// <summary>
        /// Set the current transaction on error on D365
        /// </summary>
        /// <param name="errorType">Type of the error occured</param>
        /// <param name="e">Exception occured</param>
        private void setErrorTransaction(ErrorType errorType, Exception e)
        {
            string message = e.Message;
            if (message.Length > 4000) message = message.Substring(0, 4000);

            Entity e_transactionToUpdate = new Entity("ainos_zsmarttransaction", _er_zSmartTransaction.Id);
            e_transactionToUpdate.Attributes.Add("ainos_errormessage", message);
            e_transactionToUpdate.Attributes.Add("ainos_transactionstatus", new OptionSetValue((int)TransactionStatus.Error));
            e_transactionToUpdate.Attributes.Add("ainos_zsmartid", _sZsmartId);
            e_transactionToUpdate.Attributes.Add("ainos_transactionerrordetails", e.StackTrace);
            e_transactionToUpdate.Attributes.Add("ainos_errortype", new OptionSetValue((int)errorType));
            _service.Update(e_transactionToUpdate);
        }
    }
}
