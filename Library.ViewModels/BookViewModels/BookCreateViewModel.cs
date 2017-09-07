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
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }
        public DateTime? DaiedDate { get; set; }
        public IEnumerable<AuthorGetViewModel> Authors { get; set; }
    }
}
