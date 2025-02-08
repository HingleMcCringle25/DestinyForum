using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using DestinyForum.Models; 
using DestinyForum.Data; 

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
                .Include(d => d.Comments) //include comments
                .OrderByDescending(d => d.CreateDate) 
                .ToListAsync();

            return View(discussions);
        }

        public async Task<IActionResult> GetDiscussion(int id)
        {
            var discussion = await _context.Discussion
                .Include(d => d.Comments) //include comments
                .FirstOrDefaultAsync(d => d.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound(); 
            }

            return View(discussion);
        }
    }
}
