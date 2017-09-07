using Library.DAL.EF;
using Library.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Library.DAL.Repositories
{
    public class BookRepository
    {
        private LibraryContext _dbLibrary;

        public BookRepository(LibraryContext context)
        {
            _dbLibrary = context;
        }

        public IEnumerable<Book> GetAll()
        {
            return _dbLibrary.Books;
        }
        public Book Get(int id)
        {
            return _dbLibrary.Books.Find(id);
        }
        public void Create(Book book)
        {
            book.DateAdd = DateTime.Now;
            _dbLibrary.Books.Add(book);
        }
        public void Update(Book book)
        {
            _dbLibrary.Entry(book).State = EntityState.Modified;
        }
        public IEnumerable<Book> Find(Func<Book, Boolean> predicate)
        {
            return _dbLibrary.Books.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            Book book = _dbLibrary.Books.Find(id);
            if (book != null)
            {
                _dbLibrary.Books.Remove(book);
            }
        }
    }
}