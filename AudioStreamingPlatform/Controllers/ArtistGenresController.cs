using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AudioStreamingPlatform.Data;
using AudioStreamingPlatform.Models;

namespace AudioStreamingPlatform.Controllers
{
    public class ArtistGenresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtistGenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ArtistGenres
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Artist Genres";
            var artistGenres = _context.ArtistGenres
                .Include(ag => ag.Artist)
                .Include(ag => ag.Genre);
            return View(await artistGenres.ToListAsync());
        }

        // GET: ArtistGenres/Details/5
        public async Task<IActionResult> Details(int? artistId, int? genreId)
        {
            if (artistId == null || genreId == null) return NotFound();

            ViewData["Title"] = "Artist Genre Details";

            var artistGenre = await _context.ArtistGenres
                .Include(ag => ag.Artist)
                .Include(ag => ag.Genre)
                .FirstOrDefaultAsync(ag => ag.ArtistId == artistId && ag.GenreId == genreId);

            if (artistGenre == null) return NotFound();
            return View(artistGenre);
        }

        // GET: ArtistGenres/Create
        public IActionResult Create()
        {
            ViewData["Title"] = "Add Artist Genre";
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Title");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Title");
            return View();
        }

        // POST: ArtistGenres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArtistId,GenreId")] ArtistGenre artistGenre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artistGenre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Title", artistGenre.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Title", artistGenre.GenreId);
            return View(artistGenre);
        }

        // GET: ArtistGenres/Edit
        public async Task<IActionResult> Edit(int? artistId, int? genreId)
        {
            if (artistId == null || genreId == null) return NotFound();

            ViewData["Title"] = "Edit Artist Genre";

            var artistGenre = await _context.ArtistGenres
                .FirstOrDefaultAsync(ag => ag.ArtistId == artistId && ag.GenreId == genreId);

            if (artistGenre == null) return NotFound();

            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Title", artistGenre.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Title", artistGenre.GenreId);
            return View(artistGenre);
        }

        // POST: ArtistGenres/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ArtistId,GenreId")] ArtistGenre artistGenre)
        {
            if (!ArtistGenreExists(artistGenre.ArtistId, artistGenre.GenreId)) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artistGenre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistGenreExists(artistGenre.ArtistId, artistGenre.GenreId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Title", artistGenre.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Title", artistGenre.GenreId);
            return View(artistGenre);
        }

        // GET: ArtistGenres/Delete
        public async Task<IActionResult> Delete(int? artistId, int? genreId)
        {
            if (artistId == null || genreId == null) return NotFound();

            ViewData["Title"] = "Delete Artist Genre";

            var artistGenre = await _context.ArtistGenres
                .Include(ag => ag.Artist)
                .Include(ag => ag.Genre)
                .FirstOrDefaultAsync(ag => ag.ArtistId == artistId && ag.GenreId == genreId);

            if (artistGenre == null) return NotFound();
            return View(artistGenre);
        }

        // POST: ArtistGenres/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int artistId, int genreId)
        {
            var artistGenre = await _context.ArtistGenres
                .FirstOrDefaultAsync(ag => ag.ArtistId == artistId && ag.GenreId == genreId);

            if (artistGenre != null) _context.ArtistGenres.Remove(artistGenre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistGenreExists(int artistId, int genreId)
        {
            return _context.ArtistGenres.Any(ag => ag.ArtistId == artistId && ag.GenreId == genreId);
        }
    }
}