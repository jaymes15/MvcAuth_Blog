using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAuthNBlog.Models
{
    public class Blog
    {
        public int ID { get; set; }
        public string AuthorID { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticlePost { get; set; }
        public DateTime PublishDate { get; set; }

        //public int ArticleCategory { get; set; }
        public  virtual Category Category { get; set; }
    }
}
