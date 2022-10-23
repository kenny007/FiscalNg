using System.Threading.Tasks;
using FiscalNg.Core.Services;
using FiscalNg.Data.Invoices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiscalNg.Api.Controllers {
    /// <summary>
    /// Register Transaction Controller
    /// </summary>
    [Authorize]
    [Route("RegisterTransaction")]
    public class RegisterTransactionController : Controller {
        private readonly IRegisterTransactionService _registerTransactionService;
        public RegisterTransactionController(IRegisterTransactionService registerTransactionService) {
            _registerTransactionService = registerTransactionService;
        }

        [HttpPost("RegisterTransaction")]
        public async Task<IActionResult> PostTransaction(RegisterTransactionDto parameters) {
            var result = await _registerTransactionService.SetTransactionAsync(parameters);
            return Json(result);
        }
    }
}