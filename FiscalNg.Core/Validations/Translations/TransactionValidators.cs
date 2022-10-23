using FiscalNg.Core.Validations.Base;
using FiscalNg.Data.Invoices;
using FluentValidation;

namespace FiscalNg.Core.Validations.Translations {
    /// <summary>
    /// Validator for POST /RegisterTransaction request parameters
    /// </summary>
    public class RegisterTransactionValidators : BaseValidator<RegisterTransactionDto> {
        public RegisterTransactionValidators() {
            RuleFor(x => x.InvoiceSum).GreaterThanOrEqualTo(0);
            RuleFor(x => x.ReceiptNumber).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
        }
    }
}
