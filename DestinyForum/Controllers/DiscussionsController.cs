using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DestinyForum.Data;
using DestinyForum.Models;

namespace DestinyForum.Controllers
{
    public class DiscussionsController : Controller
    {
        private readonly DestinyForumContext _context;

        public DiscussionsController(DestinyForumContext context)
        {
            _context = context;
        }

        // GET: Discussions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Discussion.ToListAsync());
        }

        // GET: Discussions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussion
                .FirstOrDefaultAsync(m => m.DiscussionId == id);
            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        // GET: Discussions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Discussions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,ImageFilename")] Discussion discussion, IFormFile imageFile) // Add IFormFile parameter
        {
            discussion.CreateDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0) // Check if a file was uploaded
                {
                    // 1. Generate unique filename
                    string uniqueFileName = GenerateUniqueFileName(imageFile.FileName);

                    // 2. Save image to wwwroot/images
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", uniqueFileName); // Corrected path
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    // 3. Update Discussion object with filename
                    discussion.ImageFilename = uniqueFileName;
                }

                _context.Add(discussion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Or wherever you want to redirect
            }
            return View(discussion);
        }

        private string GenerateUniqueFileName(string fileName)
        {
            // Use Guid or DateTime to create unique name
            // Option 1: Using Guid
            // return Guid.NewGuid().ToString() + "_" + fileName;

            // Option 2: Using DateTime (more human-readable)
            return DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + fileName;
        }

        // GET: Discussions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussion.FindAsync(id);
            if (discussion == null)
            {
                return NotFound();
            }
            return View(discussion);
        }

        // POST: Discussions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiscussionId,Title,Content,ImageFilename,CreateDate")] Discussion discussion)
        {
            if (id != discussion.DiscussionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discussion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscussionExists(discussion.DiscussionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(discussion);
        }

        // GET: Discussions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussion
                .FirstOrDefaultAsync(m => m.DiscussionId == id);
            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        // POST: Discussions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discussion = await _context.Discussion.FindAsync(id);
            if (discussion != null)
            {
                _context.Discussion.Remove(discussion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscussionExists(int id)
        {
            return _context.Discussion.Any(e => e.DiscussionId == id);
        }
    }
}
