using Library.DAL.EF;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Library.DAL.Repositories
{
    public class ArticleInJournalRepository
    {
        private LibraryContext _dbLibrary;

        public ArticleInJournalRepository(LibraryContext context)
        {
            _dbLibrary = context;
        }

        public IEnumerable<ArticleInJournal> GetJournal(int idJournal)
        {
            IEnumerable<ArticleInJournal> journal = _dbLibrary.ArticleInJournal.Where(x => x.Journal.Id == idJournal);
            return journal;
        }

        public IEnumerable<ArticleInJournal> GetAll()
        {
            return _dbLibrary.ArticleInJournal.ToList();
        }

        public void Create(IEnumerable<ArticleInJournal> articleInJournal)
        {
            _dbLibrary.ArticleInJournal.AddRange(articleInJournal);
            _dbLibrary.SaveChanges();
        }
        public void Delete(IEnumerable<ArticleInJournal> articleInJournal)
        {
            _dbLibrary.ArticleInJournal.RemoveRange(articleInJournal);
            _dbLibrary.SaveChanges();
        }
        public void Update(IEnumerable<ArticleInJournal> articleInJournal)
        {
            _dbLibrary.Entry(articleInJournal).State = EntityState.Modified;
            _dbLibrary.SaveChanges();
        }
        public IEnumerable<ArticleInJournal> GetArticle(int IdArticle)
        {
            IEnumerable<ArticleInJournal> article = _dbLibrary.ArticleInJournal.Where(x => x.Article.Id == IdArticle);
            return article;
        }

        public void Create(object articleInJournal)
        {
            throw new NotImplementedException();
        }
    }
}