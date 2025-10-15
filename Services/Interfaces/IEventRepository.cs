using PROG7312_POE.Models;

namespace PROG7312_POE.Services.Interfaces
{
    public interface IEventRepository
    {
        void Add(LocalEvent localEvent);
        LocalEvent? GetById(string id);
        IEnumerable<LocalEvent> GetAll();
        IEnumerable<LocalEvent> GetByCategory(string category);
        IEnumerable<LocalEvent> GetUpcoming();
        IEnumerable<LocalEvent> GetEventsByDateRange(DateTime startDate, DateTime endDate);
        bool Remove(string id);
        void Clear();
        int GetTotalCount();
        
        // Hashtable-based methods for efficient filtering
        IEnumerable<string> GetUniqueCategories();
        int GetCategoryCount(string category);
        Dictionary<string, int> GetCategoryCounts();
        IEnumerable<DateTime> GetUniqueDates();
        int GetDateCount(DateTime date);
        Dictionary<DateTime, int> GetDateCounts();
        bool CategoryExists(string category);
        bool HasEventsOnDate(DateTime date);
        int GetUniqueCategoryCount();
        int GetUniqueDateCount();
    }
}