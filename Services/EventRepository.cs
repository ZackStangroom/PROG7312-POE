using PROG7312_POE.Models;
using PROG7312_POE.Services.Interfaces;
using System.Collections;
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

        // Hashtable for unique categories with event counts (O(1) lookup)
        private readonly Hashtable _categoryHashtable;
        
        // Hashtable for unique dates with event counts (O(1) lookup)
        private readonly Hashtable _dateHashtable;

        public EventRepository()
        {
            _eventsByDate = new SortedDictionary<DateTime, List<LocalEvent>>();
            _eventsById = new Dictionary<string, LocalEvent>();
            _eventsByCategory = new Dictionary<string, List<LocalEvent>>(StringComparer.OrdinalIgnoreCase);
            _categoryHashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
            _dateHashtable = new Hashtable();
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

            // Update category hashtable (category -> count)
            if (_categoryHashtable.ContainsKey(localEvent.Category))
            {
                _categoryHashtable[localEvent.Category] = (int)_categoryHashtable[localEvent.Category]! + 1;
            }
            else
            {
                _categoryHashtable[localEvent.Category] = 1;
            }

            // Update date hashtable (date -> count)
            string dateString = dateKey.ToString("yyyy-MM-dd");
            if (_dateHashtable.ContainsKey(dateString))
            {
                _dateHashtable[dateString] = (int)_dateHashtable[dateString]! + 1;
            }
            else
            {
                _dateHashtable[dateString] = 1;
            }
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

            // Update category hashtable
            if (_categoryHashtable.ContainsKey(localEvent.Category))
            {
                int count = (int)_categoryHashtable[localEvent.Category]!;
                if (count <= 1)
                {
                    _categoryHashtable.Remove(localEvent.Category);
                }
                else
                {
                    _categoryHashtable[localEvent.Category] = count - 1;
                }
            }

            // Update date hashtable
            string dateString = dateKey.ToString("yyyy-MM-dd");
            if (_dateHashtable.ContainsKey(dateString))
            {
                int count = (int)_dateHashtable[dateString]!;
                if (count <= 1)
                {
                    _dateHashtable.Remove(dateString);
                }
                else
                {
                    _dateHashtable[dateString] = count - 1;
                }
            }

            return true;
        }

        public void Clear()
        {
            _eventsByDate.Clear();
            _eventsById.Clear();
            _eventsByCategory.Clear();
            _categoryHashtable.Clear();
            _dateHashtable.Clear();
        }

        public int GetTotalCount()
        {
            return _eventsById.Count;
        }

        // New methods for hashtable access

        /// <summary>
        /// Gets all unique categories using hashtable for O(1) lookup
        /// </summary>
        public IEnumerable<string> GetUniqueCategories()
        {
            return _categoryHashtable.Keys.Cast<string>().OrderBy(c => c);
        }

        /// <summary>
        /// Gets the event count for a specific category using hashtable (O(1))
        /// </summary>
        public int GetCategoryCount(string category)
        {
            return _categoryHashtable.ContainsKey(category) 
                ? (int)_categoryHashtable[category]! 
                : 0;
        }

        /// <summary>
        /// Gets all categories with their counts as a dictionary
        /// </summary>
        public Dictionary<string, int> GetCategoryCounts()
        {
            var result = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (DictionaryEntry entry in _categoryHashtable)
            {
                result[(string)entry.Key] = (int)entry.Value!;
            }
            return result;
        }

        /// <summary>
        /// Gets all unique dates using hashtable
        /// </summary>
        public IEnumerable<DateTime> GetUniqueDates()
        {
            return _dateHashtable.Keys
                .Cast<string>()
                .Select(d => DateTime.Parse(d))
                .OrderBy(d => d);
        }

        /// <summary>
        /// Gets the event count for a specific date using hashtable (O(1))
        /// </summary>
        public int GetDateCount(DateTime date)
        {
            string dateString = date.Date.ToString("yyyy-MM-dd");
            return _dateHashtable.ContainsKey(dateString) 
                ? (int)_dateHashtable[dateString]! 
                : 0;
        }

        /// <summary>
        /// Gets all dates with their counts as a dictionary
        /// </summary>
        public Dictionary<DateTime, int> GetDateCounts()
        {
            var result = new Dictionary<DateTime, int>();
            foreach (DictionaryEntry entry in _dateHashtable)
            {
                DateTime date = DateTime.Parse((string)entry.Key);
                result[date] = (int)entry.Value!;
            }
            return result.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        /// <summary>
        /// Checks if a category exists in hashtable (O(1))
        /// </summary>
        public bool CategoryExists(string category)
        {
            return _categoryHashtable.ContainsKey(category);
        }

        /// <summary>
        /// Checks if events exist on a specific date (O(1))
        /// </summary>
        public bool HasEventsOnDate(DateTime date)
        {
            string dateString = date.Date.ToString("yyyy-MM-dd");
            return _dateHashtable.ContainsKey(dateString);
        }

        /// <summary>
        /// Gets total number of unique categories
        /// </summary>
        public int GetUniqueCategoryCount()
        {
            return _categoryHashtable.Count;
        }

        /// <summary>
        /// Gets total number of unique dates
        /// </summary>
        public int GetUniqueDateCount()
        {
            return _dateHashtable.Count;
        }
    }
}