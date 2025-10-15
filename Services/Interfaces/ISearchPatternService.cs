using PROG7312_POE.Models;

namespace PROG7312_POE.Services.Interfaces
{
    public interface ISearchPatternService
    {
        // Track search actions
        void TrackSearch(SearchAction searchAction);
        
        // Get search history
        IEnumerable<SearchAction> GetSearchHistory(int limit = 50);
        
        // Analyze patterns using hashtable for O(1) lookups
        SearchPatternAnalysis AnalyzePatterns();
        
        // Get personalized event recommendations based on search patterns
        IEnumerable<EventRecommendation> GetRecommendedEvents(int maxResults = 10);
        
        // Get recommendations by specific criteria
        IEnumerable<EventRecommendation> GetRecommendationsByCategory(string category, int maxResults = 5);
        IEnumerable<EventRecommendation> GetRecommendationsByDatePreference(int maxResults = 5);
        IEnumerable<EventRecommendation> GetTrendingEvents(int maxResults = 5);
        
        // Clear search history
        void ClearHistory();
        
        // Get statistics
        int GetTotalSearches();
        Dictionary<string, int> GetCategorySearchCounts();
        Dictionary<string, int> GetDateRangeSearchCounts();
    }
}