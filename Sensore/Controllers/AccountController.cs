using Microsoft.AspNetCore.Mvc;
using Sensore.Data;
using Sensore.Models;
using System;

namespace Sensore.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDBContext appDBContext;

        public AccountController(AppDBContext context)
        {
            appDBContext = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(User users)
        {
            if (!ModelState.IsValid)
                return View(users);

            users.CreatedDate = DateTime.Now;

            appDBContext.users.Add(users);
            appDBContext.SaveChanges();

            return users.Type switch
            {
                UserType.Admin => RedirectToAction("Index", "AdminHome"),
                UserType.Clinician => RedirectToAction("Index", "ClinicianHome"),
                UserType.Patient => RedirectToAction("Index", "PatientHome"),
                _ => View(users)
            };
        }
        [HttpGet]
        public IActionResult Logout()
        {
            // for session or authentication

            return RedirectToAction("Index", "Account");
        }

    }
}
