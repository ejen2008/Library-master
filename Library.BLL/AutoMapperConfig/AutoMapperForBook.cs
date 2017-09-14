using AutoMapper;
using Library.Domain.Entities;
using Library.ViewModels.BookViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.BLL.AutoMapperConfig
{
    public class AutoMapperForBook
    {
        public Book Mapp(BookUpdateViewModel author)
        {
            Mapper.Initialize(m => m.CreateMap<BookUpdateViewModel, Book>());
            return Mapper.Map<BookUpdateViewModel, Book>(author);
        }
    }
}