using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; 
using Microsoft.EntityFrameworkCore; 
using DestinyForum.Data; 
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
        public IActionResult Create(int discussionId) 
        {
            
            ViewData["DiscussionId"] = discussionId;  
            var comment = new Comment { DiscussionId = discussionId }; 
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

                
                return RedirectToAction("GetDiscussion", "Home", new { id = comment.DiscussionId });
            }

            
            ViewData["DiscussionId"] = comment.DiscussionId; 
            return View(comment);
        }
    }
}
