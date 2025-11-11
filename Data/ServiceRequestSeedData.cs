using PROG7312_POE.Models;

namespace PROG7312_POE.Data
{
    // Provides sample service request data for seeding the application
    public static class ServiceRequestSeedData
    {
        // Gets a collection of sample service requests
        // List of sample IssueReport objects
        public static List<IssueReport> GetSampleServiceRequests()
        {
            var random = new Random(42); // Fixed seed for consistent data
            var requests = new List<IssueReport>();

            // Emergency Priority Requests
            requests.Add(CreateRequest(
                "Corner of Main Road and 5th Avenue, Rondebosch",
                "Electricity",
                "Major power outage affecting entire street block. Multiple households without electricity for over 6 hours. Traffic lights also not functioning.",
                DateTime.Now.AddDays(-2).AddHours(-3),
                IssueStatus.InProgress,
                IssuePriority.Emergency
            ));

            requests.Add(CreateRequest(
                "Nelson Mandela Boulevard near Hospital",
                "Water & Sanitation",
                "Large water main burst causing flooding on the road. Water gushing out continuously, affecting traffic flow and nearby businesses.",
                DateTime.Now.AddDays(-1).AddHours(-5),
                IssueStatus.InProgress,
                IssuePriority.Emergency
            ));

            requests.Add(CreateRequest(
                "Klipfontein Road, Athlone",
                "Emergency Services",
                "Dangerous sinkhole has appeared on main road, approximately 2 meters wide and growing. Road partially blocked and poses serious safety risk.",
                DateTime.Now.AddDays(-1),
                IssueStatus.UnderReview,
                IssuePriority.Emergency
            ));

            // High Priority Requests
            requests.Add(CreateRequest(
                "Sea Point Promenade Parking Area",
                "Water & Sanitation",
                "Sewage overflow in public parking area. Strong odor and unsanitary conditions affecting public spaces and nearby restaurants.",
                DateTime.Now.AddDays(-3),
                IssueStatus.InProgress,
                IssuePriority.High
            ));

            requests.Add(CreateRequest(
                "Durban Road Traffic Circle, Bellville",
                "Electricity",
                "Street lights not working for past week. Creates dangerous conditions for pedestrians and motorists during night hours.",
                DateTime.Now.AddDays(-7),
                IssueStatus.Received,
                IssuePriority.High
            ));

            requests.Add(CreateRequest(
                "Brooklyn Road, Milnerton",
                "Roads & Transport",
                "Large pothole cluster causing vehicle damage. Multiple residents have reported tire damage. Approximately 5-6 deep potholes in 50m stretch.",
                DateTime.Now.AddDays(-4),
                IssueStatus.InProgress,
                IssuePriority.High
            ));

            requests.Add(CreateRequest(
                "Khayelitsha Site B Community Centre",
                "Water & Sanitation",
                "Community water tap broken and leaking continuously. Significant water wastage and muddy conditions affecting accessibility.",
                DateTime.Now.AddDays(-5),
                IssueStatus.UnderReview,
                IssuePriority.High
            ));

            requests.Add(CreateRequest(
                "Voortrekker Road, Parow",
                "Electricity",
                "Damaged electrical pole leaning dangerously over sidewalk after recent storm. Wires hanging low, potential safety hazard.",
                DateTime.Now.AddDays(-2),
                IssueStatus.Received,
                IssuePriority.High
            ));

            // Standard Priority Requests
            requests.Add(CreateRequest(
                "Hanover Street, District Six",
                "Waste Management",
                "Overflowing public waste bins not collected for over a week. Creating litter problem and attracting pests.",
                DateTime.Now.AddDays(-8),
                IssueStatus.Received,
                IssuePriority.Standard
            ));

            requests.Add(CreateRequest(
                "Long Street, City Centre",
                "Roads & Transport",
                "Faded road markings making it difficult to see lanes, especially at night. Needs repainting for safety.",
                DateTime.Now.AddDays(-10),
                IssueStatus.Received,
                IssuePriority.Standard
            ));

            requests.Add(CreateRequest(
                "Rondebosch Common",
                "Parks & Recreation",
                "Broken swings and damaged playground equipment at children's play area. Equipment appears unsafe and needs repair or replacement.",
                DateTime.Now.AddDays(-6),
                IssueStatus.Resolved,
                IssuePriority.Standard
            ));

            requests.Add(CreateRequest(
                "Mitchell's Plain Town Centre",
                "Waste Management",
                "Illegal dumping site developing near shopping centre. Large amounts of building rubble and household waste accumulating.",
                DateTime.Now.AddDays(-12),
                IssueStatus.InProgress,
                IssuePriority.Standard
            ));

            requests.Add(CreateRequest(
                "Wynberg Main Road",
                "Roads & Transport",
                "Damaged sidewalk paving creating tripping hazards. Several loose and broken paving stones need replacement.",
                DateTime.Now.AddDays(-9),
                IssueStatus.Received,
                IssuePriority.Standard
            ));

            requests.Add(CreateRequest(
                "Claremont Station Taxi Rank",
                "Housing",
                "Graffiti covering multiple building walls and public structures. Area looking neglected and affecting community appearance.",
                DateTime.Now.AddDays(-15),
                IssueStatus.Resolved,
                IssuePriority.Standard
            ));

            requests.Add(CreateRequest(
                "Green Point Urban Park",
                "Waste Management",
                "Dog waste bins full and not being emptied regularly. Causing unpleasant conditions in popular walking area.",
                DateTime.Now.AddDays(-5),
                IssueStatus.UnderReview,
                IssuePriority.Standard
            ));

            requests.Add(CreateRequest(
                "Camps Bay Beach Access Road",
                "Roads & Transport",
                "Parking meter displaying error message and not accepting payment. Causing confusion for visitors.",
                DateTime.Now.AddDays(-3),
                IssueStatus.Received,
                IssuePriority.Standard
            ));

            // Low Priority Requests
            requests.Add(CreateRequest(
                "Observatory Community Garden",
                "Parks & Recreation",
                "Overgrown grass and weeds in public garden area. Needs general maintenance and tidying up.",
                DateTime.Now.AddDays(-14),
                IssueStatus.Resolved,
                IssuePriority.Low
            ));

            requests.Add(CreateRequest(
                "Table View Beachfront",
                "Parks & Recreation",
                "Faded and peeling paint on public benches along promenade. Benches still functional but appearance could be improved.",
                DateTime.Now.AddDays(-20),
                IssueStatus.Received,
                IssuePriority.Low
            ));

            requests.Add(CreateRequest(
                "Constantia Nek Picnic Area",
                "Parks & Recreation",
                "Missing information board at popular hiking trail start point. Hikers asking for trail maps and directions.",
                DateTime.Now.AddDays(-11),
                IssueStatus.Received,
                IssuePriority.Low
            ));

            requests.Add(CreateRequest(
                "Hout Bay Harbor Public Parking",
                "Other",
                "Faded parking bay markings in public parking area. Lines barely visible, causing parking confusion.",
                DateTime.Now.AddDays(-16),
                IssueStatus.Resolved,
                IssuePriority.Low
            ));

            requests.Add(CreateRequest(
                "Somerset West Community Park",
                "Parks & Recreation",
                "Public notice board glass cracked and notices getting wet. Board still readable but needs maintenance.",
                DateTime.Now.AddDays(-13),
                IssueStatus.Received,
                IssuePriority.Low
            ));

            requests.Add(CreateRequest(
                "Fish Hoek Beach",
                "Parks & Recreation",
                "Rusted bicycle rack near beach changing rooms. Still functional but showing signs of corrosion from salt air.",
                DateTime.Now.AddDays(-18),
                IssueStatus.UnderReview,
                IssuePriority.Low
            ));

            requests.Add(CreateRequest(
                "Newlands Cricket Ground Surrounds",
                "Other",
                "Faded street name sign difficult to read. Visitors having trouble locating correct street.",
                DateTime.Now.AddDays(-25),
                IssueStatus.Received,
                IssuePriority.Low
            ));

            // Additional varied requests
            requests.Add(CreateRequest(
                "Goodwood Industrial Area",
                "Public Safety",
                "Broken fence at abandoned lot allowing easy access. Concern about safety and potential unauthorized activities.",
                DateTime.Now.AddDays(-7),
                IssueStatus.InProgress,
                IssuePriority.Standard
            ));

            requests.Add(CreateRequest(
                "Muizenberg Beach Pavilion",
                "Water & Sanitation",
                "Public bathroom facilities require maintenance. Taps leaking and toilets not flushing properly.",
                DateTime.Now.AddDays(-6),
                IssueStatus.UnderReview,
                IssuePriority.High
            ));

            return requests;
        }

        // Helper method to create a service request with specific parameters
        private static IssueReport CreateRequest(
            string location,
            string category,
            string description,
            DateTime reportedDate,
            IssueStatus status,
            IssuePriority priority)
        {
            return new IssueReport
            {
                Id = Guid.NewGuid().ToString(),
                Location = location,
                Category = category,
                Description = description,
                ReportedDate = reportedDate,
                Status = status,
                Priority = priority,
                MediaAttachmentPath = null,
                MediaAttachmentFileName = null,
                MediaAttachmentContentType = null
            };
        }
    }
}