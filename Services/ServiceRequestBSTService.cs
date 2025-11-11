using PROG7312_POE.Models;
using PROG7312_POE.Services.DataStructures;
using PROG7312_POE.Services.Interfaces;

namespace PROG7312_POE.Services
{
    // Service for managing service requests using a Binary Search Tree
    // Provides efficient searching, filtering, and sorted retrieval
    public class ServiceRequestBSTService
    {
        private readonly BinarySearchTree<IssueReport> _bst;
        private readonly IIssueReportService _issueReportService;
        private readonly ILogger<ServiceRequestBSTService> _logger;

        public ServiceRequestBSTService(
            IIssueReportService issueReportService,
            ILogger<ServiceRequestBSTService> logger)
        {
            _issueReportService = issueReportService ?? throw new ArgumentNullException(nameof(issueReportService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bst = new BinarySearchTree<IssueReport>(new ServiceRequestComparer());
        }

        // Load all service requests into the BST
        public async Task InitializeAsync()
        {
            try
            {
                _bst.Clear();
                var allIssues = await _issueReportService.GetAllIssuesAsync();
                
                foreach (var issue in allIssues)
                {
                    _bst.Insert(issue);
                }

                _logger.LogInformation($"Loaded {_bst.Count()} service requests into BST");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initializing BST with service requests");
                throw;
            }
        }

        // Get all requests sorted by priority and date
        public List<IssueReport> GetSortedRequests()
        {
            return _bst.InOrderTraversal();
        }

        // Search for a specific request by ID
        public IssueReport? SearchById(string requestId)
        {
            return _bst.Search(r => r.Id.Equals(requestId, StringComparison.OrdinalIgnoreCase));
        }

        // Filter requests by category
        public List<IssueReport> FilterByCategory(string category)
        {
            return _bst.FindAll(r => r.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }

        // Filter requests by status
        public List<IssueReport> FilterByStatus(IssueStatus status)
        {
            return _bst.FindAll(r => r.Status == status);
        }

        // Filter requests by priority
        public List<IssueReport> FilterByPriority(IssuePriority priority)
        {
            return _bst.FindAll(r => r.Priority == priority);
        }

        // Filter requests by multiple criteria
        public List<IssueReport> FilterRequests(string? category = null, IssueStatus? status = null, IssuePriority? priority = null)
        {
            return _bst.FindAll(r =>
            {
                bool matchesCategory = string.IsNullOrEmpty(category) || r.Category.Equals(category, StringComparison.OrdinalIgnoreCase);
                bool matchesStatus = !status.HasValue || r.Status == status.Value;
                bool matchesPriority = !priority.HasValue || r.Priority == priority.Value;
                
                return matchesCategory && matchesStatus && matchesPriority;
            });
        }

        // Get statistics about service requests
        public ServiceRequestStatistics GetStatistics()
        {
            var allRequests = _bst.InOrderTraversal();
            
            return new ServiceRequestStatistics
            {
                TotalRequests = allRequests.Count,
                PendingCount = allRequests.Count(r => r.Status == IssueStatus.Received),
                InProgressCount = allRequests.Count(r => r.Status == IssueStatus.InProgress || r.Status == IssueStatus.UnderReview),
                ResolvedCount = allRequests.Count(r => r.Status == IssueStatus.Resolved),
                EmergencyCount = allRequests.Count(r => r.Priority == IssuePriority.Emergency),
                HighPriorityCount = allRequests.Count(r => r.Priority == IssuePriority.High),
                StandardPriorityCount = allRequests.Count(r => r.Priority == IssuePriority.Standard),
                LowPriorityCount = allRequests.Count(r => r.Priority == IssuePriority.Low)
            };
        }

        // Get tree height for performance monitoring
        public int GetTreeHeight()
        {
            return _bst.GetHeight();
        }

        // Get total count of requests in BST
        public int GetTotalCount()
        {
            return _bst.Count();
        }
    }

    // Statistics model for service requests
    public class ServiceRequestStatistics
    {
        public int TotalRequests { get; set; }
        public int PendingCount { get; set; }
        public int InProgressCount { get; set; }
        public int ResolvedCount { get; set; }
        public int EmergencyCount { get; set; }
        public int HighPriorityCount { get; set; }
        public int StandardPriorityCount { get; set; }
        public int LowPriorityCount { get; set; }
    }
}