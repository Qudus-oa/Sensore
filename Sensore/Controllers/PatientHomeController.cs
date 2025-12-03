using Microsoft.AspNetCore.Mvc;

namespace Sensore.Controllers
{
    public class PatientHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
