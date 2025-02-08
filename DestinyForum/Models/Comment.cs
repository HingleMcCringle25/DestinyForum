using System;

namespace DestinyForum.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? Content { get; set; }
        public DateTime CreateDate { get; set; }
        public int DiscussionId { get; set; } 
        public Discussion? Discussion { get; set; } 
    }
}
