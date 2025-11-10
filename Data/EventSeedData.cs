using PROG7312_POE.Models;

namespace PROG7312_POE.Data
{
    /// <summary>
    /// Provides sample event data for seeding the application
    /// </summary>
    public static class EventSeedData
    {
        /// <summary>
        /// Gets a collection of sample local events
        /// </summary>
        /// <returns>List of sample LocalEvent objects</returns>
        public static List<LocalEvent> GetSampleEvents()
        {
            return new List<LocalEvent>
            {
                new LocalEvent
                {
                    Title = "Cape Town Community Clean-Up",
                    Description = "Join us for a community beach clean-up event. Help keep our beaches beautiful!",
                    EventDate = new DateTime(2025, 12, 18, 9, 0, 0),
                    Category = "Community",
                    Location = "Sea Point Promenade",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "Local Arts & Crafts Market",
                    Description = "Discover local artisans and craftspeople showcasing their work.",
                    EventDate = new DateTime(2025, 12, 22, 10, 0, 0),
                    Category = "Arts",
                    Location = "Green Point Park",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "City Council Public Meeting",
                    Description = "Public consultation on upcoming infrastructure projects.",
                    EventDate = new DateTime(2025, 12, 25, 18, 0, 0),
                    Category = "Municipal",
                    Location = "Cape Town Civic Centre",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "Cape Town Marathon",
                    Description = "Annual city marathon featuring 10K, 21K, and 42K routes.",
                    EventDate = new DateTime(2025, 12, 28, 6, 0, 0),
                    Category = "Sports",
                    Location = "City Centre",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "Free Health Screening Day",
                    Description = "Free health screenings including blood pressure, diabetes, and cholesterol.",
                    EventDate = new DateTime(2025, 12, 2, 8, 0, 0),
                    Category = "Health",
                    Location = "Khayelitsha Community Centre",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "Tree Planting Initiative",
                    Description = "Help us plant 500 trees to combat climate change and beautify our city.",
                    EventDate = new DateTime(2025, 12, 5, 9, 0, 0),
                    Category = "Environment",
                    Location = "Newlands Forest",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "Youth Soccer Tournament",
                    Description = "Under-16 soccer tournament with teams from across the Western Cape.",
                    EventDate = new DateTime(2025, 12, 20, 14, 0, 0),
                    Category = "Sports",
                    Location = "Athlone Stadium",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "Digital Skills Workshop",
                    Description = "Free workshop teaching basic computer skills and internet safety for seniors.",
                    EventDate = new DateTime(2025, 12, 24, 10, 0, 0),
                    Category = "Education",
                    Location = "Mitchell's Plain Library",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "Water Conservation Awareness Day",
                    Description = "Learn practical tips for saving water and protecting our precious resources.",
                    EventDate = new DateTime(2025, 12, 8, 9, 0, 0),
                    Category = "Environment",
                    Location = "Company's Garden",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "Jazz Festival at the Waterfront",
                    Description = "Evening of live jazz music featuring local and international artists.",
                    EventDate = new DateTime(2025, 12, 1, 18, 30, 0),
                    Category = "Arts",
                    Location = "V&A Waterfront Amphitheatre",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "Neighborhood Watch Meeting",
                    Description = "Monthly community safety meeting to discuss crime prevention strategies.",
                    EventDate = new DateTime(2025, 12, 19, 19, 0, 0),
                    Category = "Safety",
                    Location = "Constantia Community Hall",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "Mental Health Awareness Workshop",
                    Description = "Free workshop on recognizing and managing stress, anxiety, and depression.",
                    EventDate = new DateTime(2025, 12, 6, 14, 0, 0),
                    Category = "Health",
                    Location = "Groote Schuur Hospital Auditorium",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "Small Business Development Seminar",
                    Description = "Learn about funding opportunities, business planning, and growth strategies.",
                    EventDate = new DateTime(2025, 12, 26, 9, 0, 0),
                    Category = "Education",
                    Location = "Cape Town International Convention Centre",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "Heritage Day Celebration",
                    Description = "Celebrate South African culture with traditional food, music, and dance.",
                    EventDate = new DateTime(2025, 12, 9, 11, 0, 0),
                    Category = "Community",
                    Location = "Grand Parade",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "Photography Exhibition: Cape Town Through the Lens",
                    Description = "Stunning photography exhibition showcasing the beauty and diversity of Cape Town.",
                    EventDate = new DateTime(2025, 12, 30, 10, 0, 0),
                    Category = "Arts",
                    Location = "South African National Gallery",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "Cycling Safety Workshop",
                    Description = "Learn about road safety, bike maintenance, and cycling etiquette in the city.",
                    EventDate = new DateTime(2025, 12, 3, 8, 0, 0),
                    Category = "Sports",
                    Location = "Sea Point Pavilion",
                    Status = EventStatus.Upcoming
                },
                new LocalEvent
                {
                    Title = "Budget Consultation Public Forum",
                    Description = "Have your say on the city's upcoming budget priorities and spending plans.",
                    EventDate = new DateTime(2025, 12, 7, 18, 0, 0),
                    Category = "Municipal",
                    Location = "Cape Town Civic Centre",
                    Status = EventStatus.Upcoming
                }
            };
        }
    }
}