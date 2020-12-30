using System.Collections.Generic;

namespace BlogApp.Models
{
    public class Tag
    {
        public int Id { get; set; }
        
        public string Text { get; set; }

        public ICollection<Post> Posts { get; } = new List<Post>(); 
    }
}
