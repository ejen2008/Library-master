using Library.DAL.Repositories;
using Library.Domain.Entities;
using Library.ViewModels.JournalViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.BLL.Services
{
    public class JournalService
    {
        private ArticleInJournalRepository _journalRepository;

        public JournalService()
        {
            var context = new DAL.EF.LibraryContext();
            _journalRepository = new ArticleInJournalRepository(context);
        }

        public JournalGetViewModel GetJournal(int id)
        {
            List<ArticleInJournal> journal = _journalRepository.GetJournal(id).ToList();
            var journalView = new JournalGetViewModel();
            if (journal != null)
            {
                journalView = journal.GroupBy(x => x.Journal.Id).Select(x => new JournalGetViewModel()
                {
                    Juornal = x.First().Journal,
                    Articles = x.Select(z => z.Article).ToList()
                }).First();
            }
            return journalView;
        }

        public IEnumerable<JournalGetViewModel> GetJournals()
        {
            List<ArticleInJournal> journals = _journalRepository.GetAll().ToList();
            var getJournalsViewModel = journals.GroupBy(x => x.Id).Select(x => new JournalGetViewModel()
            {
                Juornal = x.FirstOrDefault()?.Journal,
                Articles = x.Select(z => z.Article).ToList()
            });
            return getJournalsViewModel;
        }

        public void Create(JournalGetViewModel journalView)
        {
            var articleInJournal = new List<ArticleInJournal>();
            if (journalView.Articles.Count != 0)
            {
                foreach (Article article in journalView.Articles)
                {
                    articleInJournal.Add(new ArticleInJournal { Journal = journalView.Juornal, Article = article });
                }
            }
            if (journalView.Articles.Count == 0)
            {
                articleInJournal.Add(new ArticleInJournal { Journal = journalView.Juornal });
            }
            _journalRepository.Create(articleInJournal);
        }

        public void Update(JournalGetViewModel journalView)
        {
            List<ArticleInJournal> articleInJournal = _journalRepository.GetJournal(journalView.Juornal.Id).ToList();
            if (articleInJournal != null)
            {
                List<ArticleInJournal> updateJournal = CreateArticleInJournal(journalView, articleInJournal.First().Id);
                _journalRepository.Update(updateJournal);
            }
        }
        private List<ArticleInJournal> CreateArticleInJournal(JournalGetViewModel journalView, int IdArticleInJournal)
        {
            var articleInJournal = new List<ArticleInJournal>();
            if (journalView.Articles.Count != 0)
            {
                foreach (Article article in journalView.Articles)
                {
                    articleInJournal.Add(new ArticleInJournal { Journal = journalView.Juornal, Article = article , Id = IdArticleInJournal});
                }
            }
            if (journalView.Articles.Count == 0)
            {
                articleInJournal.Add(new ArticleInJournal { Journal = journalView.Juornal });
            }
            return articleInJournal;
        }

        public void Delete(JournalGetViewModel journalView)
        {
            List<ArticleInJournal> articleInJournal = _journalRepository.GetJournal(journalView.Juornal.Id).ToList();
            if (articleInJournal != null)
            {
                _journalRepository.Delete(articleInJournal);
            }
        }
    }
}