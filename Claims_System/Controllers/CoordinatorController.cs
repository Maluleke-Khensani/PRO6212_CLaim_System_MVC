using Claims_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Coordinator")]
public class CoordinatorController : Controller
{
    private readonly IClaimService _service;

    public CoordinatorController(IClaimService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var claims = await _service.GetPendingClaimsForCoordinatorAsync();
        return View(claims);
    }

    [HttpPost]
    public async Task<IActionResult> ApproveClaim(int claimId)
    {
        await _service.UpdateCoordinatorStatusAsync(claimId, "Approved");
        TempData["Message"] = "Claim approved successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> RejectClaim(int claimId)
    {
        await _service.UpdateCoordinatorStatusAsync(claimId, "Rejected");
        TempData["Message"] = "Claim rejected successfully.";
        return RedirectToAction(nameof(Index));
    }
}
