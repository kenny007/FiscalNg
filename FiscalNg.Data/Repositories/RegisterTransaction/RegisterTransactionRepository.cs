using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using FiscalNg.Data.Invoices;

namespace FiscalNg.Data.Repositories.RegisterTransaction {
    /// <inheritdoc cref="IRegisterTransactionRepository" />
    public class RegisterTransactionRepository : IRegisterTransactionRepository {
        public RegisterTransactionRepository() { }
        /// <inheritdoc />
        public async Task<RegisterTransactionDto> SetTransactionAsync(RegisterTransactionDto registerTransaction) {
            const string sql = @"INSERT INTO Invoices (
                                            InvoiceSum, 
                                            Refund, 
                                            ReceiptNumber, 
                                            Date) 
                                        VALUES(
                                            @invoiceSum 
                                            ,@refund
                                            ,@receiptNumber 
                                            ,@transactionDate
                                            );
                                         SELECT InvoiceSum,
                                                Refund,
                                                ReceiptNumber,
                                                Date
                                         FROM Invoices";

            var dynamics = new DynamicParameters();
            dynamics.Add("@invoiceSum", registerTransaction.InvoiceSum);
            dynamics.Add("@refund", registerTransaction.Refund);
            dynamics.Add("@receiptNumber", registerTransaction.ReceiptNumber);
            dynamics.Add("@transactionDate", registerTransaction.Date);
            using (var connection = new SqlConnection("Placeholder")) {
               return await connection.QueryFirstOrDefaultAsync<RegisterTransactionDto>(sql, dynamics);
            }
        }
    }
}
