using Microsoft.AspNetCore.Mvc;
using Sensore.Data;
using Sensore.Models;
using System.Linq;

namespace Sensore.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly AppDBContext _context;

        public NotificationsController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var notifications = _context.Notifications
                .OrderByDescending(n => n.CreatedAt)
                .Take(10)
                .ToList();

            return Json(notifications);
        }

        [HttpPost]
        public IActionResult MarkAllRead()
        {
            foreach (var note in _context.Notifications.Where(n => !n.IsRead))
            {
                note.IsRead = true;
            }
            _context.SaveChanges();
            return Ok();
        }

        // For demo: create fake notifications every 3 seconds
        [HttpPost]
        public IActionResult GenerateFake()
        {
            var note = new Notification
            {
                Title = "Plan to Move Soon",
                Message = $"Your zone {new Random().Next(1, 8)} is feeling pressure ({new Random().Next(60, 90)}). Adjust your position soon."
            };
            _context.Notifications.Add(note);
            _context.SaveChanges();
            return Ok();
        }
    }
}
