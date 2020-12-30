using System.Collections.Generic;

namespace BlogApp.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // List<T>로 One to Many 관계 표현
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
