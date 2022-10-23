using System;
using System.Threading.Tasks;
using FiscalNg.Data.Invoices;
using FiscalNg.Data.Repositories.RegisterTransaction;

namespace FiscalNg.Core.Services {
    /// <inheritdoc cref="IRegisterTransactionService" />
    public class RegisterTransactionService: IRegisterTransactionService {
        #region DI

        private readonly IRegisterTransactionRepository _registerTransactionRepository;
        #endregion
        public RegisterTransactionService(IRegisterTransactionRepository registerTransactionRepository) {
            _registerTransactionRepository = registerTransactionRepository;
        }

        public async Task<RegisterTransactionDto> SetTransactionAsync(RegisterTransactionDto parameters) {
            return  await _registerTransactionRepository.SetTransactionAsync(parameters);
        }
    }
}
