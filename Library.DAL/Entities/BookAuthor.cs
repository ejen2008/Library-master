using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.DAL.Entities
{
    public class BookAuthor
    {
        public int? BookId { get; set; }
        public int? AuthorId { get; set; }
    }
}