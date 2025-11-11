using PROG7312_POE.Models;
using PROG7312_POE.Services.Interfaces;

namespace PROG7312_POE.Services
{
    public class IssueReportService : IIssueReportService
    {
        private readonly IIssueReportRepository _repository;
        private readonly ILogger<IssueReportService> _logger;

        // Dependency Injection of repository and logger
        public IssueReportService(IIssueReportRepository repository, ILogger<IssueReportService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Submit a new issue report (now uses seeded data - this method is kept for backwards compatibility)
        public async Task<bool> SubmitIssueAsync(IssueReportViewModel viewModel)
        {
            try
            {
                if (viewModel == null)
                    return false;

                // Validate required fields
                if (string.IsNullOrWhiteSpace(viewModel.Location) ||
                    string.IsNullOrWhiteSpace(viewModel.Category) ||
                    string.IsNullOrWhiteSpace(viewModel.Description))
                {
                    return false;
                }

                // Create and store the issue report (without file handling)
                var report = new IssueReport(
                    viewModel.Location,
                    viewModel.Category,
                    viewModel.Description
                );

                _repository.Add(report);

                // Log the successful submission
                _logger.LogInformation($"Issue report submitted successfully. ID: {report.Id}, Category: {report.Category}");

                return true;
            }
            catch (Exception ex)
            {
                // Log any errors during submission
                _logger.LogError(ex, "Error submitting issue report");
                return false;
            }
        }

        // Retrieve an issue report by its ID
        public async Task<IssueReport?> GetIssueByIdAsync(string id)
        {
            try
            {
                // get issue by ID 
                return _repository.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving issue with ID: {id}");
                return null;
            }
        }

        // Retrieve all issue reports
        public async Task<IEnumerable<IssueReport>> GetAllIssuesAsync()
        {
            try
            {
                // get all issues
                return _repository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all issues");
                return new List<IssueReport>();
            }
        }

        // Retrieve issues filtered by category
        public async Task<IEnumerable<IssueReport>> GetIssuesByCategoryAsync(string category)
        {
            try
            {
                // get issues by category
                return _repository.GetByCategory(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving issues for category: {category}");
                return new List<IssueReport>();
            }
        }

        // Retrieve issues filtered by priority
        public async Task<IEnumerable<IssueReport>> GetIssuesByPriorityAsync(IssuePriority priority)
        {
            try
            {
                // get issues by priority
                return _repository.GetByPriority(priority);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving issues for priority: {priority}");
                return new List<IssueReport>();
            }
        }

        // Get total count of issue reports
        public async Task<int> GetTotalIssueCountAsync()
        {
            try
            {
                // get total issue count
                return _repository.GetTotalCount();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving total issue count");
                return 0;
            }
        }

        // Process the next issue in the queues
        public async Task<bool> ProcessNextIssueAsync()
        {
            try
            {
                // Process the next issue based on priority and status
                var repo = _repository as IssueReportRepository;
                var nextIssue = repo?.GetNextForProcessing();

                if (nextIssue != null)
                {
                    _repository.UpdateStatus(nextIssue.Id, IssueStatus.UnderReview);
                    _logger.LogInformation($"Issue {nextIssue.Id} moved to Under Review");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing next issue");
                return false;
            }
        }

        // Retrieve the most recently submitted issue report
        public async Task<IssueReport?> GetMostRecentIssueAsync()
        {
            try
            {
                // get most recent issue
                var repo = _repository as IssueReportRepository;
                return repo?.GetMostRecent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving most recent issue");
                return null;
            }
        }
    }
}