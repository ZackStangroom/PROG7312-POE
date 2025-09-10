using PROG7312_POE.Models;

namespace PROG7312_POE.Services.Interfaces
{
    // repository interface for managing issue reports
    // provides CRUD operations and filtering capabilities
    public interface IIssueReportRepository
    {

        // adds a new issue report
        void Add(IssueReport report);

        // retrieves an issue report by its ID
        IssueReport? GetById(string id);

        // retrieves all issue reports
        IEnumerable<IssueReport> GetAll();

        // retrieves issue reports by category
        IEnumerable<IssueReport> GetByCategory(string category);

        // retrieves issue reports by priority
        IEnumerable<IssueReport> GetByPriority(IssuePriority priority);

        // retrieves issue reports by status
        IEnumerable<IssueReport> GetByStatus(IssueStatus status);

        // updates the status of an issue report
        bool UpdateStatus(string id, IssueStatus status);

        // gets the total count of issue reports
        int GetTotalCount();

        // clears all issue reports (for testing purposes)
        void Clear();
    }
}