using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Post.CRM.WF.MODEL.ZSMART
{
    [DataContract]
    public class ZSmartAccount
    {
        [DataMember(Name = "CUST_CODE")]
        public string zSmartId;
        [DataMember(Name = "CUST_NAME")]
        public string name;
        [DataMember(Name = "STREET_NAME")]
        public string streetName;
        [DataMember(Name = "STREET_NO")]
        public string streetNumber;
        [DataMember(Name = "ZIP_CODE")]
        public string zipCode;
        [DataMember(Name = "CITY")]
        public string city;
        [DataMember(Name = "COUNTRY")]
        public string country;

        [DataMember(Name = "MANAGER_NAME")]
        public string accountManager;
        [DataMember(Name = "SECOND_MANAGER_NAME")]
        public string secondAccountManager;
        [DataMember(Name = "CREDIT_SCORE")]
        public string creditScore;
        [DataMember(Name = "COMPANY_LEGAL_STATUS")]
        public string legalStatus;
        [DataMember(Name = "CUSTOMER_DEFAULT_LANGUAGE")]
        public string language;
        [DataMember(Name = "CONTACT_PHONE")]
        public string mainPhone;
        [DataMember(Name = "EMAIL")]
        public string mainEmail;
        [DataMember(Name = "FAX_NUMBER")]
        public string mainFax;
        [DataMember(Name = "VAT_NO")]
        public string vat;
        [DataMember(Name = "POST_CUST_SEGMENT")]
        public string segmentation;
        [DataMember(Name = "STATE")]
        public string zSmartStatus;
        [DataMember(Name = "CUSTOMER_TYPE")]
        public string zSmartCustomerType;
        [DataMember(Name = "BIRTHDAY_DAY")]
        public string birthdate;
        [DataMember(Name = "CUSTOMER_PSF")]
        public string psf;
        [DataMember(Name = "DOCUMENT_TYPE")]
        public string documentType;
        [DataMember(Name = "DOCUMENT_NUMBER")]
        public string documentNumber;
        [DataMember(Name = "NACE_CODE")]
        public string naceCode;
        [DataMember(Name = "POST_GROUP_AFFILIATE")]
        public string postSubsidiary;
        [DataMember(Name = "CREATED_DATE")]
        public string createdon;

        [DataMember(Name = "ICMS_IDS")]
        public string ainos_icmsreference;
        [DataMember(Name = "CUSTOMER_ADVERTISEMENT")]
        public string ainos_customeradvertisement;

    }
}
