using Microsoft.AspNetCore.Mvc;

namespace Sensore.Controllers
{
    public class Loginpage : Controller
    {

       
        [HttpGet]
        public IActionResult PatientClinicianLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PatientClinicianLogin(string userId, string password, string userType)
        {
            if (userType == "Patient" && userId == "patient" && password == "1234")
            {
                return RedirectToAction("Index", "PatientHome");
            }
            else if (userType == "Clinician" && userId == "clinician" && password == "1234")
            {
                return RedirectToAction("Index", "ClinicianHome");
            }

            ViewBag.ErrorMessage = "Invalid credentials. Please try again.";
            return View();
        }

        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminLogin(string adminId, string password)
        {
            // Simulated admin login logic
            if (adminId == "admin" && password == "admin123")
            {
                // ✅ Redirect Admin to their dashboard
                return RedirectToAction("Index", "AdminHome");
            }
            // ❌ Invalid login
            ViewBag.ErrorMessage = "Invalid admin credentials. Please try again.";
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Clear any saved session data or authentication cookies
            HttpContext.Session.Clear();

            // Redirect to login page
            return RedirectToAction("PatientClinicianLogin", "Loginpage");
        }
    }
}