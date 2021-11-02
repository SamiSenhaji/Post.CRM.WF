using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.CRM.WF.HELPER.ZSMART
{
    /// <summary>
    /// Values of the global Optionset "ZSMART Entity Type" on D365
    /// </summary>
    public enum EntityType
    {
        Account = 192400000,
        Contact = 192400001,
        Opportunity = 192400002,
        Quote = 192400003,
        Order = 192400004,
    }
    /// <summary>
    /// Values of the global Optionset "ZSMART Operation" on D365
    /// </summary>
    public enum Operation
    {
        Create = 192400000,
        Update = 192400001,
        Delete = 192400002
    }
    /// <summary>
    /// Values of the global Optionset "ZSMART Transaction status" on D365
    /// </summary>
    public enum TransactionStatus
    {
        New = 192400000,
        Success = 192400001,
        Error = 192400002
    }

    /// <summary>
    /// Values of the global Optionset "ZSMART Error Type" on D365
    /// </summary>
    public enum ErrorType
    {
        JSONSyntaxError = 192400000,
        WrongDataType = 192400001,
        DuplicateReferenceFound = 192400002,
        WrongDataValue = 192400003,
        ReferenceNotFound = 192400004,
        JSONEmpty = 192400005,
        UNKNOWERROR = 192400006,
        MissingValue = 192400007
    }
}
