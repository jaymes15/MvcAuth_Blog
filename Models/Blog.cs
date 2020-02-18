using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAuthNBlog.Models
{
    public class Blog
    {
        public int ID { get; set; }
        public int AuthorID { get; set; }

        public string ArticleCategory { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticlePost { get; set; }
        public DateTime PublishDate { get; set; }

        public Author Author { get; set; }
    }
}
