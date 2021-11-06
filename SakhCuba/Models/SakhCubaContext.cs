using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakhCuba.Models
{
    public class SakhCubaContext : IdentityDbContext<User>
    {
        public DbSet<Application> Applications { get; set; }
        public DbSet<Decision> Decisions { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Images> Images { get; set; }
        public SakhCubaContext(DbContextOptions<SakhCubaContext> options)
            : base(options)
        {
            Database.Migrate();
        }
    }
}
