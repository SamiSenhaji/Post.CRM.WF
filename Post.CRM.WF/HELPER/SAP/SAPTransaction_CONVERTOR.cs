using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using Post.CRM.WF.MODEL.SAP;
using Post.CRM.WF.MODEL.SAP.Opportunities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Post.CRM.WF.HELPER.SAP
{
    public class SAPTransaction_CONVERTOR
    {
        /// <summary>
        /// Tracing service used to store log on D365
        /// </summary>
        private ITracingService _tracingService;
        /// <summary>
        /// SAP Transaction Mappings Collection
        /// </summary>
        private EntityCollection _mappings;
        /// <summary>
        /// Organisation service used to communicate with D365
        /// </summary>
        private IOrganizationService _service;

        /// <summary>
        /// Constructor of the class SAPTransaction_CONVERTOR
        /// </summary>
        /// <param name="tracingService">Tracing service used to store log on D365</param>
        /// <param name="mappings">SAP Transaction Mappings Collection</param>
        /// <param name="service">Organisation service used to communicate with D365</param>
        public SAPTransaction_CONVERTOR(ITracingService tracingService, EntityCollection mappings, IOrganizationService service)
        {
            _tracingService = tracingService;
            _mappings = mappings;
            _service = service;
        }

        /// <summary>
        /// Convert a SAPAccount to an D365 Entity
        /// </summary>
        /// <param name="sap_opportunity">SAP Account to convert</param>
        /// <param name="defaultOwnerRef">Default Owner if field account manager is not filled</param>
        /// <returns>the SAPAccount converted to a D365 Entity</returns>
        public Entity convertSapOpportunityToEntity(INQUIRY_CREATEFROMDATA201 sap_opportunity, EntityReference defaultOwnerRef)
        {
            if (string.IsNullOrWhiteSpace(sap_opportunity.IDOC.EDI_DC40.DOCNUM))
                throw new MissingDataError("DOCNUM is empty");

            Entity e_opportunity = new Entity("opportunity");
            e_opportunity.Attributes.Add("ainos_mastersapreference", sap_opportunity.IDOC.EDI_DC40.DOCNUM);

            if (!string.IsNullOrWhiteSpace(sap_opportunity.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1.NAME))
                e_opportunity.Attributes.Add("name", sap_opportunity.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1.NAME);
            /*
            string address1_line1 = "";
            if (!string.IsNullOrWhiteSpace(sap_opportunity.streetName))
                address1_line1 += sap_opportunity.streetName;
            if (!string.IsNullOrWhiteSpace(sap_opportunity.streetNumber))
                address1_line1 += " " + sap_opportunity.streetNumber;
            if (!string.IsNullOrWhiteSpace(address1_line1))
                e_opportunity.Attributes.Add("address1_line1", address1_line1);

            if (!string.IsNullOrWhiteSpace(sap_opportunity.zipCode))
                e_opportunity.Attributes.Add("address1_postalcode", sap_opportunity.zipCode);

            if (!string.IsNullOrWhiteSpace(sap_opportunity.city))
                e_opportunity.Attributes.Add("address1_city", sap_opportunity.city);

            if (!string.IsNullOrWhiteSpace(sap_opportunity.country))
                e_opportunity.Attributes.Add("ainos_address1_countryid", retrieveEntityWithValue("ainos_country", "ainos_countryid", "ainos_name", sap_opportunity.country));

            e_opportunity.Attributes.Add("ownerid", defaultOwnerRef);
            if (!string.IsNullOrWhiteSpace(sap_opportunity.accountManager))
            {
                EntityReference ownerId = retrieveEntityWithValue("systemuser", "systemuserid", "fullname", sap_opportunity.accountManager, false);
                if (ownerId != null) e_opportunity.Attributes["ownerid"] = ownerId;
            }
            if (!string.IsNullOrWhiteSpace(sap_opportunity.secondAccountManager))
            {
                EntityReference secondOwnerId = retrieveEntityWithValue("systemuser", "systemuserid", "fullname", sap_opportunity.secondAccountManager, false);
                if (secondOwnerId != null) e_opportunity.Attributes.Add("ainos_secondaccountmanagerid", secondOwnerId);
            }
            if (!string.IsNullOrWhiteSpace(sap_opportunity.creditScore))
                e_opportunity.Attributes.Add("ainos_creditscore", getMappings("ainos_creditscore", sap_opportunity.creditScore));
            if (!string.IsNullOrWhiteSpace(sap_opportunity.legalStatus))
                e_opportunity.Attributes.Add("ainos_legalstatus", getMappings("ainos_legalstatus", sap_opportunity.legalStatus));
            if (!string.IsNullOrWhiteSpace(sap_opportunity.language))
                e_opportunity.Attributes.Add("ainos_language", getMappings("ainos_language", sap_opportunity.language));
            if (!string.IsNullOrWhiteSpace(sap_opportunity.mainPhone))
                e_opportunity.Attributes.Add("telephone1", sap_opportunity.mainPhone);
            if (!string.IsNullOrWhiteSpace(sap_opportunity.mainEmail))
            {
                string emailaddress1, emailaddress2, emailaddress3, otheremails;
                splitEmails(sap_opportunity.mainEmail, out emailaddress1, out emailaddress2, out emailaddress3, out otheremails);

                if (!string.IsNullOrWhiteSpace(emailaddress1)) e_opportunity.Attributes.Add("emailaddress1", emailaddress1);
                if (!string.IsNullOrWhiteSpace(emailaddress2)) e_opportunity.Attributes.Add("emailaddress2", emailaddress2);
                if (!string.IsNullOrWhiteSpace(emailaddress3)) e_opportunity.Attributes.Add("emailaddress3", emailaddress3);
                if (!string.IsNullOrWhiteSpace(otheremails)) e_opportunity.Attributes.Add("ainos_otheremails", otheremails);
            }
            if (!string.IsNullOrWhiteSpace(sap_opportunity.mainFax))
                e_opportunity.Attributes.Add("fax", sap_opportunity.mainFax);
            if (!string.IsNullOrWhiteSpace(sap_opportunity.vat))
                e_opportunity.Attributes.Add("ainos_vat", sap_opportunity.vat);
            if (!string.IsNullOrWhiteSpace(sap_opportunity.segmentation))
                e_opportunity.Attributes.Add("ainos_segmentation", getMappings("ainos_segmentation", sap_opportunity.segmentation));
            if (!string.IsNullOrWhiteSpace(sap_opportunity.SAPStatus))
                e_opportunity.Attributes.Add("ainos_SAPstatus", getMappings("ainos_SAPstatus", sap_opportunity.SAPStatus));
            if (!string.IsNullOrWhiteSpace(sap_opportunity.SAPCustomerType))
                e_opportunity.Attributes.Add("ainos_customertype", getMappings("ainos_customertype", sap_opportunity.SAPCustomerType));
            if (!string.IsNullOrWhiteSpace(sap_opportunity.birthdate))
            {
                DateTime dt_birthdate;
                if (!DateTime.TryParse(sap_opportunity.birthdate, CultureInfo.GetCultureInfo("fr-FR"), DateTimeStyles.None, out dt_birthdate))
                    throw new DataTypeError("BIRTHDAY_DAY is not a date");
                e_opportunity.Attributes.Add("ainos_birthdaydate", dt_birthdate);
            }
            if (!string.IsNullOrWhiteSpace(sap_opportunity.createdon))
            {
                DateTime dt_createdon;
                if (!DateTime.TryParse(sap_opportunity.createdon, CultureInfo.GetCultureInfo("fr-FR"), DateTimeStyles.None, out dt_createdon))
                    throw new DataTypeError("CREATED_DATE is not a date");
                e_opportunity.Attributes.Add("overriddencreatedon", dt_createdon);
            }
            if (!string.IsNullOrWhiteSpace(sap_opportunity.psf))
            {
                bool b_psf = getBooleanValue(sap_opportunity.psf);
                e_opportunity.Attributes.Add("ainos_psf", b_psf);
            }
            if (!string.IsNullOrWhiteSpace(sap_opportunity.documentType))
                e_opportunity.Attributes.Add("ainos_documenttype", getMappings("ainos_documenttype", sap_opportunity.documentType));
            if (!string.IsNullOrWhiteSpace(sap_opportunity.documentNumber))
                e_opportunity.Attributes.Add("ainos_documentnumber", sap_opportunity.documentNumber);
            if (!string.IsNullOrWhiteSpace(sap_opportunity.naceCode))
                e_opportunity.Attributes.Add("ainos_nacecodeid", findOrCreateNaceCode(sap_opportunity.naceCode));
            if (!string.IsNullOrWhiteSpace(sap_opportunity.postSubsidiary))
            {
                bool b_postSubsidiary = getBooleanValue(sap_opportunity.postSubsidiary);
                e_opportunity.Attributes.Add("ainos_postsubsidiary", b_postSubsidiary);
            }
            if (!string.IsNullOrWhiteSpace(sap_opportunity.ainos_customeradvertisement))
            {
                bool b_ainos_customeradvertisement = getBooleanValue(sap_opportunity.ainos_customeradvertisement);
                e_opportunity.Attributes.Add("ainos_customeradvertisement", b_ainos_customeradvertisement);
            }
            if (!string.IsNullOrWhiteSpace(sap_opportunity.ainos_icmsreference))
                e_opportunity.Attributes.Add("ainos_icmsreference", sap_opportunity.ainos_icmsreference);

            
            OptionSetValueCollection osvcRelationShip = new OptionSetValueCollection();
            OptionSetValue osv_customer = new OptionSetValue(192400002);
            osvcRelationShip.Add(osv_customer);
            e_opportunity.Attributes.Add("ainos_mo_relationshiptype", osvcRelationShip);
            */
            return e_opportunity;
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


        public Entity convertSapQuoteToEntity(quote_ sap_quote, EntityReference defaultOwnerRef)
        {
            if (string.IsNullOrWhiteSpace(sap_quote.ORDERS05.IDOC.EDI_DC40.DOCNUM.ToString()))
                throw new MissingDataError("DOCNUM is empty");

            Entity quote = new Entity("quote");
            quote.Attributes.Add("ainos_mastersapreference", sap_quote.ORDERS05.IDOC.EDI_DC40.DOCNUM.ToString());
            //Add other attribute here to complete the quote entity
            return quote;

        }



        /// <summary>
        /// Convert a string to boolean
        /// </summary>
        /// <param name="s">string to convert</param>
        /// <returns>the string converted to a boolean</returns>
        private bool getBooleanValue(string s)
        {
            string sLower = s.ToLower();
            bool b_toreturn = sLower == "true" || sLower == "1" || sLower == "yes" || sLower == "oui";
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
            EntityReference toReturn = null;
            QueryExpression oQuery = new QueryExpression(entity);
            oQuery.ColumnSet = new ColumnSet(entityId);
            oQuery.Criteria = new FilterExpression(LogicalOperator.And);
            oQuery.Criteria.AddCondition(field, ConditionOperator.Equal, value);

            EntityCollection ec_Entities = _service.RetrieveMultiple(oQuery);
            if (ec_Entities == null || ec_Entities.Entities == null || ec_Entities.Entities.Count == 0)
            {
                if (throwException)
                    throw new ReferenceNotFoundError(string.Format("A record with the specified values ({0} : {1}) does not exist in {2} entity", field, value, entity));
            }
            else if (ec_Entities.Entities.Count > 1)
            {
                if (throwException)
                    throw new DuplicateReferenceFoundError(string.Format("Multiple records with the specified values ({0} : {1}) exist in {2} entity", field, value, entity));
            }
            else
            {
                toReturn = ec_Entities.Entities[0].ToEntityReference();
            }
            return toReturn;
        }

        /// <summary>
        /// retrieve a SAP value in the mapping for a specific field
        /// </summary>
        /// <param name="fieldCRM">The field name</param>
        /// <param name="valueSAP">The SAP Value</param>
        /// <returns>The Optionsetvalue found in the mappings collection</returns>
        public OptionSetValue getMappings(string fieldCRM, string valueSAP)
        {
            List<int> toReturn = _mappings.Entities.Where(it => it.GetAttributeValue<string>("ainos_crmfield").ToLower() == fieldCRM.ToLower() && it.GetAttributeValue<string>("ainos_sapvalue").ToLower() == valueSAP.ToLower()).Select(it => int.Parse(it.GetAttributeValue<string>("ainos_crmvalue"))).ToList();

            if (toReturn.Count == 0)
                throw new DataValueError(string.Format("Mapping with value '{0}' is unknow for the field {1}", valueSAP, fieldCRM));
            else if (toReturn.Count > 1)
                throw new DuplicateReferenceFoundError(string.Format("Multiple mapping with value '{0}' is present for the field {1}", valueSAP, fieldCRM));

            return new OptionSetValue(toReturn[0]);
        }
    }
}
