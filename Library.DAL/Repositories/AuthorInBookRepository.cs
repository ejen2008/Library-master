using Library.DAL.EF;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Library.DAL.Repositories
{
    public class AuthorInBookRepository
    {
        private LibraryContext _dbLibrary;

        public AuthorInBookRepository(LibraryContext context)
        {
            _dbLibrary = context;
        }

        public List<AuthorInBook> GetAll()
        {
            return _dbLibrary.AuthorInBooks.ToList();
        }

        public IEnumerable<AuthorInBook> GetBook(int IdBook)
        {
            IEnumerable<AuthorInBook> book = _dbLibrary.AuthorInBooks.Where(x => x.Book.Id == IdBook);
            return book;
        }

        public void Create(IEnumerable<AuthorInBook> authorInBook)
        {
            _dbLibrary.AuthorInBooks.AddRange(authorInBook);
            _dbLibrary.SaveChanges();
        }

        public void Update(IEnumerable<AuthorInBook> authorInBook)
        {
            _dbLibrary.Entry(authorInBook).State = EntityState.Modified;
            _dbLibrary.SaveChanges();
        }

        public void Delete(IEnumerable<AuthorInBook> authorInBook)
        {
            _dbLibrary.AuthorInBooks.RemoveRange(authorInBook);
            _dbLibrary.SaveChanges();
        }

        public IEnumerable<AuthorInBook> GetAuthor(int IdAuthor)
        {
            IEnumerable<AuthorInBook> author = _dbLibrary.AuthorInBooks.Where(x => x.Author.Id == IdAuthor);
            return author;
        }
    }
}