using Microsoft.AspNetCore.Mvc;
using Sensore.Data;
using Sensore.Models;
using System.Linq;

namespace Sensore.Controllers
{
    public class CommentsController : Controller
    {
        private readonly AppDBContext _context;
        public CommentsController(AppDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Add([FromBody] Comment comment)
        {
            if (string.IsNullOrWhiteSpace(comment.Text))
                return BadRequest("Comment text cannot be empty.");

            comment.CreatedAt = DateTime.UtcNow;
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Ok(new { success = true });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var comments = _context.Comments
                .OrderByDescending(c => c.CreatedAt)
                .ToList();
            return Json(comments);
        }
    }
}
