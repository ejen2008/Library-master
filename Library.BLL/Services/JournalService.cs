using Library.BLL.AutoMapperConfig;
using Library.DAL.Repositories;
using Library.Domain.Entities;
using Library.ViewModels.JournalViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.BLL.Services
{
    public class JournalService
    {
        private ArticleInJournalRepository _articleInjournalRepository;
        private JournalRepository _journalRepository;
        public JournalService()
        {
            var context = new DAL.EF.LibraryContext();
            _articleInjournalRepository = new ArticleInJournalRepository(context);
            _journalRepository = new JournalRepository(context);
        }

        public JournalUpdateViewModel GetJournal(int id)
        {
            Journal journalRepository = _journalRepository.Get(id);
            var journalView = new JournalUpdateViewModel();
            if (journalRepository != null)
            {
                var mapp = new AutoMapperForJournal();
                journalView = mapp.Mapp(journalRepository);
            }
            //List<ArticleInJournal> journal = _articleInjournalRepository.GetJournal(id).ToList();
            //var journalView = new JournalGetViewModel();
            //if (journal != null)
            //{
            //    journalView = journal.GroupBy(x => x.Journal.Id).Select(x => new JournalGetViewModel()
            //    {
            //        Juornal = x.First().Journal,
            //        Articles = x.Select(z => z.Article).ToList()
            //    }).First();
            //}
            return journalView;
        }

        public IEnumerable<JournalGetViewModel> GetJournals()
        {
            List<ArticleInJournal> journals = _articleInjournalRepository.GetAll().ToList();
            var getJournalsViewModel = journals.GroupBy(x => x.Journal.Id).Select(x => new JournalGetViewModel()
            {
                Juornal = x.FirstOrDefault()?.Journal,
                Articles = x.Select(z => z.Article).ToList()
            });
            return getJournalsViewModel;
        }

        public void Create(JournalUpdateViewModel journalView)
        {
            var mapp = new AutoMapperForJournal();
            Journal journal = mapp.Mapp(journalView);
            _journalRepository.Create(journal);
            //var articleInJournal = new List<ArticleInJournal>();
            //if (journalView.Articles.Count != 0)
            //{
            //    foreach (Article article in journalView.Articles)
            //    {
            //        articleInJournal.Add(new ArticleInJournal { Journal = journalView.Juornal, Article = article });
            //    }
            //}
            //if (journalView.Articles.Count == 0)
            //{
            //    articleInJournal.Add(new ArticleInJournal { Journal = journalView.Juornal });
            //}
            //_journalRepository.Create(articleInJournal);
        }

        public void Update(JournalUpdateViewModel journalView)
        {
            Journal journal = _journalRepository.Get(journalView.Id);
            if (journal != null)
            {
                journal.NameJornal = journalView.NameJornal;
                journal.NumberPage = journalView.NumberPage;
                journal.DatePublishing = journalView.DatePublishing;
                _journalRepository.Update(journal);
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
            List<ArticleInJournal> articleInJournal = _articleInjournalRepository.GetJournal(journalView.Juornal.Id).ToList();
            if (articleInJournal != null)
            {
                _articleInjournalRepository.Delete(articleInJournal);
            }
        }
    }
}