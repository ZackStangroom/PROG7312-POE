using PROG7312_POE.Models;
using PROG7312_POE.Services.DataStructures;
using PROG7312_POE.Services.Interfaces;
using PROG7312_POE.Data;

namespace PROG7312_POE.Services
{
    public class IssueReportRepository : IIssueReportRepository
    {
        private readonly DataStructures.LinkedList<IssueReport> _issues;
        private bool _isSeeded = false;

        // Using a linked list to store issue reports for efficient insertions and deletions
        public IssueReportRepository()
        {
            _issues = new DataStructures.LinkedList<IssueReport>();
            SeedData();
        }

        // Seed the repository with sample data
        private void SeedData()
        {
            if (_isSeeded)
                return;

            var sampleRequests = ServiceRequestSeedData.GetSampleServiceRequests();
            foreach (var request in sampleRequests)
            {
                _issues.Add(request);
            }

            _isSeeded = true;
        }

        // Adding a new issue report to the end of the linked list
        public void Add(IssueReport report)
        {
            if (report == null)
                throw new ArgumentNullException(nameof(report));

            _issues.Add(report);
        }

        // Retrieving an issue report by its unique ID
        public IssueReport? GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            return _issues.Find(report => report.Id == id);
        }

        // Retrieving all issue reports
        public IEnumerable<IssueReport> GetAll()
        {
            var result = new List<IssueReport>();
            _issues.ForEach(report => result.Add(report));
            return result;
        }

        // Filtering issue reports by category
        public IEnumerable<IssueReport> GetByCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
                return new List<IssueReport>();

            var filtered = _issues.FindAll(report => 
                report.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

            var resultList = new List<IssueReport>();
            filtered.ForEach(report => resultList.Add(report));
            return resultList;
        }

        // Filtering issue reports by priority
        public IEnumerable<IssueReport> GetByPriority(IssuePriority priority)
        {
            var filtered = _issues.FindAll(report => report.Priority == priority);

            var resultList = new List<IssueReport>();
            filtered.ForEach(report => resultList.Add(report));
            return resultList;
        }

        // Filtering issue reports by status
        public IEnumerable<IssueReport> GetByStatus(IssueStatus status)
        {
            var filtered = _issues.FindAll(report => report.Status == status);

            var resultList = new List<IssueReport>();
            filtered.ForEach(report => resultList.Add(report));
            return resultList;
        }

        // Updating the status of an existing issue report
        public bool UpdateStatus(string id, IssueStatus status)
        {
            var report = GetById(id);
            if (report != null)
            {
                report.Status = status;
                return true;
            }
            return false;
        }

        // Getting the total count of issue reports
        public int GetTotalCount()
        {
            return _issues.Count;
        }

        public void Clear()
        {
            
        }

        // Additional methods for processing order
        public IssueReport? GetNextForProcessing()
        {
            return _issues.GetFirst();
        }

        // Get the most recent report
        public IssueReport? GetMostRecent()
        {            
            return _issues.GetLast();
        }

        // Get a specified number of recent reports
        public IEnumerable<IssueReport> GetRecentReports(int count)
        {
            var result = new List<IssueReport>();
            var allReports = GetAll().Reverse().Take(count);
            return allReports;
        }
    }
}