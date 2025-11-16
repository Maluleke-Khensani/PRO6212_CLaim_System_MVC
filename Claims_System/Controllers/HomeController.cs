using System.Diagnostics;
using Claims_System.Models;
using Microsoft.AspNetCore.Mvc;
using Claims_System.Data;

namespace Claims_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;         
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Example dashboard actions — replace or remove names to match your Views/Home/*.cshtml
        public IActionResult LecturerDashboard()
        {
            var claims = _context.LecturerClaims.ToList();
            return View("LecturerDashboard", claims);
        }

        public IActionResult CoordinatorDashboard()
        {
            return View("CoordinatorDashboard");
        }

        public IActionResult ManagerDashboard()
        {
            return View("ManagerDashboard");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
