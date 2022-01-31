using System.Collections.Generic;

namespace BlogApp.Models
{
    /// <summary>
    /// 블로그: 포스트에 대한 카테고리 역할 또는 다중 블로그 이름 
    /// </summary>
    public class Blog
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // List<T>로 One to Many 관계 표현
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
