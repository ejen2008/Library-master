using Library.BLL.Services;
using Library.ViewModels.ArticleViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class ArticleController : Controller
    {
        private ArticleService _articleService;

        public ArticleController()
        {
            _articleService = new ArticleService();
        }

        public ActionResult Index()
        {
            return View(_articleService.GetArticles());
        }

        [HttpPost]
        public ActionResult Create(ArticleGetViewModel journalView)
        {
            if (journalView == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                _articleService.CreateArticle(journalView);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(ArticleGetViewModel journalView)
        {
            if (journalView == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                _articleService.UpdateArticle(journalView);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(ArticleGetViewModel journalView)
        {
            if (journalView == null)
            {
                return HttpNotFound();
            }
            if (journalView.Article.Id != 0)
            {
                _articleService.DeleteArticle(journalView);
            }
            return RedirectToAction("Index");
        }
    }
}