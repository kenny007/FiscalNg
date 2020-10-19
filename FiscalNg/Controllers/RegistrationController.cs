using Microsoft.AspNetCore.Mvc;

namespace FiscalNg.Api.Controllers
{
    public class RegistrationController : Controller {
        public IActionResult Index()
        {
            return View();
        }
    }
}