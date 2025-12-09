using System;
using Microsoft.AspNetCore.Mvc;
using Sensore.Models;
using System.Collections.Generic;
using Sensore.Data;     
using System.Linq;      




namespace Sensore.Controllers
{

    public class ClinicianController : Controller
    {
        private readonly AppDBContext _context;

        public ClinicianController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            var vm = new DashboardViewModel
            {
                CriticalAlertCount = 2,
                HighRiskPatientsCount = 2,
                NewAlertsCount = 3,
                RecentCriticalAlerts = new List<AlertViewModel>
                {
                    new AlertViewModel { Id="A1", PatientName="John Smith", Zone="Zone 5", HighestPressure=95, DetectedAt=DateTime.Now.AddMinutes(-15), Duration=TimeSpan.FromMinutes(15) },
                    new AlertViewModel { Id="A2", PatientName="Emily Davis", Zone="Zone 6", HighestPressure=88, DetectedAt=DateTime.Now.AddMinutes(-30), Duration=TimeSpan.FromMinutes(30) }
                },
                HighRiskPatients = new List<PatientRiskViewModel>
                {
                    new PatientRiskViewModel { PatientId="P1", Patient="John Smith", Room="5", RiskLevel="High", HighestPressure=95, ActiveAlerts=1 },
                    new PatientRiskViewModel { PatientId="P2", Patient="Emily Davis", Room="6", RiskLevel="High", HighestPressure=88, ActiveAlerts=2 }
                }
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Acknowledge(string id)
        {
            TempData["Toast"] = $"Alert {id} acknowledged.";
            return RedirectToAction("Dashboard");
        }

        public IActionResult SystemAlerts()
        {
            var vm = new SystemAlertViewModel
            {
                CriticalCount = 2,
                WarningCount = 2,
                NewCount = 3,
                AcknowledgedCount = 2,
                Alerts = new List<AlertItem>
                {
                    new AlertItem
                    {
                        Id = "P001",
                        PatientName = "John Smith",
                        Zone = "Zone 5",
                        PressureReading = 95,
                        DetectedAt = DateTime.Today.AddHours(14).AddMinutes(30),
                        Duration = TimeSpan.FromMinutes(15),
                        RecommendedActions = new[]
                        {
                            "Immediate patient repositioning required",
                            "Assess skin condition in affected zone",
                            "Consider pressure-relief interventions"
                        },
                        IsNew = true
                    },
                    new AlertItem
                    {
                        Id = "P003",
                        PatientName = "Emily Davis",
                        Zone = "Zone 6",
                        PressureReading = 88,
                        DetectedAt = DateTime.Today.AddHours(14).AddMinutes(15),
                        Duration = TimeSpan.FromMinutes(30),
                        RecommendedActions = new[]
                        {
                            "Review patient positioning",
                            "Reassess risk and notify nursing staff"
                        },
                        IsNew = true
                    }
                }
            };

            return View(vm);
        }

        public IActionResult Patients()
        {
            return View();
        }

        public IActionResult Analytics()
        {
            return View();
        }

        public IActionResult Communication()
        {
            var comments = _context.Comments
                .OrderByDescending(c => c.CreatedAt)
                .ToList();

            return View(comments);
        }
        [HttpPost]
        public IActionResult Reply(int id, string replyText)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
            if (comment == null)
                return NotFound();

            comment.ClinicianReply = replyText;
            comment.ReplyAt = DateTime.UtcNow;
            _context.SaveChanges();

            TempData["Toast"] = "Reply sent successfully!";
            return RedirectToAction("Communication");
        }


        public IActionResult Report()
        {
            var vm = new ClinicalReportViewModel
            {
                PatientName = "John Smith",
                PatientId = "P001",
                Age = 68,
                Room = "201A",
                RiskLevel = "High",
                ActiveAlerts = 1,
                LastReading = "2 mins ago",
                ReportPeriod = "24 Hours",
                PeakPressure = 95,
                ContactArea = 30.5,
                PPI = 1.82,
                PostureChanges = 38
            };

            return View(vm);
        }

    }
}
    
    
    
    



