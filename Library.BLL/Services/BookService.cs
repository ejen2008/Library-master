using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.DAL.Entities;
using Library.BLL.Infrastructure;
using Library.DAL.EF;
using Library.DAL.Repositories;
using AutoMapper;
using Library.ViewModels.BookViewModels;

namespace Library.BLL.Services
{
    public class BookService
    {
        private BookRepository _bookRepository;
        private string _exceptionDoesnotCreated = "Book doesn't created";
        private string _exceprionDosenotFound = "Author doesn't found";

        public BookService(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public BookGetViewModel GetBook(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Don't installed id", "");
            }
            var book = _bookRepository.Get(id.Value);
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
            Mapper.Initialize(c => c.CreateMap<Book, BookGetViewModel>());
            IEnumerable<Book> books = _bookRepository.GetAll();
            List<BookGetViewModel> booksView = Mapper.Map<IEnumerable<Book>, List<BookGetViewModel>>(books);
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

            book = null;
            Mapper.Initialize(a => a.CreateMap<BookGetViewModel, Book>());
            book = Mapper.Map<BookGetViewModel, Book>(bookView);
            _bookRepository.Update(book);
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