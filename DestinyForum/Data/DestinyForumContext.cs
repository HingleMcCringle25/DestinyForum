using Microsoft.EntityFrameworkCore;
using DestinyForum.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DestinyForum.Data
{
    public class DestinyForumContext : IdentityDbContext<ApplicationUser>
    {
        public DestinyForumContext(DbContextOptions<DestinyForumContext> options)
            : base(options)
        {
        }

        public DbSet<Discussion> Discussion { get; set; } = default!;
        public DbSet<Comment> Comment { get; set; } = default!;
    }
}
