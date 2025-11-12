// References:
// Service Layer Pattern:
// - Fowler, M. (2002). Patterns of Enterprise Application Architecture. Addison-Wesley.
//   https://martinfowler.com/eaaCatalog/serviceLayer.html
//
// Dependency Injection in ASP.NET Core:
// - Microsoft. (2024). Dependency injection in ASP.NET Core.
//   https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection
//
// Async/Await Best Practices:
// - Microsoft. (2024). Asynchronous programming with async and await.
//   https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/

using PROG7312_POE.Models;
using PROG7312_POE.Services.DataStructures;
using PROG7312_POE.Services.Interfaces;

namespace PROG7312_POE.Services
{
    // Service for managing service request relationships using a graph
    public class ServiceRequestGraphService
    {
        private readonly ServiceRequestGraph _graph;
        private readonly IIssueReportService _issueReportService;
        private readonly ILogger<ServiceRequestGraphService> _logger;
        private bool _isInitialized;

        public ServiceRequestGraphService(
            IIssueReportService issueReportService,
            ILogger<ServiceRequestGraphService> _logger)
        {
            _graph = new ServiceRequestGraph();
            _issueReportService = issueReportService ?? throw new ArgumentNullException(nameof(issueReportService));
            this._logger = _logger ?? throw new ArgumentNullException(nameof(_logger));
            _isInitialized = false;
        }

        // Initialize the graph with all service requests
        public async Task InitializeGraphAsync()
        {
            try
            {
                var allRequests = await _issueReportService.GetAllIssuesAsync();
                
                // Add all requests to the graph
                foreach (var request in allRequests)
                {
                    _graph.AddRequest(request);
                }

                // Build relationships between requests
                _graph.BuildRelationships();

                _isInitialized = true;

                var stats = _graph.GetStatistics();
                _logger.LogInformation($"Graph initialized: {stats["TotalNodes"]} nodes, {stats["TotalEdges"]} edges");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize service request graph");
                throw;
            }
        }

        // Get requests related to a specific request
        public async Task<List<IssueReport>> GetRelatedRequestsAsync(string requestId, int maxDepth = 2)
        {
            await EnsureInitializedAsync();
            return _graph.GetRelatedRequests(requestId, maxDepth);
        }

        // Get requests in the same location
        public async Task<List<IssueReport>> GetRequestsInSameLocationAsync(string requestId)
        {
            await EnsureInitializedAsync();
            return _graph.GetRelatedRequestsByType(requestId, RelationType.SameLocation);
        }

        // Get requests in the same category
        public async Task<List<IssueReport>> GetRequestsInSameCategoryAsync(string requestId)
        {
            await EnsureInitializedAsync();
            return _graph.GetRelatedRequestsByType(requestId, RelationType.SameCategory);
        }

        // Get potential duplicate requests
        public async Task<List<IssueReport>> GetPotentialDuplicatesAsync(string requestId)
        {
            await EnsureInitializedAsync();
            return _graph.GetRelatedRequestsByType(requestId, RelationType.Duplicate);
        }

        // Find dependency path between two requests
        public async Task<List<IssueReport>> FindDependencyPathAsync(string startId, string endId)
        {
            await EnsureInitializedAsync();
            return _graph.FindShortestPath(startId, endId);
        }

        // Get graph statistics
        public async Task<Dictionary<string, object>> GetGraphStatisticsAsync()
        {
            await EnsureInitializedAsync();
            return _graph.GetStatistics();
        }

        // Helper to ensure graph is initialized
        private async Task EnsureInitializedAsync()
        {
            if (!_isInitialized)
            {
                await InitializeGraphAsync();
            }
        }

        public ServiceRequestGraph GetGraph() => _graph;
    }
}