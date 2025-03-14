using System;
using System.ComponentModel.DataAnnotations.Schema; 
using DestinyForum.Data; 

namespace DestinyForum.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public int DiscussionId { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty;

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public Discussion? Discussion { get; set; }
    }
}
