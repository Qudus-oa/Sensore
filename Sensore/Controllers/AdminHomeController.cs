using Microsoft.AspNetCore.Mvc;

namespace Sensore.Controllers
{
    public class AdminHomeController : Controller
    {
        // Admin Dashboard
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Redirect to Manage Users page
        [HttpGet]
        public IActionResult GoToManageUsers()
        {
            return RedirectToAction("Index", "ManageUsers");
        }
    }
}
