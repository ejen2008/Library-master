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
            var x = new AuthorService().GetAuthors();
            ViewData["Model"] = x;

            return View(booksView);
        }

        public ActionResult Create()
        {
            //var authors = new Dictionary<string, string>();
            //authors.Add("1","sad");
            //authors.Add("2", "dsa");
            //authors.Add("3", "sda");
            //var authors = new List<string>();
            //authors.Add("sdsad");
            //authors.Add("asd");
            //authors.Add("sdasdfad");

            var authors = new List<AuthorFullNameViewModel>();
            ViewData["Authors"] = _bookService.GetAuthors();

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

        [HttpPost]
        public ActionResult Edit(int? Id)
        {

            if (Id == null)
            {
                return HttpNotFound();
            }
            //if (ModelState.IsValid)
            //{
            //    //_bookService.Update(bookView);
            //}

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(BookGetViewModel bookView)
        {
            if (bookView == null)
            {
                return HttpNotFound();
            }
            if (bookView.Book.Id != 0)
            {
                _bookService.Delete(bookView.Book.Id);
            }
            return RedirectToAction("Index");
        }








        public ActionResult AutoComplete()
        {
            var booksView = _bookService.GetBooks();
            var nameBook = new List<string>();
            foreach (BookGetViewModel aBook in booksView)
            {
                nameBook.Add(aBook.Book.NameBook);
            }
            //ViewData["nameBook"] = nameBook;
            return View(nameBook);
        }

        public ActionResult AutoCompleteLoad(string text)
        {
            var result = _bookService.GetBooks().Where(c => c.Book.NameBook.Contains(text)).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetData(/*[DataSourceRequest] DataSourceRequest request*/)
        {
            return Json(_bookService.GetBooks()/*.ToDataSourceResult(request)*/);
        }
    }
}