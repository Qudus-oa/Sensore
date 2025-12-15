using Microsoft.AspNetCore.Mvc;

namespace Sensore.Controllers
{
    public class ClinicianHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
