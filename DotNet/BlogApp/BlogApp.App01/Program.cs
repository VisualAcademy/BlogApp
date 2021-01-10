using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // PM> Install-Package Microsoft.EntityFrameworkCore.SqlServer

        using (var context = new BlogsContext())
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        // SQL Server 개체 탐색기에서 Blogs 데이터베이스와 Blogs, Posts 테이블 확인
    }
}

/// <summary>
/// 블로그: 포스트에 대한 카테고리 역할 또는 다중 블로그 이름 
/// </summary>
public class Blog
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Post> Posts { get; } = new List<Post>();
}

public class Post
{
    public int Id { get; set; }

    public string Title { get; set; }
    public string Content { get; set; }

    public Blog Blog { get; set; }
}

public class BlogsContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseSqlServer("server=(localdb)\\mssqllocaldb;Database=Blogs");
}
