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

        //  Inject database context so we can access the Comments table and other data models
        public ClinicianController(AppDBContext context)
        {
            _context = context;
        }

        //  Clinician dashboard displaying key metrics and recent critical alerts
        public IActionResult Dashboard()
        {
            var vm = new DashboardViewModel
            {
                // Dashboard summary counts
                CriticalAlertCount = 2,
                HighRiskPatientsCount = 2,
                NewAlertsCount = 3,

                // Recent critical alerts
                RecentCriticalAlerts = new List<AlertViewModel>
                {
                    new AlertViewModel { Id="A1", PatientName="John Smith", Zone="Zone 5", HighestPressure=95, DetectedAt=DateTime.Now.AddMinutes(-15), Duration=TimeSpan.FromMinutes(15) },
                    new AlertViewModel { Id="A2", PatientName="Emily Davis", Zone="Zone 6", HighestPressure=88, DetectedAt=DateTime.Now.AddMinutes(-30), Duration=TimeSpan.FromMinutes(30) }
                },

                // High-risk patients table
                HighRiskPatients = new List<PatientRiskViewModel>
                {
                    new PatientRiskViewModel { PatientId="P1", Patient="John Smith", Room="5", RiskLevel="High", HighestPressure=95, ActiveAlerts=1 },
                    new PatientRiskViewModel { PatientId="P2", Patient="Emily Davis", Room="6", RiskLevel="High", HighestPressure=88, ActiveAlerts=2 }
                }
            };

            return View(vm);
        }

        // Handles alert acknowledgement action (simple demo functionality)
        [HttpPost]
        public IActionResult Acknowledge(string id)
        {
            TempData["Toast"] = $"Alert {id} acknowledged.";
            return RedirectToAction("Dashboard");
        }

        //  Displays all active patient alerts with severity categories
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
            // 🔴 High Risk (Critical)
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
            },

            // 🟡 Medium Risk
            new AlertItem
            {
                Id = "P002",
                PatientName = "Michael Brown",
                Zone = "Zone 4",
                PressureReading = 75,
                DetectedAt = DateTime.Today.AddHours(13).AddMinutes(45),
                Duration = TimeSpan.FromHours(1),
                RecommendedActions = new[]
                {
                    "Schedule position change within 30 minutes",
                    "Monitor pressure trends closely",
                    "Document intervention in patient record"
                },
                IsNew = false
            },
            new AlertItem
            {
                Id = "P005",
                PatientName = "Sarah Wilson",
                Zone = "Zone 3",
                PressureReading = 68,
                DetectedAt = DateTime.Today.AddHours(13).AddMinutes(30),
                Duration = TimeSpan.FromMinutes(90),
                RecommendedActions = new[]
                {
                    "Schedule position change within 30 minutes",
                    "Monitor pressure trends closely",
                    "Document intervention in patient record"
                },
                IsNew = true
            },

            // 🟢 Low Risk
            new AlertItem
            {
                Id = "P004",
                PatientName = "David Martinez",
                Zone = "Zone 2",
                PressureReading = 58,
                DetectedAt = DateTime.Today.AddHours(12),
                Duration = TimeSpan.FromHours(3),
                RecommendedActions = new[]
                {
                    "Continue monitoring pressure readings",
                    "Encourage posture adjustments every 2 hours"
                },
                IsNew = false
            }
        }
            };

            return View(vm);
        }


        //  Displays static patient list and metrics (mock data)
        public IActionResult Patients()
        {
            return View();
        }

        //  Displays pressure and posture analytics charts
        public IActionResult Analytics()
        {
            return View();
        }

        //  Displays all patient comments (from the database) for clinician review
        public IActionResult Communication()
        {
            var comments = _context.Comments
                .OrderByDescending(c => c.CreatedAt)
                .ToList();

            return View(comments);
        }

        //  Allows clinician to reply to a specific patient comment
        [HttpPost]
        public IActionResult Reply(int id, string replyText)
        {
            // Find comment in database
            var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
            if (comment == null)
                return NotFound();

            // Add clinician reply and save timestamp
            comment.ClinicianReply = replyText;
            comment.ReplyAt = DateTime.UtcNow;

            // Save reply in the database
            _context.SaveChanges();

            // Show confirmation notification
            TempData["Toast"] = "Reply sent successfully!";
            return RedirectToAction("Communication");
        }

        //  Generates a mock clinical assessment report for selected patient
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
