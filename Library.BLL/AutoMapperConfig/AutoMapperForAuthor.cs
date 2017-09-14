using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Library.ViewModels.AuthorViewModels;

namespace Library.BLL.AutoMapperConfig
{
    public class AutoMapperForAuthor
    {
        public IEnumerable<AuthorFullNameViewModel> Mapp(IEnumerable<Author> authors)
        {
            Mapper.Initialize(m => m.CreateMap< Author, AuthorFullNameViewModel>()
            .ForMember("FullName", opt => opt.MapFrom(c=>c.FirstName + " " + c.LastName)));
            return Mapper.Map<IEnumerable<Author>, IEnumerable<AuthorFullNameViewModel>>(authors);
        }
    }
}