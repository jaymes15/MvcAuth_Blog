using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAuthNBlog.Models
{
    public class Author
    {
        public int ID { get; set; }
        public string Author_FirstName { get; set; }

        public string Author_LastName { get; set; }

        public ICollection<Blog> Blog { get; set; }


    }
}
