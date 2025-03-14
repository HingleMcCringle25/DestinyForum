using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DestinyForum.Models;
using DestinyForum.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace DestinyForum.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly DestinyForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(DestinyForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var discussions = await _context.Discussion
                .Include(d => d.Comments)
                .Include(d => d.ApplicationUser) 
                .OrderByDescending(d => d.CreateDate)
                .ToListAsync();

            return View(discussions);
        }

        public async Task<IActionResult> GetDiscussion(int id)
        {
            var discussion = await _context.Discussion
                .Include(d => d.Comments)
                .ThenInclude(c => c.ApplicationUser) 
                .Include(d => d.ApplicationUser) 
                .FirstOrDefaultAsync(d => d.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        public async Task<IActionResult> Profile(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var discussions = await _context.Discussion
                .Where(d => d.ApplicationUserId == id)
                .ToListAsync();

            ViewData["User"] = user;
            ViewData["Discussions"] = discussions;

            return View();
        }
    }
}
