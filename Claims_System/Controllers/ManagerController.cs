using Microsoft.AspNetCore.Mvc;
using Claims_System.Services;

public class ManagerController : Controller
{
    private readonly IClaimService _service;

    public ManagerController(IClaimService service)
    {
        _service = service;
    }

    // Dashboard
    public async Task<IActionResult> Index()
    {
        var claims = await _service.GetAllClaimsAsync();
        return View(claims);
    }

    // Opens Review Page
    [HttpGet]
    public async Task<IActionResult> ReviewClaim(int claimId)
    {
        var claim = await _service.GetClaimByIdAsync(claimId);
        if (claim == null) return NotFound();

        return View("ReviewClaim", claim);
    }

    // CONFIRMS Approval
    [HttpPost]
    public async Task<IActionResult> ApproveClaim(int claimId)
    {
        var claim = await _service.GetClaimByIdAsync(claimId);
        if (claim == null) return NotFound();

        await _service.UpdateManagerStatusAsync(claim.EmployeeNumber, "Approved");
        TempData["Message"] = "Claim approved successfully.";
        return RedirectToAction(nameof(Index));
    }

    // CONFIRMS Rejection
    [HttpPost]
    public async Task<IActionResult> RejectClaim(int claimId)
    {
        var claim = await _service.GetClaimByIdAsync(claimId);
        if (claim == null) return NotFound();

        await _service.UpdateManagerStatusAsync(claim.EmployeeNumber, "Rejected");
        TempData["Message"] = "Claim rejected successfully.";
        return RedirectToAction(nameof(Index));
    }

    // Download Document 1
    [HttpGet]
    public IActionResult DownloadDocument1(int claimId)
    {
        var fileResult = _service.DownloadDocument1(claimId);
        if (fileResult == null) return NotFound();
        return fileResult;
    }

    // Download Document 2
    [HttpGet]
    public IActionResult DownloadDocument2(int claimId)
    {
        var fileResult = _service.DownloadDocument2(claimId);
        if (fileResult == null) return NotFound();
        return fileResult;
    }

    [HttpGet]
    public IActionResult PreviewDocument(int claimId, int docNumber)
    {
        var claim = _service.GetClaimByIdAsync(claimId).Result;
        if (claim == null) return NotFound();

        byte[]? fileData = null;
        string? fileName = null;

        if (docNumber == 1)
        {
            fileData = claim.Document1FileData;
            fileName = claim.Document1FileName;
        }
        else if (docNumber == 2)
        {
            fileData = claim.Document2FileData;
            fileName = claim.Document2FileName;
        }

        if (fileData == null || string.IsNullOrEmpty(fileName))
            return NotFound();

        var decryptedData = _service.DecryptFileForPreview(fileData);
        var contentType = GetContentType(fileName);

        // Return file inline so browser shows it in <iframe>
        return File(decryptedData, contentType);
    }

    private string GetContentType(string fileName)
    {
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        return ext switch
        {
            ".pdf" => "application/pdf",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            _ => "application/octet-stream"
        };
    }

}
