using PROG7312_POE.Models;
using PROG7312_POE.Services.Interfaces;
using System.Collections.Generic;

namespace PROG7312_POE.Services
{
    public class EventRepository : IEventRepository
    {
        // SortedDictionary keeps events sorted by date automatically
        private readonly SortedDictionary<DateTime, List<LocalEvent>> _eventsByDate;
        
        // Dictionary for fast lookup by ID
        private readonly Dictionary<string, LocalEvent> _eventsById;
        
        // Dictionary for grouping by category
        private readonly Dictionary<string, List<LocalEvent>> _eventsByCategory;

        public EventRepository()
        {
            _eventsByDate = new SortedDictionary<DateTime, List<LocalEvent>>();
            _eventsById = new Dictionary<string, LocalEvent>();
            _eventsByCategory = new Dictionary<string, List<LocalEvent>>(StringComparer.OrdinalIgnoreCase);
        }

        public void Add(LocalEvent localEvent)
        {
            if (localEvent == null)
                throw new ArgumentNullException(nameof(localEvent));

            // Add to ID dictionary for O(1) lookups
            _eventsById[localEvent.Id] = localEvent;

            // Add to date sorted dictionary
            var dateKey = localEvent.EventDate.Date;
            if (!_eventsByDate.ContainsKey(dateKey))
            {
                _eventsByDate[dateKey] = new List<LocalEvent>();
            }
            _eventsByDate[dateKey].Add(localEvent);

            // Add to category dictionary
            if (!_eventsByCategory.ContainsKey(localEvent.Category))
            {
                _eventsByCategory[localEvent.Category] = new List<LocalEvent>();
            }
            _eventsByCategory[localEvent.Category].Add(localEvent);
        }

        public LocalEvent? GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            return _eventsById.TryGetValue(id, out var localEvent) ? localEvent : null;
        }

        public IEnumerable<LocalEvent> GetAll()
        {
            return _eventsById.Values.OrderBy(e => e.EventDate);
        }

        public IEnumerable<LocalEvent> GetByCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
                return Enumerable.Empty<LocalEvent>();

            return _eventsByCategory.TryGetValue(category, out var events) 
                ? events.OrderBy(e => e.EventDate) 
                : Enumerable.Empty<LocalEvent>();
        }

        public IEnumerable<LocalEvent> GetUpcoming()
        {
            var today = DateTime.Now.Date;
            
            return _eventsByDate
                .Where(kvp => kvp.Key >= today)
                .SelectMany(kvp => kvp.Value)
                .Where(e => e.Status == EventStatus.Upcoming)
                .OrderBy(e => e.EventDate);
        }

        public IEnumerable<LocalEvent> GetEventsByDateRange(DateTime startDate, DateTime endDate)
        {
            var start = startDate.Date;
            var end = endDate.Date;

            return _eventsByDate
                .Where(kvp => kvp.Key >= start && kvp.Key <= end)
                .SelectMany(kvp => kvp.Value)
                .OrderBy(e => e.EventDate);
        }

        public bool Remove(string id)
        {
            if (!_eventsById.TryGetValue(id, out var localEvent))
                return false;

            // Remove from all dictionaries
            _eventsById.Remove(id);

            var dateKey = localEvent.EventDate.Date;
            if (_eventsByDate.TryGetValue(dateKey, out var dateEvents))
            {
                dateEvents.Remove(localEvent);
                if (dateEvents.Count == 0)
                {
                    _eventsByDate.Remove(dateKey);
                }
            }

            if (_eventsByCategory.TryGetValue(localEvent.Category, out var categoryEvents))
            {
                categoryEvents.Remove(localEvent);
                if (categoryEvents.Count == 0)
                {
                    _eventsByCategory.Remove(localEvent.Category);
                }
            }

            return true;
        }

        public void Clear()
        {
            _eventsByDate.Clear();
            _eventsById.Clear();
            _eventsByCategory.Clear();
        }

        public int GetTotalCount()
        {
            return _eventsById.Count;
        }
    }
}