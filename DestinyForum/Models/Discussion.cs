using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; // Add this using directive
using DestinyForum.Data; // Add this using directive
using System.ComponentModel.DataAnnotations;

namespace DestinyForum.Models
{
    public class Discussion
    {
        public int DiscussionId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        public string ImageFilename { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
