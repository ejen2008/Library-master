using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.DAL.EF;
using Library.DAL.Repositories;
using AutoMapper;
using Library.ViewModels.BookViewModels;
using Library.ViewModels.AuthorViewModels;
using Library.Domain.Entities;
using Library.BLL.AutoMapperConfig;

namespace Library.BLL.Services
{
    public class BookService
    {
        private AuthorInBookRepository _authorInBookRepository;
        private AuthorRepository _authorRepository;
        public BookService()
        {
            var context = new DAL.EF.LibraryContext();
            _authorInBookRepository = new AuthorInBookRepository(context);
            _authorRepository = new AuthorRepository(context);
        }

        public BookGetViewModel GetBook(int id)
        {
            List<AuthorInBook> books = _authorInBookRepository.GetBook(id).ToList();
            var bookView = new BookGetViewModel();
            if (books != null)
            {
                bookView = books.GroupBy(x => x.Book.Id).Select(x => new BookGetViewModel()
                {
                    Book = x.First().Book,
                    Authors = x.Select(z => z.Author).ToList()
                }).First();
            }
            return bookView;
        }

        public IEnumerable<BookGetViewModel> GetBooks()
        {
            List<AuthorInBook> books = _authorInBookRepository.GetAll();
            var getBooksViewModel = books.GroupBy(x => x.Book.Id).Select(x => new BookGetViewModel()
            {
                Book = x.FirstOrDefault()?.Book,
                Authors = x.Select(z => z.Author).ToList()
            });
            return getBooksViewModel;
        }

        public void CreateBook(BookUpdateViewModel bookView, IEnumerable<int> authorId)
        {
            var authorInBook = new List<AuthorInBook>();
            var mapper = new AutoMapperForBook();
            Book book =  mapper.Mapp(bookView);
            if (authorId != null)
            {
                foreach (int id in authorId)
                {
                    Author author = _authorRepository.Get(id);
                    authorInBook.Add(new AuthorInBook {Author = author, Book = book});
                }
            }

            if (authorId == null)
            {
                authorInBook.Add(new AuthorInBook { Book = book });
            }
            _authorInBookRepository.Create(authorInBook);
        }

        public void Update(BookGetViewModel bookView)
        {
            List<AuthorInBook> bookInAuthor = _authorInBookRepository.GetBook(bookView.Book.Id).ToList();

            if (bookInAuthor != null)
            {
                List<AuthorInBook> updateBook = CreateAuthorInBook(bookView, bookInAuthor.First().Id);

                _authorInBookRepository.Update(updateBook);
            }
        }

        private List<AuthorInBook> CreateAuthorInBook(BookGetViewModel bookView, int IdAuthorInBook)
        {
            var updateBook = new List<AuthorInBook>();

            if (bookView.Authors.Count != 0)
            {
                foreach (Author author in bookView.Authors)
                {
                    updateBook.Add(new AuthorInBook { Author = author, Book = bookView.Book, Id = IdAuthorInBook });
                }
            }
            if (bookView.Authors.Count == 0)
            {
                updateBook.Add(new AuthorInBook { Book = bookView.Book });
            }
            return updateBook;
        }

        public void Delete(int id)
        {
            List<AuthorInBook> aBook = _authorInBookRepository.GetBook(id).ToList();
            if (aBook != null)
            {
                _authorInBookRepository.Delete(aBook);
            }
        }

        public List<AuthorFullNameViewModel> GetAuthors()
        {
            var mapperAuthor = new AutoMapperForAuthor();
            List <AuthorFullNameViewModel> authorView= mapperAuthor.Mapp(_authorRepository.GetAll()).ToList();
            return authorView;
        }
    }
}