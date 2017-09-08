using AutoMapper;
using Library.BLL.Infrastructure;
using Library.DAL.Repositories;
using Library.Domain.Entities;
using Library.ViewModels.AuthorViewModels;
using Library.ViewModels.BookViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Library.BLL.Services
{
    public class AuthorService
    {
        private AuthorRepository _authorRepository;

        private string _exceprionDosenotFound = "Author doesn't found";
        private string _exceptionDoesnotCreated = "Author doesn't created";

        public AuthorService()
        {
            var context = new DAL.EF.LibraryContext();
            _authorRepository = new AuthorRepository(context);
        }

        public AuthorGetViewModel GetAuthor(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Don't installed id", "");
            }
            Author author = _authorRepository.Get(id.Value);

            ValidExceptionFound(author);

            Mapper.Initialize(a => a.CreateMap<Author, AuthorGetViewModel>());
            AuthorGetViewModel authorView = Mapper.Map<Author, AuthorGetViewModel>(author);

            return authorView;
        }

        public IEnumerable<AuthorGetViewModel> GetAuthors()
        {
            Mapper.Initialize(a => a.CreateMap<Author, AuthorGetViewModel>().ForMember(b =>b.Books, opt => opt.Ignore()));

            List<Author> authors = _authorRepository.GetAll().ToList();
            List<AuthorGetViewModel> authorsView = Mapper.Map<List<Author>, List<AuthorGetViewModel>>(authors);

            Mapper.Initialize(c => c.CreateMap<Author, AuthorGetViewModel>().ForMember(b => b.Books, opt => opt.Ignore()));
            for (int i = 0; i < authors.Count; i++)
            {
                
                authorsView[i].Books = Mapper.Map<List<Book>, List<BookGetViewModel>>(authors[i].Books.ToList());
            }

            return authorsView;
        }

        public void CreateAuthor(AuthorCreateViewModel authorView)
        {
            if (authorView == null)
            {
                throw new ValidationException(_exceptionDoesnotCreated, "");
            }

            Mapper.Initialize(a => a.CreateMap<AuthorCreateViewModel, Author>());
            Author author = Mapper.Map<AuthorCreateViewModel, Author>(authorView);
            _authorRepository.Create(author);
        }
        public void Update(AuthorGetViewModel authorView)
        {
            ValidExceptionCreated(authorView);

            Author author = _authorRepository.Get(authorView.Id);
            ValidExceptionFound(author);

            Mapper.Initialize(a => a.CreateMap<AuthorGetViewModel, Author>());
            Author authorUpdate = Mapper.Map<AuthorGetViewModel, Author>(authorView);
            _authorRepository.Update(authorUpdate);
        }

        private void ValidExceptionCreated(AuthorGetViewModel authorView)
        {
            if (authorView == null)
            {
                throw new ValidationException(_exceptionDoesnotCreated, "");
            }
        }
        private void ValidExceptionFound(Author author)
        {
            if (author == null)
            {
                throw new ValidationException(_exceprionDosenotFound, "");
            }
        }

        public void Delete(int id)
        {
            Author author = _authorRepository.Get(id);
            ValidExceptionFound(author);
            _authorRepository.Delete(id);
        }
    }
}