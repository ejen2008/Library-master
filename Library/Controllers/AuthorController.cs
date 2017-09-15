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
        [HttpGet]
        public ActionResult Create()
        {
            var authors = new List<AuthorFullNameViewModel>();
            ViewData["Authors"] = _authorService.GetAuthors();
            return View();
        }

        [HttpPost]
        public ActionResult Create(AuthorUpdateViewModel authorView, IEnumerable<int> booksMultiSelect)
        {
            if (authorView==null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                _authorService.CreateAuthor(authorView/*, booksMultiSelect*/);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            //AuthorUpdateViewModel author = _authorService.GetBookEdit(id.Value);
            AuthorUpdateViewModel author = _authorService.GetAuthor(id.Value);
            if (author == null)
            {
                return HttpNotFound();
            }
            ViewData["Books"] = _authorService.GetBooks();

            return View(author);
        }

        [HttpPost]
        public ActionResult Edit(AuthorUpdateViewModel authorView)
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


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            if (id != 0)
            {
                _authorService.Delete(id.Value);
            }
            return RedirectToAction("Index");
        }

    }
}