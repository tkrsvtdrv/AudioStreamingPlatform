using Microsoft.AspNetCore.Mvc;
using AudioStreamingPlatform.Data;
using AudioStreamingPlatform.Models;

namespace AudioStreamingPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Home/Index
        public IActionResult Index()
        {
            ViewData["Title"] = "Dashboard"; // for dynamic page title in _Layout.cshtml

            var dashboard = new DashboardViewModel
            {
                TotalArtists = _context.Artists.Count(),
                TotalGenres = _context.Genres.Count(),
                TotalSongs = _context.Songs.Count()
            };

            return View(dashboard);
        }

        // GET: Home/Privacy
        public IActionResult Privacy()
        {
            ViewData["Title"] = "Privacy Policy"; // dynamic title for privacy page
            return View();
        }
    }
}