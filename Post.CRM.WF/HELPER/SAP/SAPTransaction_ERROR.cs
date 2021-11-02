using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using Post.CRM.WF.MODEL;
using System;

namespace Post.CRM.WF.HELPER.SAP
{
    /// <summary>
    /// Custom Error Type "Data Type Error"
    /// Use when the value of a field is not the good type
    /// </summary>
    public class DataTypeError : Exception
    {
        public DataTypeError(string message) : base(message) { }
    }
    /// <summary>
    /// Custom Error Type "Data Value Error"
    /// Use when a Zsmart mapping is not found on D365
    /// </summary>
    public class DataValueError : Exception
    {
        public DataValueError(string message) : base(message) { }
    }
    /// <summary>
    /// Custom Error Type "Missing Data Error"
    /// Use when a required field is not fill on an object
    /// </summary>
    public class MissingDataError : Exception
    {
        public MissingDataError(string message) : base(message) { }
    }
    /// <summary>
    /// Custom Error Type "Reference Not found Error"
    /// Use when a record is not found on D365
    /// </summary>
    public class ReferenceNotFoundError : Exception
    {
        public ReferenceNotFoundError(string message) : base(message) { }
    }
    /// <summary>
    /// Custom Error Type "Duplicate Reference found Error"
    /// Use when a duplicate record is found on D365
    /// </summary>
    public class DuplicateReferenceFoundError : Exception
    {
        public DuplicateReferenceFoundError(string message) : base(message) { }
    }
}
