using Library.DAL.EF;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.DAL.Repositories
{
    public class AuthorInArticleRepository
    {
        private LibraryContext _dbLibrary;

        public AuthorInArticleRepository(LibraryContext context)
        {
            _dbLibrary = context;
        }

        public List<AuthorInArticle> GetArticle(int idArticle)
        {
            return _dbLibrary.AuthorInArticle.Where(x => x.Article.Id == idArticle).ToList();
        }

        public List<AuthorInArticle> GetAll()
        {
            return _dbLibrary.AuthorInArticle.ToList();
        }

        public void Create(IEnumerable<AuthorInArticle> authorInArticle)
        {
            _dbLibrary.AuthorInArticle.AddRange(authorInArticle);
            _dbLibrary.SaveChanges();
        }

        public void Update(IEnumerable<AuthorInArticle> authorInArticle)
        {
            _dbLibrary.Entry(authorInArticle).State = System.Data.Entity.EntityState.Modified;
            _dbLibrary.SaveChanges();
        }

        public void Delete(IEnumerable<AuthorInArticle> authorInArticle)
        {
            _dbLibrary.AuthorInArticle.RemoveRange(authorInArticle);
            _dbLibrary.SaveChanges();
        }
    }
}