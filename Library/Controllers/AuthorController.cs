using AutoMapper;
using Kendo.Mvc.Extensions;
using Library.BLL.Services;
using Library.Domain.Entities;
using Library.ViewModels.AuthorViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Library.Controllers
{
    public class AuthorController : Controller
    {
        private AuthorService _authorService;
        public AuthorController()
        {
            _authorService = new AuthorService();
        }

        public ActionResult Index()
        {
            IEnumerable<AuthorGetViewModel> authorsView = _authorService.GetAuthors();
            return View(authorsView);
        }

        [HttpPost]
        public ActionResult Create(AuthorGetViewModel authorView)
        {
            if (authorView==null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                _authorService.CreateAuthor(authorView);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(AuthorGetViewModel authorView)
        {
            if (authorView == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                _authorService.Update(authorView);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(AuthorGetViewModel authorView)
        {
            if (authorView == null)
            {
                return HttpNotFound();
            }
            if (authorView.Author.Id != 0)
            {
                _authorService.Delete(authorView.Author.Id);
            }
            return RedirectToAction("Index");
        }
    }
}