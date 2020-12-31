using System.Threading.Tasks;
using FiscalNg.Data.Invoices;

namespace FiscalNg.Core.Services {
    /// <summary>
    /// Register Transaction Service
    /// </summary>
    public interface IRegisterTransactionService {
        /// <summary>
        /// POST Merchant Transaction
        /// </summary>
        /// <param name="registerTransaction"></param>
        /// <returns></returns>
        Task<RegisterTransactionDto> SetTransactionAsync(RegisterTransactionDto registerTransaction);
    }
}
