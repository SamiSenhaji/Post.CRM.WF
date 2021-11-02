using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using Post.CRM.WF.MODEL.SAP;
using Post.CRM.WF.MODEL.SAP.Opportunities;
using Post.SAP.Webapp.Logics.Helpers;
using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Post.CRM.WF.HELPER.SAP
{
    public class SAPTransaction_HELPER
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
        /// Generic Serializer Helper used to convert a xml string or <T> object to a T object or an xml string
        /// </summary>
        private GenericSerializerHelper _serializer;
        /// <summary>
        /// Converter Service used to convert a Sap to an entity crm
        /// </summary>
        private SAPTransaction_CONVERTOR _converter;
        /// <summary>
        /// Entity Transaction
        /// </summary>
        private Entity _e_SapTransaction;
        /// <summary>
        /// Current Sap Transaction Reference
        /// </summary>
        private EntityReference _er_SapTransaction;
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
        private string _sSapId;
        /// <summary>
        /// zsmdartfieldof the current transaction
        /// </summary>
        private string _sSapField;


        private Entity QuoteToUpdate = null;
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="service">Organisation service used to communicate with D365</param>
        /// <param name="tracingService">Tracing service used to store log on D365</param>
        /// <param name="er_SAPTransaction">Current SAP Transaction Reference</param>
        public SAPTransaction_HELPER(IOrganizationService service, ITracingService tracingService, EntityReference er_SAPTransaction)
        {
            _service = service;
            _tracingService = tracingService;
            _er_SapTransaction = er_SAPTransaction;

            // Retrieve transaction
            _e_SapTransaction = _service.Retrieve(_er_SapTransaction.LogicalName, _er_SapTransaction.Id, new ColumnSet("ainos_xmldata", "ainos_operation", "ainos_entitytype", "ainos_opportunity", "ainos_quote", "ainos_order", "ownerid"));

            // Retrieve mappings
            QueryExpression oQuery = new QueryExpression("ainos_saptransactionmapping");
            oQuery.ColumnSet = new ColumnSet(true);
            oQuery.Criteria = new FilterExpression();
            oQuery.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            EntityCollection ecMappings = _service.RetrieveMultiple(oQuery);
            _serializer = new GenericSerializerHelper();
            _converter = new SAPTransaction_CONVERTOR(tracingService, ecMappings, _service);
        }

        /// <summary>
        /// Start the global process
        /// </summary>
        public void Start(ITracingService tracingService)
        {
            try
            {
                #region Check required

                //Check if json data is not empty
                if (!_e_SapTransaction.Attributes.Contains("ainos_xmldata") || _e_SapTransaction.Attributes["ainos_xmldata"] == null)
                {
                    tracingService.Trace("Check if json data is not empty");
                    setErrorTransaction(ErrorType.DATAEmpty, "XML Data is null");
                    return;
                }

                //Check if mandatory data is not empty
                if (!_e_SapTransaction.Attributes.Contains("ainos_entitytype") || _e_SapTransaction.Attributes["ainos_entitytype"] == null
                    || !_e_SapTransaction.Attributes.Contains("ainos_operation") || _e_SapTransaction.Attributes["ainos_operation"] == null)
                {
                    tracingService.Trace("Check if mandatory data is not empty");
                    setErrorTransaction(ErrorType.MissingValue, "Entity Type or Operation is empty on this transaction");
                    return;
                }
                #endregion

                #region Get Values
                tracingService.Trace("Get Values");
                OptionSetValue osv_entityType = _e_SapTransaction.GetAttributeValue<OptionSetValue>("ainos_entitytype");
                OptionSetValue osv_operation = _e_SapTransaction.GetAttributeValue<OptionSetValue>("ainos_operation");
                int i_operation = osv_operation.Value;
                EntityReference defaultOwnerRef = _e_SapTransaction.GetAttributeValue<EntityReference>("ownerid");
                string s_xmlData = _e_SapTransaction.GetAttributeValue<string>("ainos_xmldata");
                Entity e_entityToProcess = null;
                tracingService.Trace(s_xmlData);
                #endregion

                #region Convert Json to Object
                SAPTransaction_VALIDATOR validator = new SAPTransaction_VALIDATOR();
                switch (osv_entityType.Value)
                {
                    #region Opportunity
                    //case (int)EntityType.Opportunity:

                    //    INQUIRY_CREATEFROMDATA201 sap_opportunity = this._serializer.ConvertTo<INQUIRY_CREATEFROMDATA201>(s_xmlData);

                    //    SAPTransaction_VALIDATOR validator = new SAPTransaction_VALIDATOR();
                    //    if( validator.Validate(sap_opportunity) )
                    //    {
                    //        e_entityToProcess = _converter.convertSapOpportunityToEntity(sap_opportunity, defaultOwnerRef);

                    //        QueryExpression oQuery = new QueryExpression("opportunity");
                    //        oQuery.Criteria.AddCondition("ainos_mastersapreference", ConditionOperator.Equal, sap_opportunity.IDOC.EDI_DC40.DOCNUM);
                    //        oQuery.ColumnSet = new ColumnSet("statecode", "statuscode");
                    //        EntityCollection ecAccounts = _service.RetrieveMultiple(oQuery);

                    //        if (ecAccounts != null && ecAccounts.Entities != null && ecAccounts.Entities.Count == 1)
                    //        {
                    //            e_entityToProcess.Id = ecAccounts.Entities[0].Id;
                    //            if (i_operation == (int)Operation.Create) i_operation = (int)Operation.Update;

                    //            if (ecAccounts.Entities[0].GetAttributeValue<OptionSetValue>("statecode").Value == 1)
                    //            {
                    //                Entity accountToUpdate = new Entity(e_entityToProcess.LogicalName, e_entityToProcess.Id);
                    //                accountToUpdate.Attributes.Add("statecode", new OptionSetValue(0));
                    //                accountToUpdate.Attributes.Add("statuscode", new OptionSetValue(1));
                    //                _service.Update(accountToUpdate);
                    //            }
                    //        }
                    //        else if (i_operation == (int)Operation.Delete)
                    //            throw new ReferenceNotFoundError("Opportunity with Sap id '" + sap_opportunity.IDOC.EDI_DC40.DOCNUM + "' not exists");

                    //        _sSapId = sap_opportunity.IDOC.EDI_DC40.DOCNUM;
                    //        _sSapField = "ainos_mastersapreference";
                    //        _sFieldLinkRecord = "ainos_opportunity";
                    //    }
                    //    else
                    //    {
                    //        setErrorTransaction(validator.ToString());
                    //        return;
                    //    }                       
                    //    break; 
                    #endregion

                    #region Quote
                    case (int)EntityType.Quote:
                        // if(opération == outgoing){- retreive l'opportunity liée à la quote
                        //- transformer l'opportunity en Json
                        //-  appeler la webapp avec un call post pour créer une inquery
                        //-  update la transaction avec le json de l'opportunity dans xmlData
                        //- set transaction status succes}



                        tracingService.Trace("Quote-->:serialization");

                        var sap_quote = this._serializer.ConvertToQuoteObject(s_xmlData);//JsonConvert.DeserializeObject<quote_>(s_xmlData);

                        tracingService.Trace("Quote-->:serialization--finish");
                        if (true)//validator.Validate(sap_quote)
                        {

                            e_entityToProcess = _converter.convertSapQuoteToEntity(sap_quote, defaultOwnerRef);
                            tracingService.Trace("ConverterQuote-->--finish");

                            QueryExpression oQuery = new QueryExpression("quote");
                            oQuery.Criteria.AddCondition("ainos_mastersapreference", ConditionOperator.Equal, sap_quote.ORDERS05.IDOC.EDI_DC40.DOCNUM);
                            oQuery.ColumnSet = new ColumnSet("statecode", "statuscode");
                            EntityCollection ecQuotes = _service.RetrieveMultiple(oQuery);
                            tracingService.Trace("retreiveecQuote-->--finish");
                            if (ecQuotes != null && ecQuotes.Entities != null && ecQuotes.Entities.Count == 1)
                            {
                                e_entityToProcess.Id = ecQuotes.Entities[0].Id;
                                if (i_operation == (int)Operation.Create) i_operation = (int)Operation.Update;

                                if (true/*ecQuotes.Entities[0]./*GetAttributeValue<OptionSetValue>("statecode").Value == 1*/)
                                {
                                    QuoteToUpdate = new Entity(ecQuotes.Entities[0].LogicalName, ecQuotes.Entities[0].Id);
                                    //    QuoteToUpdate.Attributes.Add("statecode", new OptionSetValue(0));
                                    //    QuoteToUpdate.Attributes.Add("statuscode", new OptionSetValue(1));
                                    // _service.Update(QuoteToUpdate);
                                }
                            }
                            else if (i_operation == (int)Operation.Delete)
                                throw new ReferenceNotFoundError("Opportunity with Sap id '" + sap_quote + "' not exists");

                            _sSapId = sap_quote.ORDERS05.IDOC.EDI_DC40.DOCNUM;
                            _sSapField = "ainos_mastersapreference";
                            _sFieldLinkRecord = "ainos_quote";
                        }
                        else
                        {
                            setErrorTransaction(validator.ToString());
                            return;
                        }

                        break;
                    #endregion
                    #region Order
                    case (int)EntityType.Order:


                        break;
                    #endregion
                    #region Default
                    default:

                        setErrorTransaction(ErrorType.MissingValue, "Entity Type is not accepted for this SAP transaction, only Opportunity, Quote, Order are valid");
                        return;
                        #endregion

                }


                #endregion

                #region D365 Operations

                #region Create Operation
                if (i_operation == (int)Operation.Create)
                {
                    EntityReference existingEntity = _e_SapTransaction.GetAttributeValue<EntityReference>(_sFieldLinkRecord);

                    if (existingEntity == null)
                    {
                        Guid id = _service.Create(e_entityToProcess);
                        _er_entityToProcess = new EntityReference(e_entityToProcess.LogicalName, id);

                    }
                    else
                    {
                        e_entityToProcess.Id = existingEntity.Id;
                        _service.Update(e_entityToProcess);
                        _er_entityToProcess = new EntityReference(e_entityToProcess.LogicalName, _sSapField, _sSapId);
                    }
                    setSuccessTransaction();
                }
                #endregion
                #region Update Operation
                else if (i_operation == (int)Operation.Update)
                {
                    _er_entityToProcess = new EntityReference(QuoteToUpdate.LogicalName, QuoteToUpdate.Id);
                    QuoteToUpdate.Attributes.Add("ainos_statusquote", new OptionSetValue(192400001));
                    // e_entityToProcess.Attributes.Add("ainos_statusquote", new OptionSetValue(192400001));
                    _service.Update(QuoteToUpdate);
                    // Activate the quote
                    SetStateRequest activateQuote = new SetStateRequest()
                    {
                        EntityMoniker = QuoteToUpdate.ToEntityReference(),
                        State = new OptionSetValue(1),
                        Status = new OptionSetValue(2)
                    };
                    _service.Execute(activateQuote);

                    WinQuoteRequest winQuoteRequest = new WinQuoteRequest();
                    winQuoteRequest.QuoteClose = new Entity("quoteclose");
                    winQuoteRequest.QuoteClose.Id = QuoteToUpdate.Id;
                    winQuoteRequest.QuoteClose.Attributes.Add("actualend", DateTime.Now);
                    winQuoteRequest.QuoteClose.Attributes.Add("subject", "Quote Close" + DateTime.Now.ToString());
                    winQuoteRequest.QuoteClose.Attributes.Add("quoteid", new EntityReference(QuoteToUpdate.LogicalName, QuoteToUpdate.Id));
                    winQuoteRequest.Status = new OptionSetValue(-1);
                    _service.Execute(winQuoteRequest);

                    //Console.WriteLine("Quote won.");


                    // Define columns to be retrieved after creating the order
                    ColumnSet salesOrderColumns =
                        new ColumnSet("salesorderid", "totalamount");

                    // Convert the quote to a sales order
                    ConvertQuoteToSalesOrderRequest convertQuoteRequest =
                        new ConvertQuoteToSalesOrderRequest()
                        {
                            QuoteId = QuoteToUpdate.Id,
                            ColumnSet = salesOrderColumns
                        };
                    ConvertQuoteToSalesOrderResponse convertQuoteResponse =
                        (ConvertQuoteToSalesOrderResponse)_service.Execute(convertQuoteRequest);
                    Entity salesOrder = convertQuoteResponse.Entity;
                    var _salesOrderId = salesOrder.Id;

                    setSuccessTransaction();
                }
                #endregion
                #region Delete Operation
                else if (i_operation == (int)Operation.Delete)
                {
                    //e_entityToProcess.Attributes.Add("statecode", new OptionSetValue(1));
                    //e_entityToProcess.Attributes.Add("statuscode", new OptionSetValue(2));
                    //_service.Update(e_entityToProcess);
                    //_er_entityToProcess = new EntityReference(e_entityToProcess.LogicalName, _sSapField, _sSapId);
                    _service.Delete(e_entityToProcess.LogicalName, e_entityToProcess.Id);
                    setSuccessTransaction();
                }
                #endregion

                #endregion
            }
            catch (SerializationException e)
            {
                setErrorTransaction(ErrorType.DATASyntaxError, e);
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
            Entity e_transactionToUpdate = new Entity("ainos_saptransaction", _er_SapTransaction.Id);
            e_transactionToUpdate.Attributes.Add("ainos_transactionstatus", new OptionSetValue((int)TransactionStatus.Success));
            e_transactionToUpdate.Attributes.Add("ainos_sapid", _sSapId);
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

            Entity e_transactionToUpdate = new Entity("ainos_saptransaction", _er_SapTransaction.Id);
            e_transactionToUpdate.Attributes.Add("ainos_errormessage", message);
            e_transactionToUpdate.Attributes.Add("ainos_transactionstatus", new OptionSetValue((int)TransactionStatus.Error));
            e_transactionToUpdate.Attributes.Add("ainos_sapid", _sSapId);
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

            Entity e_transactionToUpdate = new Entity("ainos_saptransaction", _er_SapTransaction.Id);
            e_transactionToUpdate.Attributes.Add("ainos_errormessage", message);
            e_transactionToUpdate.Attributes.Add("ainos_transactionstatus", new OptionSetValue((int)TransactionStatus.Error));
            e_transactionToUpdate.Attributes.Add("ainos_sapid", _sSapId);
            e_transactionToUpdate.Attributes.Add("ainos_transactionerrordetails", e.StackTrace);
            e_transactionToUpdate.Attributes.Add("ainos_errortype", new OptionSetValue((int)errorType));
            _service.Update(e_transactionToUpdate);
        }

        /// <summary>
        /// Set the current transaction on error on D365
        /// </summary>
        /// <param name="errorType">Type of the error occured</param>
        /// <param name="e">Exception occured</param>
        private void setErrorTransaction(string errorDetails)
        {
            Entity e_transactionToUpdate = new Entity("ainos_saptransaction", _er_SapTransaction.Id);
            e_transactionToUpdate.Attributes.Add("ainos_errormessage", "Data validation error detected.");
            e_transactionToUpdate.Attributes.Add("ainos_transactionstatus", new OptionSetValue((int)TransactionStatus.Error));
            e_transactionToUpdate.Attributes.Add("ainos_sapid", _sSapId);
            e_transactionToUpdate.Attributes.Add("ainos_transactionerrordetails", errorDetails);
            e_transactionToUpdate.Attributes.Add("ainos_errortype", new OptionSetValue((int)ErrorType.ValidationError));
            _service.Update(e_transactionToUpdate);
        }
    }
}
