using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // For SelectList
using Microsoft.EntityFrameworkCore; // For Include (if needed later)
using DestinyForum.Data; // Your DbContext
using DestinyForum.Models;

namespace DestinyForum.Controllers
{
    public class CommentsController : Controller
    {
        private readonly DestinyForumContext _context;

        public CommentsController(DestinyForumContext context)
        {
            _context = context;
        }

        // GET: Comments/Create
        public IActionResult Create(int discussionId) // Receive discussionId as a parameter
        {
            // Make sure the view knows which discussion it's for
            ViewData["DiscussionId"] = discussionId;  // No need for a SelectList if it's predetermined
            var comment = new Comment { DiscussionId = discussionId }; // Initialize DiscussionId
            return View(comment);
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Content,DiscussionId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CreateDate = DateTime.Now;
                _context.Add(comment);
                await _context.SaveChangesAsync();

                // Redirect to GetDiscussion - This will need to be adjusted once that action exists
                return RedirectToAction("GetDiscussion", "Home", new { id = comment.DiscussionId }); // Correct redirect
            }

            // If model state isn't valid, redisplay the form with the entered data
            ViewData["DiscussionId"] = comment.DiscussionId; // Preserve DiscussionId
            return View(comment);
        }
    }
}
