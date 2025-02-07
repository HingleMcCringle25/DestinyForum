using System;
using System.Collections.Generic;

namespace DestinyForum.Models
{
    public class Discussion
    {
        public int DiscussionId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ImageFilename { get; set; }
        public DateTime CreateDate { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
