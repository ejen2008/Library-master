using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.BLL.DTO;
using Library.DAL.Entities;
using Library.BLL.Infrastructure;
using Library.DAL.EF;
using Library.DAL.Repositories;
using AutoMapper;

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

        public BookDTO GetBook(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Don't installed id", "");
            }
            var book = _bookRepository.Get(id.Value);
            if (book == null)
            {
                throw new ValidationException("Book doesn't found","");
            }
            Mapper.Initialize(c => c.CreateMap<Book, BookDTO>());
            BookDTO bookDto = Mapper.Map<Book, BookDTO>(book);
            return bookDto;
        }

        public IEnumerable<BookDTO> GetBooks()
        {
            Mapper.Initialize(c => c.CreateMap<Book, BookDTO>());
            IEnumerable<Book> books = _bookRepository.GetAll();
            List<BookDTO> booksDTO = Mapper.Map<IEnumerable<Book>, List<BookDTO>>(books);
            return booksDTO;
        }

        public void CreateBook(BookDTO bookDTO)
        {
            ValidExceptionCreated(bookDTO);
            Book book = MappBook(bookDTO);
            _bookRepository.Create(book);
        }
        public void Update(BookDTO bookDTO)
        {
            ValidExceptionCreated(bookDTO);

            Book book = _bookRepository.Get(bookDTO.Id);

            ValidExceptionFound(book);

            book = null;
            book = MappBook(bookDTO);
            _bookRepository.Update(book);
        }

        public void Delete(BookDTO bookDTO)
        {
            ValidExceptionCreated(bookDTO);

            Book book = _bookRepository.Get(bookDTO.Id);

            ValidExceptionFound(book);
            _bookRepository.Delete(book.Id);
        }

        private void ValidExceptionCreated(BookDTO BookDTO)
        {
            if (BookDTO == null)
            {
                throw new ValidationException(_exceptionDoesnotCreated, "");
            }
        }
        private Book MappBook(BookDTO bookDTO)
        {
            Mapper.Initialize(a => a.CreateMap<BookDTO, Book>());
            Book book = Mapper.Map<BookDTO, Book>(bookDTO);
            return book;
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