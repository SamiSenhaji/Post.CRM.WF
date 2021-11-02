using Microsoft.Xrm.Sdk;
using Post.CRM.WF.MODEL.SAP;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace Post.SAP.Webapp.Logics.Helpers
{
    public class GenericSerializerHelper
    {

        #region XML
       
        protected T ConvertToXml<T>(string inputXML)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
             // A MemoryStream is needed to read the XML data.
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(inputXML));
         
            T ret = (T)serializer.Deserialize(ms);
            return ret;
        }

        protected string ConvertToXml<T>(T inputXML)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            serializer.Serialize(ms, inputXML);
            ms.Position = 0;
            string res = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            ms.Dispose();
            return res;
        }



        public string ConvertTo<T>(T retData)
        {
            return ConvertToXml(retData);
        }

        public T ConvertTo<T>(string rawXml)
        {
            return ConvertToXml<T>(rawXml);
        }
        #endregion

        #region Json
        public quote_ ConvertToQuoteObject(string json)
        {
            quote_ jsonData = new quote_();
            byte[] result = Encoding.UTF8.GetBytes(json);
            using (var jsonReader = JsonReaderWriterFactory.CreateJsonReader(result, XmlDictionaryReaderQuotas.Max))
            {
                var outputSerialiser = new DataContractJsonSerializer(jsonData.GetType());
                jsonData = outputSerialiser.ReadObject(jsonReader) as quote_;
            }
            return jsonData;
        }

        #endregion
    }
}