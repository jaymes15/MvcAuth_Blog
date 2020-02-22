using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAuthNBlog.Models
{
    public class CategoryBlogViewModel
    {
        public Category Category { get; set; }
        public List<Blog> Blogs { get; set; }

        public int CategoryID { get; set; }

        public string AuthorID { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticlePost { get; set; }
        public DateTime PublishDate { get; set; }

    }
}
