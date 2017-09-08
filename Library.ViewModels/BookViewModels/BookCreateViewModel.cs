using Library.ViewModels.AuthorViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels.BookViewModels
{
    public class BookCreateViewModel
    {
        public string NameBook { get; set; }
        public int? NumberPages { get; set; }
        public int? DatePublishing { get; set; }
        public string PublishingCompany { get; set; }
        public DateTime? DateAdd { get; set; }

        public virtual IEnumerable<AuthorGetViewModel> Authors { get; set; }
    }
}
