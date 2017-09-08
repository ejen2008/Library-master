using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library.ViewModels.AuthorViewModels;

namespace Library.ViewModels.BookViewModels
{
    public class BookGetViewModel
    {
        public int Id { get; set; }
        public string NameBook { get; set; }
        public int? NumberPages { get; set; }
        public int? DatePublishing { get; set; }
        public string PublishingCompany { get; set; }
        public DateTime? DateAdd { get; set; }

        public virtual IEnumerable<AuthorGetViewModel> Authors { get; set; }
    }
}
