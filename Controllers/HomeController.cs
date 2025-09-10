using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PROG7312_POE.Models;
using PROG7312_POE.Services.Interfaces;

namespace PROG7312_POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IIssueReportService _issueReportService;

        public HomeController(ILogger<HomeController> logger, IIssueReportService issueReportService)
        {
            _logger = logger;
            _issueReportService = issueReportService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Home/ReportIssues
        public IActionResult ReportIssues()
        {
            var viewModel = new IssueReportViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReportIssues(IssueReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Simulate issue submission
                    var success = await _issueReportService.SubmitIssueAsync(model);

                    if (success)
                    {
                        // Provide feedback to the user
                        model.IsSubmitted = true;
                        model.SubmissionMessage = "Your issue has been successfully submitted! Thank you for helping improve Cape Town.";
                        
                        // Clear form data after successful submission
                        ModelState.Clear();
                        return View(new IssueReportViewModel 
                        { 
                            IsSubmitted = true, 
                            SubmissionMessage = model.SubmissionMessage 
                        });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "There was an error submitting your report. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error submitting issue report");
                    ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
                }
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
