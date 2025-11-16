using Claims_System.Models;
using Claims_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Lecturer")]
public class LecturerController : Controller
{
    private readonly IClaimService _claimService;
    private readonly UserManager<ApplicationUser> _userManager;

    public LecturerController(IClaimService claimService, UserManager<ApplicationUser> userManager)
    {
        _claimService = claimService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToPage("/Account/Login", new { area = "Identity" });

        var allClaims = await _claimService.GetAllClaimsAsync();
        var userClaims = allClaims
            .Where(c => c.EmployeeNumber == user.EmployeeNumber)
            .OrderByDescending(c => c.Submitted);

        return View(userClaims);
    }

    public async Task<IActionResult> ClaimForm()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToPage("/Account/Login", new { area = "Identity" });

        // only fields the lecturer can edit
        var model = new LecturerClaimViewModel
        {
            Month = DateTime.Now.Month,
            Year = DateTime.Now.Year,
            HoursWorked = 0,
            ModuleName = "",
            Notes = ""
        };

        // read-only fields for display
        ViewBag.Username = user.UserName;
        ViewBag.FullName = user.FullName;
        ViewBag.Rate = user.HourlyRate;
        ViewBag.Submitted = DateTime.Now.ToString("yyyy-MM-dd"); // format for date input

        return View(model);
    }




    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ClaimForm(LecturerClaimViewModel model)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToPage("/Account/Login", new { area = "Identity" });

        if (!ModelState.IsValid)
            return View(model);

        // force system values here before sending to service
        var claim = new LecturerClaim
        {
            Username = user.UserName,          // locked
            FullName = user.FullName,          // locked
            EmployeeNumber = user.EmployeeNumber,
            Rate = user.HourlyRate,            // locked
            Submitted = DateTime.Now,
            Month = model.Month,
            Year = model.Year,
            HoursWorked = model.HoursWorked,
            ModuleName = model.ModuleName,
            Notes = model.Notes
        };

        var success = await _claimService.CreateClaimAsync(
            claim,
            Request.Form.Files["Document1"],
            Request.Form.Files["Document2"]
        );

        TempData[success ? "Success" : "Error"] = success
            ? "Claim submitted successfully!"
            : "Failed to save the claim. Please try again.";

        return RedirectToAction(nameof(Index));
    }



}
