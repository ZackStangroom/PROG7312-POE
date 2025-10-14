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

        private void SeedEventsIntoDictionary()
        {
            var sampleEvents = new List<LocalEvent>
            {
                new LocalEvent
                {
                    Title = "Cape Town Community Clean-Up",
                    Description = "Join us for a community beach clean-up event. Help keep our beaches beautiful!",
                    EventDate = new DateTime(2025, 10, 18, 9, 0, 0),
                    Category = "Community",
                    Location = "Sea Point Promenade",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "Local Arts & Crafts Market",
                    Description = "Discover local artisans and craftspeople showcasing their work.",
                    EventDate = new DateTime(2025, 10, 22, 10, 0, 0),
                    Category = "Arts",
                    Location = "Green Point Park",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "City Council Public Meeting",
                    Description = "Public consultation on upcoming infrastructure projects.",
                    EventDate = new DateTime(2025, 10, 25, 18, 0, 0),
                    Category = "Municipal",
                    Location = "Cape Town Civic Centre",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "Cape Town Marathon",
                    Description = "Annual city marathon featuring 10K, 21K, and 42K routes.",
                    EventDate = new DateTime(2025, 10, 28, 6, 0, 0),
                    Category = "Sports",
                    Location = "City Centre",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "Free Health Screening Day",
                    Description = "Free health screenings including blood pressure, diabetes, and cholesterol.",
                    EventDate = new DateTime(2025, 11, 2, 8, 0, 0),
                    Category = "Health",
                    Location = "Khayelitsha Community Centre",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "Tree Planting Initiative",
                    Description = "Help us plant 500 trees to combat climate change and beautify our city.",
                    EventDate = new DateTime(2025, 11, 5, 9, 0, 0),
                    Category = "Environment",
                    Location = "Newlands Forest",
                    Status = EventStatus.Upcoming
                }
            };

            foreach (var evt in sampleEvents)
            {
                _eventRepository.Add(evt);
            }
            
            _logger.LogInformation($"Seeded {sampleEvents.Count} events into dictionary");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
