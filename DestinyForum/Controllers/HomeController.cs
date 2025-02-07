using System.Linq; // For ordering
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // For Include
using DestinyForum.Models; // Your models
using DestinyForum.Data; // Your DbContext

namespace DestinyForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly DestinyForumContext _context;

        public HomeController(DestinyForumContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var discussions = await _context.Discussion
                .Include(d => d.Comments) // Include comments for count
                .OrderByDescending(d => d.CreateDate) // Order by date descending
                .ToListAsync();

            return View(discussions);
        }

        public async Task<IActionResult> GetDiscussion(int id)
        {
            var discussion = await _context.Discussion
                .Include(d => d.Comments) // Include comments
                .FirstOrDefaultAsync(d => d.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound(); // Handle if discussion not found
            }

            return View(discussion);
        }
    }
}
