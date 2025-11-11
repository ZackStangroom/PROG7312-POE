using PROG7312_POE.Models;

namespace PROG7312_POE.Services.DataStructures
{
    // Comparer for sorting service requests by priority and date
    // Higher priority requests come first, then sorted by date (oldest first)
    public class ServiceRequestComparer : IComparer<IssueReport>
    {
        public int Compare(IssueReport? x, IssueReport? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return 1;
            if (y == null) return -1;

            // First compare by priority (higher priority first)
            int priorityComparison = GetPriorityValue(y.Priority).CompareTo(GetPriorityValue(x.Priority));
            
            if (priorityComparison != 0)
            {
                return priorityComparison;
            }

            // If priorities are equal, compare by date (older first)
            return x.ReportedDate.CompareTo(y.ReportedDate);
        }

        private int GetPriorityValue(IssuePriority priority)
        {
            return priority switch
            {
                IssuePriority.Emergency => 4,
                IssuePriority.High => 3,
                IssuePriority.Standard => 2,
                IssuePriority.Low => 1,
                _ => 0
            };
        }
    }
}