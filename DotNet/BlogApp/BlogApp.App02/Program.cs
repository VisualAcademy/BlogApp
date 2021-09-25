using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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

                    //// 5개 이상 입력 배치 작업? 

                    //new Post
                    //{
                    //    Title = "C# 교과서",
                    //    Content = "C# 교과서 책의 내용입니다. "
                    //},
                    //new Post
                    //{
                    //    Title = "ASP.NET & Core를 다루는 기술",
                    //    Content = "ASP.NET & Core를 다루는 기술 책의 내용입니다. "
                    //},
                    //new Post
                    //{
                    //    Title = "C# 교과서",
                    //    Content = "C# 교과서 책의 내용입니다. "
                    //},
                    //new Post
                    //{
                    //    Title = "ASP.NET & Core를 다루는 기술",
                    //    Content = "ASP.NET & Core를 다루는 기술 책의 내용입니다. "
                    //},
                }
            });

            context.SaveChanges();
        }

        // SQL Server 개체 탐색기에서 Blogs 데이터베이스와 Blogs, Posts 테이블 확인
    }
}

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

    public Blog Blog { get; set; } // public int BlogId { get; set; }
}

public class BlogsContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            //.LogTo(Console.WriteLine) //[1] 로깅 
            //.LogTo(Console.WriteLine, LogLevel.Information) //[2] 로깅 
            .LogTo(Console.WriteLine, LogLevel.Debug) //[3] 로깅 
            .EnableSensitiveDataLogging() //[!] ? 대신 직접 값을 입력해서 디버깅
            .UseSqlServer("server=(localdb)\\mssqllocaldb;Database=Blogs");
}
