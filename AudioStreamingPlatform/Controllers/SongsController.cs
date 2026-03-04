using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AudioStreamingPlatform.Data;
using AudioStreamingPlatform.Models;

namespace AudioStreamingPlatform.Controllers
{
    public class SongsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Songs
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Songs"; // dynamic page title

            var songs = _context.Songs.Include(s => s.Artist);
            return View(await songs.ToListAsync());
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            ViewData["Title"] = "Song Details"; // dynamic page title

            var song = await _context.Songs
                .Include(s => s.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (song == null) return NotFound();

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Add Song"; // dynamic title
            // show artist names instead of just Id
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Title");
            return View();
        }

        // POST: Songs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ArtistId,Duration,ReleaseDate")] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Title", song.ArtistId);
            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            ViewData["Title"] = "Edit Song"; // dynamic title

            var song = await _context.Songs.FindAsync(id);
            if (song == null) return NotFound();

            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Title", song.ArtistId);
            return View(song);
        }

        // POST: Songs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ArtistId,Duration,ReleaseDate")] Song song)
        {
            if (id != song.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Title", song.ArtistId);
            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            ViewData["Title"] = "Delete Song"; // dynamic title

            var song = await _context.Songs
                .Include(s => s.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null) return NotFound();

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song != null) _context.Songs.Remove(song);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }
    }
}