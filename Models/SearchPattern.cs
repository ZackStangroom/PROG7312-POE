namespace PROG7312_POE.Models
{
    /// <summary>
    /// Represents a single search action performed by the user
    /// </summary>
    public class SearchAction
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string? SearchTerm { get; set; }
        public string? Category { get; set; }
        public string? DateRange { get; set; }
        public int ResultsCount { get; set; }
    }

    /// <summary>
    /// Aggregated analysis of user search patterns
    /// </summary>
    public class SearchPatternAnalysis
    {
        public Dictionary<string, int> CategoryFrequency { get; set; } = new();
        public Dictionary<string, int> DateRangeFrequency { get; set; } = new();
        public Dictionary<string, int> SearchTermFrequency { get; set; } = new();
        public List<string> TopCategories { get; set; } = new();
        public List<string> TopDateRanges { get; set; } = new();
        public List<string> TopSearchTerms { get; set; } = new();
        public int TotalSearches { get; set; }
        public DateTime? LastSearchTime { get; set; }
        public double AverageResultsPerSearch { get; set; }
    }

    /// <summary>
    /// Represents an event recommendation based on user patterns
    /// </summary>
    public class EventRecommendation
    {
        public LocalEvent Event { get; set; } = null!;
        public double RelevanceScore { get; set; }
        public List<string> RecommendationReasons { get; set; } = new();
        public string RecommendationType { get; set; } = string.Empty; // "Category", "Date", "Popular", "Similar"
    }
}