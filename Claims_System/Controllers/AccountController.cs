using Microsoft.AspNetCore.Mvc;
using Claims_System.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Claims_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly ClaimsDbContext _context;

        public AccountController(ClaimsDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

       
        [HttpPost]
        public IActionResult Login(LoginView model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users
                .FirstOrDefault(u => u.Username == model.Username && u.PasswordHash == model.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(model);
            }

            // Store session info
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", model.Role);

            // Redirect based on selected role
            return model.Role switch
            {
                "Lecturer" => RedirectToAction("Index", "Lecturer"),
                "Coordinator" => RedirectToAction("Index", "Coordinator"),
                "Manager" => RedirectToAction("Index", "Manager"),
                _ => RedirectToAction("Index", "Home")
            };
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
