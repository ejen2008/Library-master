using Library.Domain.Entities;
using Library.ViewModels.BookViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels.AuthorViewModels
{
    public class AuthorGetViewModel
    {
        public Author Author { get; set; }
        public List<Book> Books { get; set; }
    }
}
