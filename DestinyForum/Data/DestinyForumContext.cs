using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DestinyForum.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DestinyForum.Data
{
    //I added Identity, but when adding the migration and updating the database, 
    //thats where I get errors.

    //I also created the Comment/Discussion table in sqlserver, updated the connection
    //string in appsettings.json, and changed "UseSqlite" to "UseSqlServer" in program.cs
    public class DestinyForumContext : IdentityDbContext
    {
        public DestinyForumContext (DbContextOptions<DestinyForumContext> options)
            : base(options)
        {
        }

        public DbSet<DestinyForum.Models.Discussion> Discussion { get; set; } = default!;
        public DbSet<DestinyForum.Models.Comment> Comment { get; set; } = default!;
    }
}
