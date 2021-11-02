using Post.CRM.WF.MODEL.SAP.Opportunities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Collections.Specialized;

namespace Post.CRM.WF.HELPER.SAP
{
    public class SAPTransaction_VALIDATOR
    {
        private int _number = 0;
        /// <summary>
        /// Tracing service used to store log on D365
        /// </summary>
        private List<Tuple<int, ErrorType, Exception>> _validationErrors = null;

        private Tuple<TransactionStatus, string, List<Tuple<int, ErrorType, Exception>>> _validationResult;

        /// <summary>
        /// Constructor of the class SAPTransaction_VALIDATOR
        /// </summary>
        public SAPTransaction_VALIDATOR()
        {
            _validationErrors = new List<Tuple<int, ErrorType, Exception>>();
            _validationResult = new Tuple<TransactionStatus, string, List<Tuple<int, ErrorType, Exception>>>(TransactionStatus.Success, "Success : No validation errors.", _validationErrors);

        }
        private bool IsValidDataValue(string data, string dataName, string value = "")
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(data))
            {
                isValid &= false;
                _validationErrors.Add(Tuple.Create(_number++, ErrorType.MissingValue, (Exception)new MissingDataError(@"Requested XML Data &lt;" + dataName + "&gt; node is mandatory!")));
            }
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (!data.Equals(value))
                {
                    isValid &= false;
                    _validationErrors.Add(Tuple.Create(_number++, ErrorType.WrongDataValue, (Exception)new DataValueError(@"Requested XML Data &lt;" + dataName + "&gt; value must be (" + value + ") ! ")));
                }
            }
            return isValid;

        }
        private bool IsValidDataValue(object data, Type dataType, string value = "")
        {
            bool isValid = true;
            if (data == null)
            {
                isValid &= false;
                _validationErrors.Add(Tuple.Create(_number++, ErrorType.MissingValue, (Exception)new MissingDataError(@"Requested XML Data &lt;" + dataType.Name + "&gt; node is mandatory!")));
            }
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (!data.Equals(value))
                {
                    isValid &= false;
                    _validationErrors.Add(Tuple.Create(_number++, ErrorType.WrongDataValue, (Exception)new DataValueError(@"Requested XML Data &lt;" + dataType.Name + "&gt; value must be (" + value+") ! ")));
                }
            }
            return isValid;
        }

        private bool IsValidDataSize(string data, string dataName, int fieldLength = 0)
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(data))
            {
                isValid &= false;
                _validationErrors.Add(Tuple.Create(_number++, ErrorType.MissingValue, (Exception)new MissingDataError(@"Requested XML Data &lt;" + dataName + "&gt; node is mandatory!")));
            }
            if (fieldLength > 0 && data.Length > fieldLength )
            {
                isValid &= false;
                _validationErrors.Add(Tuple.Create(_number++, ErrorType.WrongDataValue, (Exception)new DataValueError(@"Requested XML Data &lt;" + dataName + "&gt; value must not exced (" + fieldLength + ") char length ! ")));
                
            }
            return isValid;
        }

        private bool IsValidDataDateFormat(string data, string dataName, string dateFormat = "yyyyMMdd")
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(data))
            {
                isValid &= false;
                _validationErrors.Add(Tuple.Create(_number++, ErrorType.MissingValue, (Exception)new MissingDataError(@"Requested XML Data &lt;" + dataName + "&gt; node is mandatory!")));
            }
            DateTime date = DateTime.MinValue;
            CultureInfo frFR = new CultureInfo("fr-FR");
            if (!string.IsNullOrWhiteSpace(data) && DateTime.TryParseExact(data, dateFormat, frFR, DateTimeStyles.None, out date));
            {
                isValid &= false;
                _validationErrors.Add(Tuple.Create(_number++, ErrorType.WrongDataValue, (Exception)new DataValueError(@"Requested XML Data &lt;" + dataName + "&gt; must be date (" + dateFormat + ") formated ! ")));

            }
            return isValid;
        }
        /// <summary>
        /// Validate an SAP object
        /// </summary>
        public bool Validate(INQUIRY_CREATEFROMDATA201 sapObject)
        {
            bool isValid = true;

            if (sapObject != null)
            {
                if (sapObject.IDOC == null)
                {
                    isValid &= IsValidDataValue(sapObject.IDOC, typeof(IDOC));
                }
                else if (sapObject.IDOC.EDI_DC40 == null)
                {
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40, typeof(EDI_DC40));
                }
                else
                {
                    // Message unique id	Incremental value (Must be unique)
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.DOCNUM, "DOCNUM");
                    // Fix value: INQUIRY_CREATEFROMDATA201
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.IDOCTYP, "IDOCTYP", "INQUIRY_CREATEFROMDATA201");
                    // Fix value: INQUIRY_CREATEFROMDATA2
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.MESTYP, "MESTYP", "INQUIRY_CREATEFROMDATA2");
                    // Fix value: 	Final value: D365
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.SNDPOR, "SNDPOR", "D365");
                    // Fix value   Final value: LS
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.SNDPRT, "SNDPRT", "LS");   // final value                               
                    // Fix value: 	Final value: D365
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.SNDPRN, "SNDPRN", "D365");
                    // Fix value   Final value: LS
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.RCVPRT, "RCVPRT", "LS");
                    // Fix value: 	Final value: D365
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.RCVPRN, "RCVPRN", "D365");

                    isValid &= IsValidDataValue(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2, typeof(E1INQUIRY_CREATEFROMDATA2));
                    isValid &= IsValidDataValue(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1, typeof(E1BPSDHD1));
                    isValid &= IsValidDataValue(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1X, typeof(E1BPSDHD1X));
                    if (IsValidDataValue(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1X.DOC_TYPE, "DOC_TYPE", "X"))
                    {
                        isValid &= IsValidDataValue(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1.DOC_TYPE, "DOC_TYPE", "ZDOF"); 
                    }
                    if (IsValidDataValue(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1X.SALES_ORG, "SALES_ORG", "X"))
                    {
                        isValid &= IsValidDataValue(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1.SALES_ORG, "SALES_ORG", "2000");
                    }
                    if (IsValidDataValue(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1X.DISTR_CHAN, "DISTR_CHAN", "X"))
                    {
                        isValid &= IsValidDataValue(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1.DISTR_CHAN, "DISTR_CHAN", "10");
                    }
                    if (IsValidDataValue(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1X.DIVISION, "DIVISION", "X"))
                    {
                        isValid &= IsValidDataValue(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1.DIVISION, "DIVISION", "10");
                    }
                    if (IsValidDataValue(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1X.SALES_OFF, "SALES_OFF", "X"))
                    {
                        isValid &= IsValidDataValue(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1.SALES_OFF, "SALES_OFF", "100");
                    }
                    if (IsValidDataValue(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1X.REQ_DATE_H, "REQ_DATE_H", "X"))
                    {
                        isValid &= IsValidDataSize(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1.REQ_DATE_H, "REQ_DATE_H", 8);
                        isValid &= IsValidDataDateFormat(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1.REQ_DATE_H, "REQ_DATE_H", "YYYMMDD");
                    }
                    if (IsValidDataValue(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1X.NAME, "NAME", "X"))
                    {
                        isValid &= IsValidDataValue(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1.NAME, "NAME");
                        isValid &= IsValidDataSize(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1.NAME, "NAME", 35);
                    }

                    if (IsValidDataValue(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1X.PURCH_NO_C, "PURCH_NO_C", "X"))
                    {
                        isValid &= IsValidDataValue(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1.PURCH_NO_C, "PURCH_NO_C");
                        isValid &= IsValidDataSize(sapObject.IDOC.E1INQUIRY_CREATEFROMDATA2.E1BPSDHD1.PURCH_NO_C, "PURCH_NO_C", 35);
                    }

                }
            }
            else
            {
                isValid &= false;
                _validationErrors.Add(Tuple.Create(_number++, ErrorType.DATAEmpty, (Exception)new MissingDataError("Request XML Data could not be empty!")));
            }


            if (isValid)
            {
                _validationResult = Tuple.Create(TransactionStatus.Success, "No error during data validation process.", _validationErrors);
            }
            else
            {
                _validationResult = Tuple.Create(TransactionStatus.Error, "Error during data validation process...", _validationErrors);
            }
            return isValid;
        }
        public bool Validate(MODEL.SAP.Orders.ORDERS05 sapObject)
        {
            bool isValid = true;

            if (sapObject != null)
            {
                if (sapObject.IDOC == null)
                {
                    isValid &= IsValidDataValue(sapObject.IDOC, typeof(IDOC));
                }
                else if (sapObject.IDOC.EDI_DC40 == null)
                {
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40, typeof(EDI_DC40));
                }
                else
                {
                    // Message unique id	Incremental value (Must be unique)
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.DOCNUM, "DOCNUM");
                    // Fix value: INQUIRY_CREATEFROMDATA201
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.IDOCTYP, "IDOCTYP", "INQUIRY_CREATEFROMDATA201");
                    // Fix value: INQUIRY_CREATEFROMDATA2
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.MESTYP, "MESTYP", "INQUIRY_CREATEFROMDATA2");
                    // Fix value: 	Final value: D365
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.SNDPOR, "SNDPOR", "D365");
                    // Fix value   Final value: LS
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.SNDPRT, "SNDPRT", "LS");   // final value                               
                    // Fix value: 	Final value: D365
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.SNDPRN, "SNDPRN", "D365");
                    // Fix value   Final value: LS
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.RCVPRT, "RCVPRT", "LS");
                    // Fix value: 	Final value: D365
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.RCVPRN, "RCVPRN", "D365");

                }
            }
            else
            {
                isValid &= false;
                _validationErrors.Add(Tuple.Create(_number++, ErrorType.DATAEmpty, (Exception)new MissingDataError("Request XML Data could not be empty!")));
            }


            if (isValid)
            {
                _validationResult = Tuple.Create(TransactionStatus.Success, "No error during data validation process.", _validationErrors);
            }
            else
            {
                _validationResult = Tuple.Create(TransactionStatus.Error, "Error during data validation process...", _validationErrors);
            }
            return isValid;
        }

        public bool Validate(MODEL.SAP.Quotes.ORDERS05 sapObject)
        {
            bool isValid = true;

            if (sapObject != null)
            {
                if (sapObject.IDOC == null)
                {
                    isValid &= IsValidDataValue(sapObject.IDOC, typeof(IDOC));
                }
                else if (sapObject.IDOC.EDI_DC40 == null)
                {
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40, typeof(EDI_DC40));
                }
                else
                {
                    // Message unique id	Incremental value (Must be unique)
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.DOCNUM, "DOCNUM");
                    // Fix value: INQUIRY_CREATEFROMDATA201
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.IDOCTYP, "IDOCTYP", "INQUIRY_CREATEFROMDATA201");
                    // Fix value: INQUIRY_CREATEFROMDATA2
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.MESTYP, "MESTYP", "INQUIRY_CREATEFROMDATA2");
                    // Fix value: 	Final value: D365
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.SNDPOR, "SNDPOR", "D365");
                    // Fix value   Final value: LS
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.SNDPRT, "SNDPRT", "LS");   // final value                               
                    // Fix value: 	Final value: D365
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.SNDPRN, "SNDPRN", "D365");
                    // Fix value   Final value: LS
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.RCVPRT, "RCVPRT", "LS");
                    // Fix value: 	Final value: D365
                    isValid &= IsValidDataValue(sapObject.IDOC.EDI_DC40.RCVPRN, "RCVPRN", "D365");

                }
            }
            else
            {
                isValid &= false;
                _validationErrors.Add(Tuple.Create(_number++, ErrorType.DATAEmpty, (Exception)new MissingDataError("Request XML Data could not be empty!")));
            }


            if (isValid)
            {
                _validationResult = Tuple.Create(TransactionStatus.Success, "No error during data validation process.", _validationErrors);
            }
            else
            {
                _validationResult = Tuple.Create(TransactionStatus.Error, "Error during data validation process...", _validationErrors);
            }
            return isValid;
        }

        public override string ToString()
        {
            string validationResult = @"<strong>Validation result report:</strong> <br/><br/>
                                        <strong>Status - </strong>" + ((TransactionStatus)_validationResult.Item1).ToString().ToUpper() +
                                        "<br/><strong>Message - </strong>" + _validationResult.Item2 + "<br/>";
            if (_validationResult.Item3.Count > 0)
            {
                validationResult += @"<strong>Errors Details : </strong><br/>";
                foreach (var errors in (List<Tuple<int, ErrorType, Exception>>)_validationResult.Item3)
                {
                    validationResult += errors.Item2.ToString().ToUpper() + " - " + errors.Item3.Message + " : <br/>" + errors.Item3.StackTrace + "<br/>";
                }
            }

            return validationResult;
        }
    }
}
