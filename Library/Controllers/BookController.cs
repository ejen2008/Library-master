using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Library.BLL.Services;
using Library.Domain.Entities;
using Library.ViewModels.AuthorViewModels;
using Library.ViewModels.BookViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private BookService _bookService;

        public BookController()
        {
            _bookService = new BookService();
        }

        public ActionResult Index()
        {
            IEnumerable<BookGetViewModel> booksView = _bookService.GetBooks();
            //var x = new AuthorService().GetAuthors();
            //ViewData["Model"] = x;

            return View(booksView);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var authors = new List<AuthorFullNameViewModel>();
            ViewBag.Authors = _bookService.GetAuthors();

            return View();
        }

        [HttpPost]
        public ActionResult Create(BookUpdateViewModel bookView, IEnumerable<int> authorsMultiSelect)
        {
            if (bookView == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                _bookService.CreateBook(bookView, authorsMultiSelect);
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
            BookUpdateViewModel book = _bookService.GetBookEdit(id.Value);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewData["Authors"] = _bookService.GetAuthors();
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(BookUpdateViewModel bookView, List<int> authorsMultiSelect)
        {
            if (bookView == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                _bookService.Update(bookView, authorsMultiSelect);
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
                _bookService.Delete(id.Value);
            }
            return RedirectToAction("Index");
        }
    }
}