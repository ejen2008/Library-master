using Library.DAL.Repositories;
using Library.Domain.Entities;
using Library.ViewModels.ArticleViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.BLL.Services
{
    public class ArticleService
    {
        private AuthorInArticleRepository _authorInArticleRepository;
        private ArticleInJournalRepository _articleInJournalRepository;
        public ArticleService()
        {
            var context = new DAL.EF.LibraryContext();
            _authorInArticleRepository = new AuthorInArticleRepository(context);
        }

        public ArticleGetViewModel GetArticle(int id)
        {
            List<AuthorInArticle> article = _authorInArticleRepository.GetArticle(id).ToList();
            var articleView = new ArticleGetViewModel();
            if (article != null)
            {
                articleView = article.GroupBy(x => x.Article.Id).Select(x => new ArticleGetViewModel()
                {
                    Article = x.First().Article,
                    Authors = x.Select(z => z.Author).ToList()
                }).First();
            }
            var articleInJournal = _articleInJournalRepository.GetArticle(article.First().Id);
            articleView.Journal = articleInJournal.First().Journal;
            return articleView;
        }

        public IEnumerable<ArticleGetViewModel> GetArticles()
        {
            List<AuthorInArticle> articles = _authorInArticleRepository.GetAll().ToList();
            List<ArticleGetViewModel> getArticlesViewModel = articles.GroupBy(x => x.Id).Select(x => new ArticleGetViewModel()
            {
                Journal = _articleInJournalRepository.GetArticle(x.First().Id).First().Journal,
                Article = x.FirstOrDefault()?.Article,
                Authors = x.Select(a => a.Author).ToList()
            }).ToList();
            return getArticlesViewModel;
        }

        public void CreateArticle(ArticleGetViewModel articleView)
        {
            var articleInJournal = new List<ArticleInJournal>();
            articleInJournal.Add(new ArticleInJournal() { Journal = articleView.Journal, Article = articleView.Article });

            var authorsInArticle = new List<AuthorInArticle>();
            if (articleView.Authors.Count != 0)
            {
                foreach (Author author in articleView.Authors)
                {
                    authorsInArticle.Add(new AuthorInArticle() { Article = articleView.Article, Author = author});
                }
                _authorInArticleRepository.Create(authorsInArticle);
            }

            _articleInJournalRepository.Create(articleInJournal);
        }

        public void UpdateArticle(ArticleGetViewModel articleView)
        {
           List<AuthorInArticle> authorInArticle = _authorInArticleRepository.GetArticle(articleView.Article.Id);
            ArticleInJournal articleInJournal = _articleInJournalRepository.GetJournal(articleView.Journal.Id).First();
            if (authorInArticle != null)
            {
                List<AuthorInArticle> updateArticle = CreateAuthorInArticle(articleView, authorInArticle.First().Id);
                _authorInArticleRepository.Update(updateArticle);
            }
            if (articleInJournal != null)
            {
                var article = new List<ArticleInJournal>();
                article.Add(articleInJournal);
                _articleInJournalRepository.Update(article);
            }
        }
        private List<AuthorInArticle> CreateAuthorInArticle(ArticleGetViewModel articleView, int IdArticle)
        {
            var updateArticle = new List<AuthorInArticle>();
            if (articleView.Authors.Count != 0)
            {
                foreach (Author author in articleView.Authors)
                {
                    updateArticle.Add(new AuthorInArticle { Article = articleView.Article, Author = author, Id = IdArticle });
                }
            }
            if (articleView.Authors.Count == 0)
            {
                updateArticle.Add(new AuthorInArticle { Article = articleView.Article });
            }
            return updateArticle;
        }

        public void DeleteArticle(ArticleGetViewModel articleView)
        {
            List <AuthorInArticle> authorInArticle =_authorInArticleRepository.GetArticle(articleView.Article.Id);
            if (authorInArticle != null)
            {
                _authorInArticleRepository.Delete(authorInArticle);
            }
            List<ArticleInJournal> articleInJournal = _articleInJournalRepository.GetJournal(articleView.Article.Id).ToList();
            if (articleInJournal != null)
            {
                _articleInJournalRepository.Delete(articleInJournal);
            }
        }
    }
}