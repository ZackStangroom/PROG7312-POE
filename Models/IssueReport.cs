using System.ComponentModel.DataAnnotations;
using PROG7312_POE.Models;

namespace PROG7312_POE.Models
{
    public class IssueReport
    {
        public string Id { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string? MediaAttachmentPath { get; set; } 
        public string? MediaAttachmentFileName { get; set; } 
        public string? MediaAttachmentContentType { get; set; } // Add content type
        public DateTime ReportedDate { get; set; }
        public IssueStatus Status { get; set; }
        public IssuePriority Priority { get; set; }

        // Default constructor
        public IssueReport()
        {
            Id = Guid.NewGuid().ToString();
            ReportedDate = DateTime.Now;
            Status = IssueStatus.Received;
            Location = string.Empty;
            Category = string.Empty;
            Description = string.Empty;
        }

        // Parameterized constructor
        public IssueReport(string location, string category, string description, string? mediaAttachmentPath = null, string? mediaAttachmentFileName = null, string? mediaAttachmentContentType = null)
        {
            Id = Guid.NewGuid().ToString();
            Location = location;
            Category = category;
            Description = description;
            MediaAttachmentPath = mediaAttachmentPath;
            MediaAttachmentFileName = mediaAttachmentFileName;
            MediaAttachmentContentType = mediaAttachmentContentType;
            ReportedDate = DateTime.Now;
            Status = IssueStatus.Received;
            Priority = DeterminePriority(category);
        }

        // priority based on category
        private IssuePriority DeterminePriority(string category)
        {
            return category switch
            {
                "Water & Sanitation" => IssuePriority.High,
                "Electricity" => IssuePriority.High,
                "Roads & Transport" => IssuePriority.Standard,
                "Waste Management" => IssuePriority.Standard,
                "Parks & Recreation" => IssuePriority.Low,
                "Housing" => IssuePriority.Standard,
                _ => IssuePriority.Standard
            };
        }

        // text for enums
        public string GetPriorityText()
        {
            return Priority switch
            {
                IssuePriority.Emergency => "Emergency",
                IssuePriority.High => "High Priority",
                IssuePriority.Standard => "Standard",
                IssuePriority.Low => "Low Priority",
                _ => "Standard"
            };
        }

        // text for enums
        public string GetStatusText()
        {
            return Status switch
            {
                IssueStatus.Received => "Report Received",
                IssueStatus.UnderReview => "Under Review",
                IssueStatus.InProgress => "In Progress",
                IssueStatus.Resolved => "Resolved",
                _ => "Unknown"
            };
        }
    }

    // enums for status and priority
    public enum IssueStatus
    {
        Received,
        UnderReview,
        InProgress,
        Resolved
    }

    // Priority levels
    public enum IssuePriority
    {
        Emergency,
        High,
        Standard,
        Low
    }
}