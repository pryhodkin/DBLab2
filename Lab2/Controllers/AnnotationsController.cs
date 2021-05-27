using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab2.Models;

namespace Lab2.Controllers
{
    public class AnnotationsController : Controller
    {
        private readonly LyricsServiceContext _context;

        public AnnotationsController(LyricsServiceContext context)
        {
            _context = context;
        }

        // GET: Annotations
        public async Task<IActionResult> Index()
        {
            var lyricsServiceContext = _context.Annotations.Include(a => a.Author).Include(a => a.Song);
            return View(await lyricsServiceContext.ToListAsync());
        }

        // GET: Annotations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annotation = await _context.Annotations
                .Include(a => a.Author)
                .Include(a => a.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (annotation == null)
            {
                return NotFound();
            }

            return View(annotation);
        }

        // GET: Annotations/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Id");
            return View();
        }

        // POST: Annotations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AuthorId,SongId,Lines,Text")] Annotation annotation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(annotation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Name", annotation.AuthorId);
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Id", annotation.SongId);
            return View(annotation);
        }

        // GET: Annotations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annotation = await _context.Annotations.FindAsync(id);
            if (annotation == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Name", annotation.AuthorId);
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Lyrics", annotation.SongId);
            return View(annotation);
        }

        // POST: Annotations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AuthorId,SongId,Lines,Text")] Annotation annotation)
        {
            if (id != annotation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(annotation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnotationExists(annotation.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Users, "Id", "Name", annotation.AuthorId);
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Lyrics", annotation.SongId);
            return View(annotation);
        }

        // GET: Annotations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annotation = await _context.Annotations
                .Include(a => a.Author)
                .Include(a => a.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (annotation == null)
            {
                return NotFound();
            }

            return View(annotation);
        }

        // POST: Annotations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var annotation = await _context.Annotations.FindAsync(id);
            _context.Annotations.Remove(annotation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnotationExists(int id)
        {
            return _context.Annotations.Any(e => e.Id == id);
        }
    }
}
