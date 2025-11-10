using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PROG7312_POE.Models;
using PROG7312_POE.Services.Interfaces;
using PROG7312_POE.Data;

namespace PROG7312_POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IIssueReportService _issueReportService;
        private readonly IEventRepository _eventRepository;

        public HomeController(ILogger<HomeController> logger, IIssueReportService issueReportService, IEventRepository eventRepository)
        {
            _logger = logger;
            _issueReportService = issueReportService;
            _eventRepository = eventRepository;
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

        // GET: Home/LocalEvents
        public IActionResult LocalEvents()
        {
            // Check if dictionary is empty and seed it
            if (_eventRepository.GetTotalCount() == 0)
            {
                SeedEventsIntoDictionary();
            }
            
            // Get events from dictionary and pass to view
            var events = _eventRepository.GetUpcoming().ToList();
            return View(events);
        }

        // GET: Home/ServiceRequestStatus
        public IActionResult ServiceRequestStatus()
        {
            return View();
        }

        // API: Get all service requests as JSON
        [HttpGet]
        public async Task<IActionResult> GetServiceRequests()
        {
            try
            {
                var issues = await _issueReportService.GetAllIssuesAsync();
                
                // Transform to a format suitable for the frontend
                var requests = issues.Select(issue => new
                {
                    id = FormatRequestId(issue.Id),
                    category = issue.Category,
                    location = issue.Location,
                    description = issue.Description,
                    status = GetStatusText(issue.Status),
                    priority = GetPriorityText(issue.Priority),
                    submittedDate = issue.ReportedDate.ToString("o"), // ISO 8601 format
                    lastUpdated = issue.ReportedDate.ToString("o")
                }).ToList();

                return Json(requests);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving service requests");
                return Json(new List<object>());
            }
        }

        private void SeedEventsIntoDictionary()
        {
            // Get sample events from the separate data file
            var sampleEvents = EventSeedData.GetSampleEvents();

            foreach (var evt in sampleEvents)
            {
                _eventRepository.Add(evt);
            }
            
            _logger.LogInformation($"Seeded {sampleEvents.Count} events into dictionary");
        }

        // Helper method to format request ID
        private string FormatRequestId(string id)
        {
            // Convert GUID to SR-YYYY-NNNNNN format
            var hashCode = Math.Abs(id.GetHashCode());
            var year = DateTime.Now.Year;
            var number = (hashCode % 999999).ToString("D6");
            return $"SR-{year}-{number}";
        }

        // Helper method to convert IssueStatus enum to text
        private string GetStatusText(IssueStatus status)
        {
            return status switch
            {
                IssueStatus.Received => "Pending",
                IssueStatus.UnderReview => "In Progress",
                IssueStatus.InProgress => "In Progress",
                IssueStatus.Resolved => "Resolved",
                _ => "Pending"
            };
        }

        // Helper method to convert IssuePriority enum to text
        private string GetPriorityText(IssuePriority priority)
        {
            return priority switch
            {
                IssuePriority.Emergency => "Emergency",
                IssuePriority.High => "High",
                IssuePriority.Standard => "Medium",
                IssuePriority.Low => "Low",
                _ => "Medium"
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Add this method to your HomeController
        public IActionResult GetRecommendations()
        {
            var searchPatternService = HttpContext.RequestServices.GetRequiredService<ISearchPatternService>();
            var recommendations = searchPatternService.GetRecommendedEvents(6);
            return Json(recommendations);
        }
    }
}
