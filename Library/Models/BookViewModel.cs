using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string NameBook { get; set; }
        public int? NumberPages { get; set; }
        public int? DatePublishing { get; set; }
        public string PublishingCompany { get; set; }
        public virtual ICollection<AuthorViewModel> Authors { get; set; }
    }
}