using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Post.CRM.WF.MODEL.ZSMART
{
    [DataContract]
    public class ZSmartContact
    {
        [DataMember(Name = "CONTACT_ID")]
        public string zSmartId;
        [DataMember(Name = "PRENOM")]
        public string firstName;
        [DataMember(Name = "NOM")]
        public string lastName;
        [DataMember(Name = "CUST_CODE")]
        public string accountZSmartId;

        [DataMember(Name = "ACCOUNT_MANAGER_NAME")]
        public string owner;
        [DataMember(Name = "CONTACT_LEVEL")]
        public string contactLevel;
        [DataMember(Name = "CON_MAN_TYPE")]
        public string contactType;
        [DataMember(Name = "MARQ")]
        public string makers;
        [DataMember(Name = "CIVILITE")]
        public string civility;
        [DataMember(Name = "GENDER")]
        public string gender;
        [DataMember(Name = "TITRE")]
        public string title;
        [DataMember(Name = "TELEPHONE")]
        public string businessPhone;
        [DataMember(Name = "HOME")]
        public string homePhone;
        [DataMember(Name = "MOBILE")]
        public string mobilePhone;
        [DataMember(Name = "FAX")]
        public string fax;
        [DataMember(Name = "EMAIL")]
        public string email;
        [DataMember(Name = "NATIONALITY")]
        public string nationality;
        [DataMember(Name = "LANGUE")]
        public string language;

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
    }
}
