using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DestinyForum.Models;

namespace DestinyForum.Data
{
    public class DestinyForumContext : DbContext
    {
        public DestinyForumContext (DbContextOptions<DestinyForumContext> options)
            : base(options)
        {
        }

        public DbSet<DestinyForum.Models.Discussion> Discussion { get; set; } = default!;
        public DbSet<DestinyForum.Models.Comment> Comment { get; set; } = default!;
    }
}
