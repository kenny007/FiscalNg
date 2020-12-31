
using System.Threading.Tasks;
using FiscalNg.Data.Invoices;

namespace FiscalNg.Data.Repositories.RegisterTransaction {
    /// <summary>
    /// Merchants Transaction Repository
    /// </summary>
    public interface IRegisterTransactionRepository {
        /// <summary>
        /// POST merchant transaction
        /// </summary>
        /// <param name="registerTransaction"></param>
        /// <returns></returns>
        Task<RegisterTransactionDto> SetTransactionAsync(RegisterTransactionDto registerTransaction);
    }
}
