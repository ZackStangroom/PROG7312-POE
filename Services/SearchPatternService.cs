using PROG7312_POE.Models;
using PROG7312_POE.Services.Interfaces;
using System.Collections;

namespace PROG7312_POE.Services
{
    public class SearchPatternService : ISearchPatternService
    {
        private readonly IEventRepository _eventRepository;
        
        // Stack for search history (LIFO - most recent first)
        private readonly Stack<SearchAction> _searchHistory;
        
        // Hashtable for category search frequency (O(1) lookup and update)
        private readonly Hashtable _categoryFrequency;
        
        // Hashtable for date range search frequency
        private readonly Hashtable _dateRangeFrequency;
        
        // Hashtable for search term frequency
        private readonly Hashtable _searchTermFrequency;
        
        // Dictionary for quick access to all searches
        private readonly Dictionary<string, SearchAction> _searchesById;
        
        private const int MAX_HISTORY_SIZE = 100;
        private const double CATEGORY_WEIGHT = 0.4;
        private const double DATE_WEIGHT = 0.3;
        private const double POPULARITY_WEIGHT = 0.2;
        private const double RECENCY_WEIGHT = 0.1;

        public SearchPatternService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
            _searchHistory = new Stack<SearchAction>();
            _categoryFrequency = new Hashtable(StringComparer.OrdinalIgnoreCase);
            _dateRangeFrequency = new Hashtable(StringComparer.OrdinalIgnoreCase);
            _searchTermFrequency = new Hashtable(StringComparer.OrdinalIgnoreCase);
            _searchesById = new Dictionary<string, SearchAction>();
        }

        public void TrackSearch(SearchAction searchAction)
        {
            if (searchAction == null)
                throw new ArgumentNullException(nameof(searchAction));

            // Push to stack (most recent on top)
            _searchHistory.Push(searchAction);
            _searchesById[searchAction.Id] = searchAction;

            // Maintain max size by removing oldest if needed
            if (_searchHistory.Count > MAX_HISTORY_SIZE)
            {
                var oldest = _searchHistory.Last();
                _searchesById.Remove(oldest.Id);
            }

            // Update category frequency hashtable (O(1))
            if (!string.IsNullOrEmpty(searchAction.Category))
            {
                UpdateFrequency(_categoryFrequency, searchAction.Category);
            }

            // Update date range frequency hashtable (O(1))
            if (!string.IsNullOrEmpty(searchAction.DateRange))
            {
                UpdateFrequency(_dateRangeFrequency, searchAction.DateRange);
            }

            // Update search term frequency hashtable (O(1))
            if (!string.IsNullOrEmpty(searchAction.SearchTerm))
            {
                var normalizedTerm = searchAction.SearchTerm.ToLower().Trim();
                if (normalizedTerm.Length >= 3) // Only track terms with 3+ characters
                {
                    UpdateFrequency(_searchTermFrequency, normalizedTerm);
                }
            }
        }

        private void UpdateFrequency(Hashtable hashtable, string key)
        {
            if (hashtable.ContainsKey(key))
            {
                hashtable[key] = (int)hashtable[key]! + 1;
            }
            else
            {
                hashtable[key] = 1;
            }
        }

        public IEnumerable<SearchAction> GetSearchHistory(int limit = 50)
        {
            return _searchHistory.Take(limit);
        }

        public SearchPatternAnalysis AnalyzePatterns()
        {
            var analysis = new SearchPatternAnalysis
            {
                TotalSearches = _searchHistory.Count,
                LastSearchTime = _searchHistory.Any() ? _searchHistory.Peek().Timestamp : null
            };

            // Convert hashtables to dictionaries for analysis
            analysis.CategoryFrequency = HashtableToDictionary(_categoryFrequency);
            analysis.DateRangeFrequency = HashtableToDictionary(_dateRangeFrequency);
            analysis.SearchTermFrequency = HashtableToDictionary(_searchTermFrequency);

            // Get top categories (sorted by frequency)
            analysis.TopCategories = analysis.CategoryFrequency
                .OrderByDescending(kvp => kvp.Value)
                .Take(5)
                .Select(kvp => kvp.Key)
                .ToList();

            // Get top date ranges
            analysis.TopDateRanges = analysis.DateRangeFrequency
                .OrderByDescending(kvp => kvp.Value)
                .Take(3)
                .Select(kvp => kvp.Key)
                .ToList();

            // Get top search terms
            analysis.TopSearchTerms = analysis.SearchTermFrequency
                .OrderByDescending(kvp => kvp.Value)
                .Take(5)
                .Select(kvp => kvp.Key)
                .ToList();

            // Calculate average results per search
            var searchesWithResults = _searchHistory.Where(s => s.ResultsCount > 0);
            analysis.AverageResultsPerSearch = searchesWithResults.Any() 
                ? searchesWithResults.Average(s => s.ResultsCount) 
                : 0;

            return analysis;
        }

        public IEnumerable<EventRecommendation> GetRecommendedEvents(int maxResults = 10)
        {
            var analysis = AnalyzePatterns();
            var allEvents = _eventRepository.GetAll().ToList();
            var recommendations = new List<EventRecommendation>();

            // If no search history, return trending/upcoming events
            if (analysis.TotalSearches == 0)
            {
                return GetTrendingEvents(maxResults);
            }

            foreach (var evt in allEvents)
            {
                var recommendation = CalculateRecommendation(evt, analysis);
                if (recommendation.RelevanceScore > 0)
                {
                    recommendations.Add(recommendation);
                }
            }

            // Sort by relevance score and return top results
            return recommendations
                .OrderByDescending(r => r.RelevanceScore)
                .Take(maxResults);
        }

