using System.Collections.Generic;

namespace FiscalNg.Data.Invoices {
    /// <summary>
    /// Validator for POST /RegisterTransaction request parameters
    /// </summary>
    public class RegisterTransactionDto {
        /// <summary>
        /// Invoice sum
        /// </summary>
        public decimal InvoiceSum { get; set; }
        /// <summary>
        /// Boolean value indicating if transaction is refund action
        /// </summary>
        public bool Refund { get; set; }
        /// <summary>
        /// A javascript object. The key - tax value. The value - the sum e.g 300.50NGN split between 180.50NGN VAT 7.5% and 100.00 for VAT 5% will do
        /// </summary>
        public Dictionary<string, int> VatRateToSum { get; set; }
        /// <summary>
        /// Autoincrement receipt number within one Registration Unit
        /// </summary>
        public int ReceiptNumber { get; set; }
        /// <summary>
        /// Current date, amount of milliseconds since UNIX epoch
        /// </summary>
        public long Date { get; set; }
    }
}
