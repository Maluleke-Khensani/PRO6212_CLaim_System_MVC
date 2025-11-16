using Claims_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Manager")]
public class ManagerController : Controller
{
    private readonly IClaimService _service;

    public ManagerController(IClaimService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var claims = await _service.GetPendingClaimsForManagerAsync();
        return View(claims);
    }

    [HttpPost]
    public async Task<IActionResult> ApproveClaim(int claimId)
    {
        await _service.UpdateManagerStatusAsync(claimId, "Approved");
        TempData["Message"] = "Claim approved successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> RejectClaim(int claimId)
    {
        await _service.UpdateManagerStatusAsync(claimId, "Rejected");
        TempData["Message"] = "Claim rejected successfully.";
        return RedirectToAction(nameof(Index));
    }
}
