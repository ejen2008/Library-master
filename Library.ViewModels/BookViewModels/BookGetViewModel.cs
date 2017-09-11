using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library.ViewModels.AuthorViewModels;
using Library.Domain.Entities;

namespace Library.ViewModels.BookViewModels
{
    public class BookGetViewModel
    {
        public Book Book { get; set; }
        public List<Author> Authors { get; set; }
    }
}
