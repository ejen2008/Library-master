using AutoMapper;
using Library.Domain.Entities;
using Library.ViewModels.AuthorViewModels;
using Library.ViewModels.BookViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.BLL.AutoMapperConfig
{
    public class AutoMapperForBook
    {
        public Book Mapp(BookUpdateViewModel book)
        {
            Mapper.Initialize(m => m.CreateMap<BookUpdateViewModel, Book>());
            return Mapper.Map<BookUpdateViewModel, Book>(book);
        }

        public IEnumerable<BookTitleViewModel> Mapp(IEnumerable<Book> book)
        {
            Mapper.Initialize(m => m.CreateMap<Book, BookTitleViewModel>());
            return Mapper.Map<IEnumerable<Book>, IEnumerable<BookTitleViewModel>>(book);
        }

    }
}