using Microsoft.EntityFrameworkCore;

namespace BlogApp.Models
{
    public class BlogsContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseSqlServer("server=(localdb)\\mssqllocaldb;Database=Blogs"); 
    }
}
