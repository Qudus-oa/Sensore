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
    }
}