        private EventRecommendation CalculateRecommendation(LocalEvent evt, SearchPatternAnalysis analysis)
        {
            var recommendation = new EventRecommendation
            {
                Event = evt,
                RelevanceScore = 0
            };

            double categoryScore = 0;
            double dateScore = 0;
            double popularityScore = 0;
            double recencyScore = 0;

            // 1. Category matching (40% weight)
            if (analysis.CategoryFrequency.ContainsKey(evt.Category))
            {
                int frequency = analysis.CategoryFrequency[evt.Category];
                categoryScore = (double)frequency / analysis.TotalSearches;
                recommendation.RecommendationReasons.Add($"Matches your interest in {evt.Category}");
                recommendation.RecommendationType = "Category Match";
            }

            // 2. Date preference matching (30% weight)
            var daysUntilEvent = (evt.EventDate.Date - DateTime.Now.Date).Days;
            
            foreach (var dateRange in analysis.TopDateRanges)
            {
                bool matches = dateRange.ToLower() switch
                {
                    "today" => daysUntilEvent == 0,
                    "week" => daysUntilEvent >= 0 && daysUntilEvent <= 7,
                    "month" => daysUntilEvent >= 0 && daysUntilEvent <= 30,
                    "upcoming" => daysUntilEvent >= 0,
                    _ => false
                };

                if (matches)
                {
                    int frequency = analysis.DateRangeFrequency[dateRange];
                    dateScore = Math.Max(dateScore, (double)frequency / analysis.TotalSearches);
                    recommendation.RecommendationReasons.Add($"Matches your preferred timeframe: {dateRange}");
                }
            }

            // 3. Popularity score based on category (20% weight)
            int categoryEventCount = _eventRepository.GetCategoryCount(evt.Category);
            int totalEvents = _eventRepository.GetTotalCount();
            popularityScore = totalEvents > 0 ? (double)categoryEventCount / totalEvents : 0;

            // 4. Recency bonus for upcoming events (10% weight)
            if (evt.Status == EventStatus.Upcoming && daysUntilEvent >= 0 && daysUntilEvent <= 14)
            {
                recencyScore = 1.0 - (daysUntilEvent / 14.0);
                recommendation.RecommendationReasons.Add("Coming soon");
            }

            // Calculate weighted total score
            recommendation.RelevanceScore = 
                (categoryScore * CATEGORY_WEIGHT) +
                (dateScore * DATE_WEIGHT) +
                (popularityScore * POPULARITY_WEIGHT) +
                (recencyScore * RECENCY_WEIGHT);

            // Boost score if multiple criteria match
            if (categoryScore > 0 && dateScore > 0)
            {
                recommendation.RelevanceScore *= 1.2; // 20% boost
                recommendation.RecommendationType = "Perfect Match";
            }

            return recommendation;
        }

        public IEnumerable<EventRecommendation> GetRecommendationsByCategory(string category, int maxResults = 5)
        {
            var events = _eventRepository.GetByCategory(category);
            var recommendations = new List<EventRecommendation>();

            foreach (var evt in events.Take(maxResults))
            {
                recommendations.Add(new EventRecommendation
                {
                    Event = evt,
                    RelevanceScore = 1.0,
                    RecommendationReasons = new List<string> { $"From {category} category" },
                    RecommendationType = "Category"
                });
            }

            return recommendations;
        }

        public IEnumerable<EventRecommendation> GetRecommendationsByDatePreference(int maxResults = 5)
        {
            var analysis = AnalyzePatterns();
            var topDateRange = analysis.TopDateRanges.FirstOrDefault();

            if (string.IsNullOrEmpty(topDateRange))
            {
                return GetTrendingEvents(maxResults);
            }

            var events = topDateRange.ToLower() switch
            {
                "today" => _eventRepository.GetEventsByDateRange(DateTime.Now.Date, DateTime.Now.Date),
                "week" => _eventRepository.GetEventsByDateRange(DateTime.Now.Date, DateTime.Now.Date.AddDays(7)),
                "month" => _eventRepository.GetEventsByDateRange(DateTime.Now.Date, DateTime.Now.Date.AddDays(30)),
                "upcoming" => _eventRepository.GetUpcoming(),
                _ => _eventRepository.GetUpcoming()
            };

            return events.Take(maxResults).Select(evt => new EventRecommendation
            {
                Event = evt,
                RelevanceScore = 0.8,
                RecommendationReasons = new List<string> { $"Matches your {topDateRange} searches" },
                RecommendationType = "Date Preference"
            });
        }

        public IEnumerable<EventRecommendation> GetTrendingEvents(int maxResults = 5)
        {
            var upcomingEvents = _eventRepository.GetUpcoming()
                .Take(maxResults)
                .ToList();

            return upcomingEvents.Select(evt => new EventRecommendation
            {
                Event = evt,
                RelevanceScore = 0.7,
                RecommendationReasons = new List<string> { "Popular upcoming event" },
                RecommendationType = "Trending"
            });
        }

        public void ClearHistory()
        {
            _searchHistory.Clear();
            _searchesById.Clear();
            _categoryFrequency.Clear();
            _dateRangeFrequency.Clear();
            _searchTermFrequency.Clear();
        }

        public int GetTotalSearches()
        {
            return _searchHistory.Count;
        }

        public Dictionary<string, int> GetCategorySearchCounts()
        {
            return HashtableToDictionary(_categoryFrequency);
        }

        public Dictionary<string, int> GetDateRangeSearchCounts()
        {
            return HashtableToDictionary(_dateRangeFrequency);
        }

        private Dictionary<string, int> HashtableToDictionary(Hashtable hashtable)
        {
            var dictionary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (DictionaryEntry entry in hashtable)
            {
                dictionary[(string)entry.Key] = (int)entry.Value!;
            }
            return dictionary;
        }
    }
}