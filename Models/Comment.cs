using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAuthNBlog.Models
{
    public class Comment
    {
        public int id { get; set; }
        [MaxLength(200)]
        [Column(TypeName = "varchar(200)")]
        public string CommentAuthorID { get; set; }

        [Required]
        [Column(TypeName = "varchar(500)")]
        public string CommentPost { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        public virtual Blog BlogComment { get; set; }
    }
}
