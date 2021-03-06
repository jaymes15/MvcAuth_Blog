﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAuthNBlog.Models
{
    public class Blog
    {
        public int ID { get; set; }

      
        [MaxLength(200)]
        [Column(TypeName = "varchar(200)")]
        public string AuthorID { get; set; }

        [Required]
        [MaxLength(500)]
        [Column(TypeName = "varchar(500)")]
        public string ArticleTitle { get; set; }

        [Required]
        [Column(TypeName = "varchar(500)")]
        public string ArticlePost { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        //public int ArticleCategory { get; set; }
        public  virtual Category Category { get; set; }
    }
}
