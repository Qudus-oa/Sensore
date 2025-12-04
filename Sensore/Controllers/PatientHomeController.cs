using Microsoft.AspNetCore.Mvc;
using Sensore.Models;
using Sensore.Services;

namespace Sensore.Controllers
{
    public class PatientHomeController : Controller
    {
        private readonly PressureDataService _service = new PressureDataService();

        public IActionResult Index()
        {
            return View(); // this is to direct user to homepage first 
        }
        public IActionResult PressureDistribution()
        {
            var frames = _service.LoadAllFrames();
            return View(frames);
        }

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
    }
}