﻿using AutoMapper;
using Library.BLL.Infrastructure;
using Library.DAL.Entities;
using Library.DAL.Repositories;
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
        private BookRepository _bookRepository;
        private BookService _bookService;
        private string _exceprionDosenotFound = "Author doesn't found";
        private string _exceptionDoesnotCreated = "Author doesn't created";

        public AuthorService()
        {
            var context = new DAL.EF.LibraryContext();
            _authorRepository = new AuthorRepository(context);
            _bookRepository = new BookRepository(context);
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
            authorView.Books = new List<BookGetViewModel>();

            _bookService = new BookService(_bookRepository);
            authorView.Books = GetBook(author.BookId);

            return authorView;
        }
        private List<BookGetViewModel> GetBook(IEnumerable<int> bookId)
        {
            var booksView = new List<BookGetViewModel>();
            foreach (int idBook in bookId)
            {
                 booksView.Add(_bookService.GetBook(idBook));
            }
            return booksView;
        }

        public IEnumerable<AuthorGetViewModel> GetAuthors()
        {
            Mapper.Initialize(a => a.CreateMap<Author, AuthorGetViewModel>());
            IEnumerable<Author> authors = _authorRepository.GetAll();
            List<AuthorGetViewModel> authorsView = Mapper.Map<IEnumerable<Author>, List<AuthorGetViewModel>>(authors);

            _bookService = new BookService(_bookRepository);

            for (int i = 0; i < authorsView.Count; i++)
            {
                IEnumerable<int> numberBooks = authors.ToList()[i].BookId;
                authorsView[i].Books = GetBook(numberBooks);
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

        public void Delete(AuthorGetViewModel authorView)
        {
            ValidExceptionCreated(authorView);

            Author author = _authorRepository.Get(authorView.Id);

            ValidExceptionFound(author);

            _authorRepository.Delete(authorView.Id);
        }
    }
}