// 디버그 뷰
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // PM> Install-Package Microsoft.EntityFrameworkCore.SqlServer

        // Unit of Work 1
        using (var context = new BlogsContext())
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            // SQL Server 개체 탐색기에서 Blogs 데이터베이스와 Blogs, Posts 테이블 확인

            context.Add(new Blog 
            { 
                Name = ".NET Korea",
                Posts = 
                {
                    new Post
                    {
                        Title = "C# 교과서",
                        Content = "C# 교과서 책의 내용입니다. "
                    },
                    new Post
                    {
                        Title = "ASP.NET & Core를 다루는 기술",
                        Content = "ASP.NET & Core를 다루는 기술 책의 내용입니다. "
                    },
                }
            });

            // 쿼리 디버그 2: F9 -> F5 -> Locals -> context -> Model -> DebugView -> ShortView | LongView 
            context.SaveChanges(); // Step Over: Temp 에서 Real 데이터로 변경
        }

        // Unit of Work 2
        using (var context = new BlogsContext())
        {
            var queryable = context.Blogs.Include(b => b.Posts);

            // 쿼리 디버그 1: F9 -> F5 -> Locals -> queryable -> Non-Public members -> _queryable - DebugView -> Query
            var blogs = queryable.ToList(); // 중단점 설정 후 디버깅

            Console.WriteLine();
            Console.WriteLine();

            foreach (var blog in blogs)
            {
                Console.WriteLine($"블로그: {blog.Name}");
                foreach (var post in blog.Posts)
                {
                    Console.WriteLine($"\t포스트: {post.Title}");
                }
            }
        }
    }
}

public class Blog
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();
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
            //.LogTo(Console.WriteLine) //[!] 로깅 
            .LogTo(Console.WriteLine, LogLevel.Information) //[!] 로깅 
            .EnableSensitiveDataLogging() //[!] ? 대신 직접 값을 입력해서 디버깅
            .UseSqlServer("server=(localdb)\\mssqllocaldb;Database=Blogs");
}
