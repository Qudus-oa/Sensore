using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sensore.Models;
using Sensore.Services;
using Sensore.Data;

namespace Sensore.Controllers
{
    public class PatientHomeController : Controller
    {
        private readonly PressureDataService _service = new PressureDataService();
        private readonly AppDBContext _context;  // ✅ single consistent DB context

        // ✅ Constructor with dependency injection
        public PatientHomeController(AppDBContext context)
        {
            _context = context;
        }

        // ===== Home Page =====
        public IActionResult Index()
        {
            return View();
        }

        // ===== Reports Page =====
        public IActionResult Reports()
        {
            return View();
        }


        // ===== Pressure Distribution Page =====
        public IActionResult PressureDistribution()
        {
            var frames = _service.LoadAllFrames();
            return View(frames);
        }

        // ===== Metrics API for Chart/Graphs =====
        [HttpGet]
        public IActionResult GetMetricsData(string range = "6h")
        {
            var frames = _service.LoadAllFrames();

            DateTime cutoff = range switch
            {
                "1h" => DateTime.Now.AddHours(-1),
                "24h" => DateTime.Now.AddHours(-24),
                _ => DateTime.Now.AddHours(-6)
            };

            var filtered = frames
                .Where(f => f.Timestamp >= cutoff)
                .OrderBy(f => f.Timestamp)
                .ToList();

            var result = new
            {
                timestamps = filtered.Select(f => f.Timestamp.ToString("HH:mm")).ToList(),
                peakPressure = filtered.Select(f => f.PeakPressureIndex).ToList(),
                avgPressure = filtered.Select(f => f.AveragePressure).ToList(),
                contactArea = filtered.Select(f => f.ContactAreaPercent).ToList(),
                distributionScore = filtered.Select(f => f.DistributionScore).ToList()
            };

            return Json(result);
        }

        // ===== 🗨️ Chat Page (shows all comments) =====
        public IActionResult Chat()
        {
            var comments = _context.Comments
                                   .OrderByDescending(c => c.CreatedAt)
                                   .ToList();
            return View(comments);
        }
    }
}
