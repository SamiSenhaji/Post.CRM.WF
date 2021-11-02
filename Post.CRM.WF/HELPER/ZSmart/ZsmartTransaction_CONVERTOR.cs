using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Post.CRM.WF.MODEL.ZSMART;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Post.CRM.WF.HELPER.ZSMART
{
    public class ZsmartTransaction_CONVERTOR
    {
        /// <summary>
        /// Tracing service used to store log on D365
        /// </summary>
        private ITracingService _tracingService;
        /// <summary>
        /// Zsmart Transaction Mappings Collection
        /// </summary>
        private EntityCollection _mappings;
        /// <summary>
        /// Organisation service used to communicate with D365
        /// </summary>
        private IOrganizationService _service;

        /// <summary>
        /// Constructor of the class ZsmartTransaction_CONVERTOR
        /// </summary>
        /// <param name="tracingService">Tracing service used to store log on D365</param>
        /// <param name="mappings">Zsmart Transaction Mappings Collection</param>
        /// <param name="service">Organisation service used to communicate with D365</param>
        public ZsmartTransaction_CONVERTOR(ITracingService tracingService, EntityCollection mappings, IOrganizationService service)
        {
            _tracingService = tracingService;
            _mappings = mappings;
            _service = service;
        }

        /// <summary>
        /// Convert a ZSmartAccount to an D365 Entity
        /// </summary>
        /// <param name="zs_account">Zsmart Account to convert</param>
        /// <param name="defaultOwnerRef">Default Owner if field account manager is not filled</param>
        /// <returns>the ZsmartAccount converted to a D365 Entity</returns>
        public Entity convertZsmartAccountToEntity(ZSmartAccount zs_account, EntityReference defaultOwnerRef)
        {
            if (string.IsNullOrWhiteSpace(zs_account.zSmartId))
                throw new MissingDataError("CUST_CODE is empty");

            Entity e_account = new Entity("account");
            e_account.Attributes.Add("ainos_masterzsmartreference", zs_account.zSmartId);

            //if (!string.IsNullOrWhiteSpace(zs_account.name))
                e_account.Attributes.Add("name", zs_account.name);

            string address1_line1 = "";
            if (!string.IsNullOrWhiteSpace(zs_account.streetName))
                address1_line1 += zs_account.streetName;
            if (!string.IsNullOrWhiteSpace(zs_account.streetNumber))
                address1_line1 += " " + zs_account.streetNumber;

           //if (!string.IsNullOrWhiteSpace(address1_line1))
                e_account.Attributes.Add("address1_line1", address1_line1);

            //if (!string.IsNullOrWhiteSpace(zs_account.zipCode))
                e_account.Attributes.Add("address1_postalcode", zs_account.zipCode);

            //if (!string.IsNullOrWhiteSpace(zs_account.city))
                e_account.Attributes.Add("address1_city", zs_account.city);

            if (!string.IsNullOrWhiteSpace(zs_account.country))
                e_account.Attributes.Add("ainos_address1_countryid", retrieveEntityWithValue("ainos_country", "ainos_countryid", "ainos_name", zs_account.country));
            else
                e_account.Attributes.Add("ainos_address1_countryid", null);

            e_account.Attributes.Add("ownerid", defaultOwnerRef);
            if (!string.IsNullOrWhiteSpace(zs_account.accountManager))
            {
                EntityReference ownerId = retrieveEntityWithValue("systemuser", "systemuserid", "fullname", zs_account.accountManager, false);
                if (ownerId != null) e_account.Attributes["ownerid"] = ownerId;
            }
            if (!string.IsNullOrWhiteSpace(zs_account.secondAccountManager))
            {
                EntityReference secondOwnerId = retrieveEntityWithValue("systemuser", "systemuserid", "fullname", zs_account.secondAccountManager, false);
                if (secondOwnerId != null) e_account.Attributes.Add("ainos_secondaccountmanagerid", secondOwnerId);
            }
            else
            {
                e_account.Attributes.Add("ainos_secondaccountmanagerid", null);
            }
            //if (!string.IsNullOrWhiteSpace(zs_account.creditScore))
                e_account.Attributes.Add("ainos_creditscore", getMappings("ainos_creditscore", zs_account.creditScore));
            //if (!string.IsNullOrWhiteSpace(zs_account.legalStatus))
                e_account.Attributes.Add("ainos_legalstatus", getMappings("ainos_legalstatus", zs_account.legalStatus));
            //if (!string.IsNullOrWhiteSpace(zs_account.language))
                e_account.Attributes.Add("ainos_language", getMappings("ainos_language", zs_account.language));
            //if (!string.IsNullOrWhiteSpace(zs_account.mainPhone))
                e_account.Attributes.Add("telephone1", zs_account.mainPhone);
            //if (!string.IsNullOrWhiteSpace(zs_account.mainEmail))
            //{
                string emailaddress1, emailaddress2, emailaddress3, otheremails;
                splitEmails(zs_account.mainEmail, out emailaddress1, out emailaddress2, out emailaddress3, out otheremails);

                //if (!string.IsNullOrWhiteSpace(emailaddress1)) 
                    e_account.Attributes.Add("emailaddress1", emailaddress1);
                //if (!string.IsNullOrWhiteSpace(emailaddress2)) 
                    e_account.Attributes.Add("emailaddress2", emailaddress2);
                //if (!string.IsNullOrWhiteSpace(emailaddress3)) 
                    e_account.Attributes.Add("emailaddress3", emailaddress3);
                //if (!string.IsNullOrWhiteSpace(otheremails)) 
                    e_account.Attributes.Add("ainos_otheremails", otheremails);
            //}
            //if (!string.IsNullOrWhiteSpace(zs_account.mainFax))
                e_account.Attributes.Add("fax", zs_account.mainFax);
            //if (!string.IsNullOrWhiteSpace(zs_account.vat))
                e_account.Attributes.Add("ainos_vat", zs_account.vat);
            //if (!string.IsNullOrWhiteSpace(zs_account.segmentation))
                e_account.Attributes.Add("ainos_segmentation", getMappings("ainos_segmentation", zs_account.segmentation));
            //if (!string.IsNullOrWhiteSpace(zs_account.zSmartStatus))
                e_account.Attributes.Add("ainos_zsmartstatus", getMappings("ainos_zsmartstatus", zs_account.zSmartStatus));
            //if (!string.IsNullOrWhiteSpace(zs_account.zSmartCustomerType))
                e_account.Attributes.Add("ainos_customertype", getMappings("ainos_customertype", zs_account.zSmartCustomerType));
            if (!string.IsNullOrWhiteSpace(zs_account.birthdate))
            {
                DateTime dt_birthdate;
                if (!DateTime.TryParse(zs_account.birthdate, CultureInfo.GetCultureInfo("fr-FR"), DateTimeStyles.None, out dt_birthdate))
                    throw new DataTypeError("BIRTHDAY_DAY is not a date");
                e_account.Attributes.Add("ainos_birthdaydate", dt_birthdate);
            }
            else
                e_account.Attributes.Add("ainos_birthdaydate", null);
            if (!string.IsNullOrWhiteSpace(zs_account.createdon))
            {
                DateTime dt_createdon;
                if (!DateTime.TryParse(zs_account.createdon, CultureInfo.GetCultureInfo("fr-FR"), DateTimeStyles.None, out dt_createdon))
                    throw new DataTypeError("CREATED_DATE is not a date");
                e_account.Attributes.Add("overriddencreatedon", dt_createdon);
            }
            //if (!string.IsNullOrWhiteSpace(zs_account.psf))
            //{
            bool? b_psf = getBooleanValue(zs_account.psf);
            if(b_psf.HasValue)
                e_account.Attributes.Add("ainos_psf", b_psf);
            else
                e_account.Attributes.Add("ainos_psf", null);
            //}
            //if (!string.IsNullOrWhiteSpace(zs_account.documentType))
            e_account.Attributes.Add("ainos_documenttype", getMappings("ainos_documenttype", zs_account.documentType));
            //if (!string.IsNullOrWhiteSpace(zs_account.documentNumber))
                e_account.Attributes.Add("ainos_documentnumber", zs_account.documentNumber);
            //if (!string.IsNullOrWhiteSpace(zs_account.naceCode))
                e_account.Attributes.Add("ainos_nacecodeid", findOrCreateNaceCode(zs_account.naceCode));
            //if (!string.IsNullOrWhiteSpace(zs_account.postSubsidiary))
            //{
            bool? b_postSubsidiary = getBooleanValue(zs_account.postSubsidiary);
            if (b_postSubsidiary.HasValue)
                e_account.Attributes.Add("ainos_postsubsidiary", b_postSubsidiary);
            else
                e_account.Attributes.Add("ainos_postsubsidiary", null);
            //}
            //if (!string.IsNullOrWhiteSpace(zs_account.ainos_customeradvertisement))
            //{
            bool? b_ainos_customeradvertisement = getBooleanValue(zs_account.ainos_customeradvertisement);
            if (b_ainos_customeradvertisement.HasValue)
                e_account.Attributes.Add("ainos_customeradvertisement", b_ainos_customeradvertisement);
            else
                e_account.Attributes.Add("ainos_customeradvertisement", null);
            //}
            //if (!string.IsNullOrWhiteSpace(zs_account.ainos_icmsreference))
            e_account.Attributes.Add("ainos_icmsreference", zs_account.ainos_icmsreference);

            OptionSetValueCollection osvcRelationShip = new OptionSetValueCollection();
            OptionSetValue osv_customer = new OptionSetValue(192400002);
            osvcRelationShip.Add(osv_customer);
            e_account.Attributes.Add("ainos_mo_relationshiptype", osvcRelationShip);

            return e_account;
        }

        /// <summary>
        /// Convert a ZSmartContact to an D365 Entity
        /// </summary>
        /// <param name="zs_contact">Zsmart Contact to convert</param>
        /// <param name="defaultOwnerRef">Default Owner if field account manager is not filled</param>
        /// <returns>the ZsmartContact converted to a D365 Entity</returns>
        public Entity convertZsmartContactToEntity(ZSmartContact zs_contact, EntityReference defaultOwnerRef)
        {
            if (string.IsNullOrWhiteSpace(zs_contact.zSmartId))
                throw new MissingDataError("CONTACTID is empty");

            Entity e_contact = new Entity("contact");
            e_contact.Attributes.Add("ainos_contactid", zs_contact.zSmartId);

            //if (!string.IsNullOrWhiteSpace(zs_contact.firstName))
                e_contact.Attributes.Add("firstname", zs_contact.firstName);
            //if (!string.IsNullOrWhiteSpace(zs_contact.lastName))
                e_contact.Attributes.Add("lastname", zs_contact.lastName);

            if (!string.IsNullOrWhiteSpace(zs_contact.accountZSmartId))
                e_contact.Attributes.Add("parentcustomerid", new EntityReference("account", "ainos_masterzsmartreference", zs_contact.accountZSmartId));
            else
                e_contact.Attributes.Add("parentcustomerid", null);

            //if (!string.IsNullOrWhiteSpace(zs_contact.contactType))
                e_contact.Attributes.Add("ainos_contacttype", getMappings("ainos_contacttype", zs_contact.contactType));

            e_contact.Attributes.Add("ownerid", defaultOwnerRef);
            if (!string.IsNullOrWhiteSpace(zs_contact.owner))
            {
                EntityReference ownerId = retrieveEntityWithValue("systemuser", "systemuserid", "fullname", zs_contact.owner, false);
                if(ownerId != null) e_contact.Attributes["ownerid"] = ownerId;
            }

           // if (!string.IsNullOrWhiteSpace(zs_contact.contactLevel))
                e_contact.Attributes.Add("ainos_contactlevel", getMappings("ainos_contactlevel", zs_contact.contactLevel));

            if (!string.IsNullOrWhiteSpace(zs_contact.makers) && zs_contact.makers.ToLower().Trim() != "null")
            {
                List<string> splitMakers = zs_contact.makers.Split('|').ToList();
                OptionSetValueCollection osvc = new OptionSetValueCollection();
                foreach (string marker in splitMakers)
                {
                    OptionSetValue osv_marker = getMappings("ainos_os_markers", marker);
                    osvc.Add(osv_marker);
                }
                e_contact.Attributes.Add("ainos_os_markers", osvc);
            } else
            {
                e_contact.Attributes.Add("ainos_os_markers", null);
            }
            //if (!string.IsNullOrWhiteSpace(zs_contact.civility))
                e_contact.Attributes.Add("ainos_salutation", getMappings("ainos_salutation", zs_contact.civility));
            //if (!string.IsNullOrWhiteSpace(zs_contact.gender))
                e_contact.Attributes.Add("gendercode", getMappings("gendercode", zs_contact.gender));
            //if (!string.IsNullOrWhiteSpace(zs_contact.title))
                e_contact.Attributes.Add("jobtitle", zs_contact.title);
            //if (!string.IsNullOrWhiteSpace(zs_contact.businessPhone))
                e_contact.Attributes.Add("telephone1", zs_contact.businessPhone);
            //if (!string.IsNullOrWhiteSpace(zs_contact.homePhone))
                e_contact.Attributes.Add("telephone2", zs_contact.homePhone);
            //if (!string.IsNullOrWhiteSpace(zs_contact.mobilePhone))
                e_contact.Attributes.Add("mobilephone", zs_contact.mobilePhone);
            //if (!string.IsNullOrWhiteSpace(zs_contact.fax))
                e_contact.Attributes.Add("fax", zs_contact.fax);
            //if (!string.IsNullOrWhiteSpace(zs_contact.email))
            //{
                string emailaddress1, emailaddress2, emailaddress3, otheremails;
                splitEmails(zs_contact.email, out emailaddress1, out emailaddress2, out emailaddress3, out otheremails);

                //if (!string.IsNullOrWhiteSpace(emailaddress1))
                e_contact.Attributes.Add("emailaddress1", emailaddress1);
                //if (!string.IsNullOrWhiteSpace(emailaddress2))
                e_contact.Attributes.Add("emailaddress2", emailaddress2);
                //if (!string.IsNullOrWhiteSpace(emailaddress3))
                e_contact.Attributes.Add("emailaddress3", emailaddress3);
                //if (!string.IsNullOrWhiteSpace(otheremails))
                e_contact.Attributes.Add("ainos_otheremails", otheremails);
            //}
            //if (!string.IsNullOrWhiteSpace(zs_contact.nationality))
                e_contact.Attributes.Add("ainos_nationalityid", retrieveEntityWithValue("ainos_country", "ainos_countryid", "ainos_name", zs_contact.nationality));
            //if (!string.IsNullOrWhiteSpace(zs_contact.language))
                e_contact.Attributes.Add("ainos_language", getMappings("ainos_language", zs_contact.language));

            string address1_line1 = "";
            if (!string.IsNullOrWhiteSpace(zs_contact.streetName))
                address1_line1 += zs_contact.streetName;
            if (!string.IsNullOrWhiteSpace(zs_contact.streetNumber))
                address1_line1 += " " + zs_contact.streetNumber;
            //if (!string.IsNullOrWhiteSpace(address1_line1))
                e_contact.Attributes.Add("address1_line1", address1_line1);

            //if (!string.IsNullOrWhiteSpace(zs_contact.zipCode))
                e_contact.Attributes.Add("address1_postalcode", zs_contact.zipCode);

            //if (!string.IsNullOrWhiteSpace(zs_contact.city))
                e_contact.Attributes.Add("address1_city", zs_contact.city);

            //if (!string.IsNullOrWhiteSpace(zs_contact.country))
                e_contact.Attributes.Add("address1_country", zs_contact.country);

            return e_contact;
        }

        /// <summary>
        /// Find on D365 if a nace code exist based on his code
        /// If the nace is not found, the method create the nace code record
        /// </summary>
        /// <param name="naceCode">Code of the nace code to check</param>
        /// <returns>The entityreference of the nace code (existing or created)</returns>
        public EntityReference findOrCreateNaceCode(string naceCode)
        {
            if (string.IsNullOrWhiteSpace(naceCode)) return null;

            EntityReference toReturn;
            QueryExpression oQuery = new QueryExpression("ainos_nacecode");
            oQuery.Criteria.AddCondition("ainos_code", ConditionOperator.Equal, naceCode);
            EntityCollection ecNaceCodes = _service.RetrieveMultiple(oQuery);
            if (ecNaceCodes == null || ecNaceCodes.Entities == null || ecNaceCodes.Entities.Count == 0)
            {
                Entity naceCodeToCreate = new Entity("ainos_nacecode");
                naceCodeToCreate.Attributes.Add("ainos_name", naceCode);
                naceCodeToCreate.Attributes.Add("ainos_code", naceCode);
                naceCodeToCreate.Id = _service.Create(naceCodeToCreate);
                toReturn = naceCodeToCreate.ToEntityReference();
            }
            else
            {
                toReturn = ecNaceCodes.Entities[0].ToEntityReference();
            }

            return toReturn;
        }

        /// <summary>
        /// Split a string who contains some emails split by |
        /// </summary>
        /// <param name="mainEmail">string to split</param>
        /// <param name="emailaddress1">the first email of the mainEmail parameter</param>
        /// <param name="emailaddress2">the second email of the mainEmail parameter</param>
        /// <param name="emailaddress3">the third email of the mainEmail parameter</param>
        /// <param name="otheremails">all the other emails of the mainEmai parameter</param>
        private void splitEmails(string mainEmail, out string emailaddress1, out string emailaddress2, out string emailaddress3, out string otheremails)
        {
            emailaddress1 = null;
            emailaddress2 = null;
            emailaddress3 = null;
            otheremails = null;

            if (string.IsNullOrWhiteSpace(mainEmail)) return;

            List<string> emails = mainEmail.Split('|').ToList();

            if (emails.Count > 0)
            {
                emailaddress1 = emails[0];
                emails.RemoveAt(0);
            }
            if (emails.Count > 0)
            {
                emailaddress2 = emails[0];
                emails.RemoveAt(0);
            }
            if (emails.Count > 0)
            {
                emailaddress3 = emails[0];
                emails.RemoveAt(0);
            }
            if (emails.Count > 0)
            {
                otheremails = string.Join("|", emails);
            }
        }

        /// <summary>
        /// Convert a string to boolean
        /// </summary>
        /// <param name="s">string to convert</param>
        /// <returns>the string converted to a boolean</returns>
        private bool? getBooleanValue(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;

            string sLower = s.ToLower();
            bool b_toreturn;
            if (sLower == "true" || sLower == "1" || sLower == "yes" || sLower == "oui" || sLower == "o" || sLower == "y")
                b_toreturn = true;
            else if (sLower == "false" || sLower == "0" || sLower == "no" || sLower == "non" || sLower == "n" || sLower == "n")
                b_toreturn = false;
            else
                throw new DataTypeError("Value '"+ s + "' is not a boolean");

            return b_toreturn;
        }

        /// <summary>
        /// Find an Record on D365 based on a condition field = value
        /// </summary>
        /// <param name="entity">The entity name to find</param>
        /// <param name="entityId">The primary name of the entity to find</param>
        /// <param name="field">Field of the condition</param>
        /// <param name="value">Value of the condition</param>
        /// <param name="throwException">Throw an exception if the record is not found on D365</param>
        /// <returns>The EntityReference of the founded record</returns>
        public EntityReference retrieveEntityWithValue(string entity, string entityId, string field, string value, bool throwException = true)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            EntityReference toReturn = null;
            QueryExpression oQuery = new QueryExpression(entity);
            oQuery.ColumnSet = new ColumnSet(entityId);
            oQuery.Criteria = new FilterExpression(LogicalOperator.And);
            oQuery.Criteria.AddCondition(field, ConditionOperator.Equal, value);

            EntityCollection ec_Entities = _service.RetrieveMultiple(oQuery);
            if(ec_Entities == null || ec_Entities.Entities == null || ec_Entities.Entities.Count == 0)
            {
                if(throwException)
                    throw new ReferenceNotFoundError(string.Format("A record with the specified values ({0} : {1}) does not exist in {2} entity", field, value, entity));
            }
            else if(ec_Entities.Entities.Count > 1)
            {
                if(throwException)
                    throw new DuplicateReferenceFoundError(string.Format("Multiple records with the specified values ({0} : {1}) exist in {2} entity", field, value, entity));
            }
            else
            {
                toReturn = ec_Entities.Entities[0].ToEntityReference();
            }
            return toReturn;
        }
        
        /// <summary>
        /// retrieve a Zsmart value in the mapping for a specific field
        /// </summary>
        /// <param name="fieldCRM">The field name</param>
        /// <param name="valueZsmart">The Zsmart Value</param>
        /// <returns>The Optionsetvalue found in the mappings collection</returns>
        public OptionSetValue getMappings(string fieldCRM, string valueZsmart)
        {
            if (string.IsNullOrWhiteSpace(valueZsmart)) return null;
            List<int> toReturn = _mappings.Entities.Where(it => it.GetAttributeValue<string>("ainos_crmfield").ToLower() == fieldCRM.ToLower() && it.GetAttributeValue<string>("ainos_zsmartvalue").ToLower() == valueZsmart.ToLower()).Select(it => int.Parse(it.GetAttributeValue<string>("ainos_crmvalue"))).ToList();
                
            if(toReturn.Count == 0)
                throw new DataValueError(string.Format("Mapping with value '{0}' is unknow for the field {1}", valueZsmart, fieldCRM));
            else if(toReturn.Count > 1)
                throw new DuplicateReferenceFoundError(string.Format("Multiple mapping with value '{0}' is present for the field {1}", valueZsmart, fieldCRM));

            return new OptionSetValue(toReturn[0]);
        }
    }
}
