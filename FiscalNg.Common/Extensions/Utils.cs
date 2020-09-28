namespace FiscalNg.Common.Extensions {
    /// <summary>
    /// Class for utility methods
    /// </summary>
    public static class Utils {

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}
