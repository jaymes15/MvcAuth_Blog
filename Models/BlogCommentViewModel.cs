using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAuthNBlog.Models
{
    public class BlogCommentViewModel
    {
        public Blog Blog { get; set; }
        public List<Comment> Comments { get; set; }

        public int BlogID { get; set; }
        public string CommentAuthorID { get; set; }

        public string CommentPost { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
