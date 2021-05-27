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
    public class SongsController : Controller
    {
        private readonly LyricsServiceContext _context;

        public SongsController(LyricsServiceContext context)
        {
            _context = context;
        }

        // GET: Songs
        public async Task<IActionResult> Index()
        {
            var lyricsServiceContext = _context.Songs.Include(s => s.Album).Include(s => s.Label).Include(s => s.MainArtist).Include(s => s.SecondaryArtist);
            return View(await lyricsServiceContext.ToListAsync());
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .Include(s => s.Label)
                .Include(s => s.MainArtist)
                .Include(s => s.SecondaryArtist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Name");
            ViewData["LabelId"] = new SelectList(_context.Labels, "Id", "Name");
            ViewData["MainArtistId"] = new SelectList(_context.Artists, "Id", "Name");
            ViewData["SecondaryArtistId"] = new SelectList(_context.Artists, "Id", "Name");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MainArtistId,SecondaryArtistId,AlbumId,Lyrics,LabelId")] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Name", song.AlbumId);
            ViewData["LabelId"] = new SelectList(_context.Labels, "Id", "Name", song.LabelId);
            ViewData["MainArtistId"] = new SelectList(_context.Artists, "Id", "Name", song.MainArtistId);
            ViewData["SecondaryArtistId"] = new SelectList(_context.Artists, "Id", "Name", song.SecondaryArtistId);
            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Name", song.AlbumId);
            ViewData["LabelId"] = new SelectList(_context.Labels, "Id", "Name", song.LabelId);
            ViewData["MainArtistId"] = new SelectList(_context.Artists, "Id", "Name", song.MainArtistId);
            ViewData["SecondaryArtistId"] = new SelectList(_context.Artists, "Id", "Name", song.SecondaryArtistId);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MainArtistId,SecondaryArtistId,AlbumId,Lyrics,LabelId")] Song song)
        {
            if (id != song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id))
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
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Name", song.AlbumId);
            ViewData["LabelId"] = new SelectList(_context.Labels, "Id", "Name", song.LabelId);
            ViewData["MainArtistId"] = new SelectList(_context.Artists, "Id", "Name", song.MainArtistId);
            ViewData["SecondaryArtistId"] = new SelectList(_context.Artists, "Id", "Name", song.SecondaryArtistId);
            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .Include(s => s.Label)
                .Include(s => s.MainArtist)
                .Include(s => s.SecondaryArtist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }
    }
}
