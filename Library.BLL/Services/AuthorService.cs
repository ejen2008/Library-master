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
        private AuthorInBookRepository _authorInBookRepository;

        public AuthorService()
        {
            var context = new DAL.EF.LibraryContext();
            _authorInBookRepository = new AuthorInBookRepository(context);
        }

        public AuthorGetViewModel GetAuthor(int id)
        {
            List<AuthorInBook> authors = _authorInBookRepository.GetAuthor(id).ToList();
            var authorView = new AuthorGetViewModel();
            if (authors != null)
            {
                authorView = authors.GroupBy(x => x.Author.Id).Select(x => new AuthorGetViewModel()
                {
                    Author = x.First().Author,
                    Books = x.Select(a => a.Book).ToList()
                }).First();
            }
            return authorView;
        }

        public IEnumerable<AuthorGetViewModel> GetAuthors()
        {
            List<AuthorInBook> authors = _authorInBookRepository.GetAll();
            var getAuthorsViewModel = authors.GroupBy(a => a.Author.Id).Select(x => new AuthorGetViewModel()
            {
                Author = x.FirstOrDefault()?.Author,
                Books = x.Select(z => z.Book).ToList()
            });

            return getAuthorsViewModel;
        }

        public void CreateAuthor(AuthorGetViewModel authorView)
        {
            var authorInBook = new List<AuthorInBook>();
            if (authorView.Books.Count != 0)
            {
                foreach (Book b in authorView.Books)
                {
                    authorInBook.Add(new AuthorInBook { Author = authorView.Author, Book = b });
                }
            }

            if (authorView.Books.Count == 0)
            {
                authorInBook.Add(new AuthorInBook { Author = authorView.Author });
            }
            _authorInBookRepository.Create(authorInBook);
        }

        public void Update(AuthorGetViewModel authorView)
        {

            List<AuthorInBook> bookInAurhor = _authorInBookRepository.GetAuthor(authorView.Author.Id).ToList();

            if (bookInAurhor != null)
            {
                List<AuthorInBook> updateAuthor = CreateAuthorInBook(authorView);
                _authorInBookRepository.Update(updateAuthor);
            }
        }
        private List<AuthorInBook> CreateAuthorInBook(AuthorGetViewModel authorView)
        {
            var updateAuthor = new List<AuthorInBook>();
            if (authorView.Books.Count != 0)
            {
                foreach (Book book in authorView.Books)
                {
                    updateAuthor.Add(new AuthorInBook { Author = authorView.Author, Book = book });
                }
            }
            if (authorView.Books.Count == 0)
            {
                updateAuthor.Add(new AuthorInBook { Author = authorView.Author });
            }
            return updateAuthor;
        }

        public void Delete(int id)
        {
            List<AuthorInBook> author = _authorInBookRepository.GetAuthor(id).ToList();
            if (author != null)
            {
                _authorInBookRepository.Delete(author);
            }
        }
    }
}