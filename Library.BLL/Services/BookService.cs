using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.BLL.Infrastructure;
using Library.DAL.EF;
using Library.DAL.Repositories;
using AutoMapper;
using Library.ViewModels.BookViewModels;
using Library.ViewModels.AuthorViewModels;
using Library.Domain.Entities;

namespace Library.BLL.Services
{
    public class BookService
    {
        private BookRepository _bookRepository;

        private string _exceptionDoesnotCreated = "Book doesn't created";
        private string _exceprionDosenotFound = "Author doesn't found";

        public BookService()
        {
            var context = new DAL.EF.LibraryContext();
            _bookRepository = new BookRepository(context);
        }

        public BookGetViewModel GetBook(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Don't installed id", "");
            }
            Book book = _bookRepository.Get(id.Value);
            if (book == null)
            {
                throw new ValidationException("Book doesn't found", "");
            }
            Mapper.Initialize(c => c.CreateMap<Book, BookGetViewModel>());
            BookGetViewModel bookView = Mapper.Map<Book, BookGetViewModel>(book);

            return bookView;
        }

        public IEnumerable<BookGetViewModel> GetBooks()
        {
            Mapper.Initialize(c => c.CreateMap<Book, BookGetViewModel>().ForMember(b => b.Authors, opt => opt.Ignore()));

            List<Book> books = _bookRepository.GetAll().ToList();
            List<BookGetViewModel> booksView = Mapper.Map<List<Book>, List<BookGetViewModel>>(books);

            Mapper.Initialize(c => c.CreateMap<Author, AuthorGetViewModel>().ForMember(a => a.Books, opt => opt.Ignore()));
            for (int i = 0; i < books.Count; i++)
            {
                booksView[i].Authors = Mapper.Map<IEnumerable<Author>, IEnumerable<AuthorGetViewModel>>(books[i].Authors);
            }
            return booksView;
        }

        public void CreateBook(BookCreateViewModel bookView)
        {
            ValidExceptionCreated(bookView);
            Mapper.Initialize(a => a.CreateMap<BookCreateViewModel, Book>());
            Book book = Mapper.Map<BookCreateViewModel, Book>(bookView);
            _bookRepository.Create(book);
        }
        public void Update(BookGetViewModel bookView)
        {
            if (bookView == null)
            {
                throw new ValidationException(_exceptionDoesnotCreated, "");
            }

            Book book = _bookRepository.Get(bookView.Id);

            ValidExceptionFound(book);


            Mapper.Initialize(a => a.CreateMap<BookGetViewModel, Book>());
            Book bookUpdate = Mapper.Map<BookGetViewModel, Book>(bookView);
            _bookRepository.Update(bookUpdate);
        }

        public void Delete(int id)
        {
            Book book = _bookRepository.Get(id);
            ValidExceptionFound(book);
            _bookRepository.Delete(id);
        }

        private void ValidExceptionCreated(BookCreateViewModel bookView)
        {
            if (bookView == null)
            {
                throw new ValidationException(_exceptionDoesnotCreated, "");
            }
        }

        private void ValidExceptionFound(Book book)
        {
            if (book == null)
            {
                throw new ValidationException(_exceprionDosenotFound, "");
            }
        }
    }
}