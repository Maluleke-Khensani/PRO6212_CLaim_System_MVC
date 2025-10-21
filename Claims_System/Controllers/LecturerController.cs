using Claims_System.Models;
using Claims_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Claims_System.Controllers
{
    public class LecturerController : Controller
    {
        private readonly IClaimService _claimService;
        private readonly ClaimsDbContext _context; // add this


        public LecturerController(IClaimService lecturerService, ClaimsDbContext context)
        {
            _claimService = lecturerService;
            _context = context; // initialize it
        }


        // ------------------- Dashboard -------------------
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                TempData["Error"] = "Session expired. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            var allClaims = await _claimService.GetAllClaimsAsync();
            var userClaims = allClaims
                .Where(c => c.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(c => c.Submitted);

            return View(userClaims);
        }

        // ------------------- Create Claim -------------------
        public IActionResult ClaimForm()
        {
            var username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                TempData["Error"] = "Session expired. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            var model = new LecturerClaim
            {
                Username = username, // auto-populate
                Rate = 500,
                Submitted = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClaimForm(LecturerClaim model, IFormFile? Document1, IFormFile? Document2)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please correct the highlighted errors in the form.";
                return View(model);
            }

            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                TempData["Error"] = "Your session has expired. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            // Ensure the model's username matches logged-in user
            model.Username = username;

            // Retrieve the user from the database to get their unique EmployeeNumber
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                model.EmployeeNumber = user.EmployeeNumber;
            }
            else
            {
                // Fallback if somehow the user is not found
                TempData["Error"] = "User profile not found. Cannot submit claim.";
                return RedirectToAction(nameof(Index));
            }

            var success = await _claimService.CreateClaimAsync(model, Document1, Document2);

            TempData[success ? "Success" : "Error"] = success
                ? "Claim submitted successfully!"
                : "Failed to save the claim. Please try again.";

            return RedirectToAction(nameof(Index));
        }


        // ------------------- View Claim -------------------
        public async Task<IActionResult> ViewClaim(int id)
        {
            var claim = await _claimService.GetClaimByIdAsync(id);
            if (claim == null)
                return NotFound();

            return View(claim);
        }

        // ------------------- Edit Claim -------------------
        public async Task<IActionResult> Edit(int id)
        {
            var username = HttpContext.Session.GetString("Username");
            var claim = await _claimService.GetClaimByIdAsync(id);

            if (claim == null || !claim.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
            {
                TempData["Error"] = "Claim not found or you are not authorized to edit it.";
                return RedirectToAction("Index");
            }

            if (claim.CoordinatorStatus != "Pending" || claim.ManagerStatus != "Pending")
            {
                TempData["Error"] = "Claim has already been reviewed and cannot be edited.";
                return RedirectToAction("Index");
            }

            return View(claim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LecturerClaim model)
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                TempData["Error"] = "Session expired. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            if (!model.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
            {
                TempData["Error"] = "You are not authorized to edit this claim.";
                return RedirectToAction("Index");
            }

            var result = await _claimService.UpdateClaimAsync(model);

            TempData[result ? "Success" : "Error"] = result
                ? "Claim updated successfully!"
                : "Failed to update claim. Please try again.";

            return RedirectToAction("Index");
        }

        // ------------------- Delete Claim -------------------
        public async Task<IActionResult> Delete(int id)
        {
            var username = HttpContext.Session.GetString("Username");
            var claim = await _claimService.GetClaimByIdAsync(id);

            if (claim == null || !claim.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
            {
                TempData["Error"] = "Claim not found or you cannot delete it.";
                return RedirectToAction("Index");
            }

            if (claim.ManagerStatus != "Pending")
            {
                TempData["Error"] = "Claim has been reviewed and cannot be deleted.";
                return RedirectToAction("Index");
            }

            return View(claim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string Username)
        {
            var username = HttpContext.Session.GetString("Username");

            // Optional: check if posted Username matches session
            if (!username.Equals(Username, StringComparison.OrdinalIgnoreCase))
            {
                TempData["Error"] = "Unauthorized action.";
                return RedirectToAction("Index");
            }

            var claim = await _claimService.GetClaimByIdAsync(id);

            if (claim == null || !claim.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
            {
                TempData["Error"] = "Claim not found or unauthorized action.";
                return RedirectToAction("Index");
            }

            var success = await _claimService.DeleteClaimAsync(id);
            TempData[success ? "Success" : "Error"] = success
                ? "Claim deleted successfully!"
                : "Failed to delete claim. Please try again.";

            return RedirectToAction("Index");
        }


        // ------------------- Download Documents -------------------
        public IActionResult DownloadDocument1(int id)
        {
            try
            {
                var fileResult = _claimService.DownloadDocument1(id);
                if (fileResult == null)
                {
                    TempData["Error"] = "Document not found or unavailable.";
                    return RedirectToAction(nameof(Index));
                }

                return fileResult;
            }
            catch (Exception)
            {
                TempData["Error"] = "Unable to download the document at this time.";
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult DownloadDocument2(int id)
        {
            try
            {
                var fileResult = _claimService.DownloadDocument2(id);
                if (fileResult == null)
                {
                    TempData["Error"] = "Document not found or unavailable.";
                    return RedirectToAction(nameof(Index));
                }

                return fileResult;
            }
            catch (Exception)
            {
                TempData["Error"] = "Unable to download the document at this time.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
