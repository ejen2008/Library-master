using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.DAL.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string NameBook { get; set; }
        public int? NumberPages { get; set; }
        public int? DatePublishing { get; set; }
        public string PublishingCompany { get; set; }
        public DateTime? DateAdd { get; set; }

        public IEnumerable<int> AuthorId { get; set; }
        //public virtual ICollection<Author> Authors { get; set; }
    }
}