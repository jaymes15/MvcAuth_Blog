using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAuthNBlog.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(200)]
        [Column(TypeName = "varchar(200)")]
        public string CategoryName { get; set; }

        //public IList<Blog> Blog { get; set; }
    }
}
