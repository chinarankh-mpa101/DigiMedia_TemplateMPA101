using DigiMedia_Template.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiMedia_Template.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}

