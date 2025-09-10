using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PROG7312_POE.Models
{
    public class IssueReportViewModel
    {
        // Properties for the issue report form
        [Required(ErrorMessage = "Location is required")]
        [Display(Name = "Location")]
        public string Location { get; set; } = string.Empty;

        // List of predefined categories
        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public string Category { get; set; } = string.Empty;

        // Predefined categories for the dropdown
        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        // Property to hold the uploaded media file
        [Display(Name = "Media Attachment")]
        public IFormFile? MediaAttachment { get; set; }

        public bool IsSubmitted { get; set; }
        public string? SubmissionMessage { get; set; }
    }
}