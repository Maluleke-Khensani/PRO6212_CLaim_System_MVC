using System.Linq;
using System.Threading.Tasks;
using Claims_System.Data;
using Claims_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Claims_System.Controllers
{
    public class HRController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public HRController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

            public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }


        public IActionResult CreateUser()
        {
            ViewBag.Roles = _roleManager.Roles.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(ApplicationUser model, string password, string role)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _roleManager.Roles.ToList();
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                Surname = model.Surname,
                EmployeeNumber = model.EmployeeNumber,
                HourlyRate = model.HourlyRate
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
                return RedirectToAction("ListUsers");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            ViewBag.Roles = _roleManager.Roles.ToList();
            return View(model);
        }


        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            ViewBag.Roles = _roleManager.Roles.ToList();
            ViewBag.UserRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> EditUser(ApplicationUser model, string role)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();

            // update info
            user.FullName = model.FullName;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.EmployeeNumber = model.EmployeeNumber;
            user.HourlyRate = model.HourlyRate;

            await _userManager.UpdateAsync(user);

            // update role
            var currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            if (currentRole != role)
            {
                if (currentRole != null)
                    await _userManager.RemoveFromRoleAsync(user, currentRole);

                await _userManager.AddToRoleAsync(user, role);
            }

            return RedirectToAction("ListUsers");
        }


        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            await _userManager.DeleteAsync(user);
            return RedirectToAction("ListUsers");
        }

        public async Task<IActionResult> GenerateReport()
        {
            var claims = await _context.LecturerClaims.ToListAsync();
            return View(claims);
        }

    }
}
