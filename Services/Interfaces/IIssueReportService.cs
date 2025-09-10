using PROG7312_POE.Models;

namespace PROG7312_POE.Services.Interfaces
{
    public interface IIssueReportService
    {
        // managing issue reports
        Task<bool> SubmitIssueAsync(IssueReportViewModel viewModel);
        Task<IssueReport?> GetIssueByIdAsync(string id);
        Task<IEnumerable<IssueReport>> GetAllIssuesAsync();
        Task<IEnumerable<IssueReport>> GetIssuesByCategoryAsync(string category);
        Task<IEnumerable<IssueReport>> GetIssuesByPriorityAsync(IssuePriority priority);
        Task<int> GetTotalIssueCountAsync();
        Task<bool> ProcessNextIssueAsync();
        Task<IssueReport?> GetMostRecentIssueAsync();
    }
}