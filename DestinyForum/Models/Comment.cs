using System;

namespace DestinyForum.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? Content { get; set; }
        public DateTime CreateDate { get; set; }
        public int DiscussionId { get; set; } //Foreign key. the discussion id this comment belongs to
        public Discussion? Discussion { get; set; } //navigation property. a comment belongs to one discussion
    }
}
