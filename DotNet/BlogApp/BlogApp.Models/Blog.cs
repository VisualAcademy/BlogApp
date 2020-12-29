using System.Collections.Generic;

namespace BlogApp.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
