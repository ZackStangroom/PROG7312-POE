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
    }
}