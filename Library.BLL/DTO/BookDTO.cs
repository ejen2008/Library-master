using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.BLL.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string NameBook { get; set; }
        public int? NumberPages { get; set; }
        public int? DatePublishing { get; set; }
        public string PublishingCompany { get; set; }
        public ICollection<AuthorDTO> Authors { get; set; }
    }
}