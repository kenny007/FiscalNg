using System;

namespace FiscalNg.Common.Attributes {
    /// <summary>
    /// Attribute used to mark password strings in order to avoid password leaks into database logs
    /// </summary>
    public class PasswordAttribute : Attribute {

    }
}
