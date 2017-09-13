using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Library.BLL.Services;
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

        //public ActionResult Index()
        //{
        //    IEnumerable<BookGetViewModel> booksView = _bookService.GetBooks();
        //    return View(booksView);
        //}

        //[HttpPost]
        //public ActionResult Create(BookGetViewModel bookView)
        //{
        //    if (bookView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        _bookService.CreateBook(bookView);
        //    }
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public ActionResult Update(BookGetViewModel bookView)
        //{
        //    if (bookView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        _bookService.Update(bookView);
        //    }
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public ActionResult Delete(BookGetViewModel bookView)
        //{
        //    if (bookView == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    if (bookView.Book.Id != 0)
        //    {
        //        _bookService.Delete(bookView.Book.Id);
        //    }
        //    return RedirectToAction("Index");
        //}


        public ActionResult Index()
        {
            IEnumerable<BookGetViewModel> booksView = _bookService.GetBooks();

            return View(MapperBook(booksView));
        }
        private IEnumerable<BookView> MapperBook(IEnumerable<BookGetViewModel> booksView)
        {
            List<BookView> books = new List<BookView>();
            foreach (BookGetViewModel b in booksView)
            {
                books.Add(new BookView
                {
                    Id = b.Book.Id,
                    NameBook = b.Book.NameBook,
                    NumberPages = b.Book.NumberPages,
                    DatePublishing = b.Book.DatePublishing,
                    PublishingCompany = b.Book.PublishingCompany
                });
            }
            return books;
        }

        [HttpPost]
        public ActionResult Create(BookView bookView)
        {
            if (bookView == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                //_bookService.CreateBook(bookView);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(BookView bookView)
        {
            if (bookView == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                //_bookService.Update(bookView);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(BookView bookView)
        {
            if (bookView == null)
            {
                return HttpNotFound();
            }
            if (bookView.Id != 0)
            {
                _bookService.Delete(bookView.Id);
            }
            return RedirectToAction("Index");
        }







        ////////////////////////////////////////////////////////////////
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

        /////////////////////////////////////////////////////////////////

        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult Load([DataSourceRequest] DataSourceRequest request)
        //{
        //    return Json(_bookService.GetBooks().ToDataSourceResult(request));
        //}

        //[HttpPost]
        //public ActionResult Create([DataSourceRequest] DataSourceRequest request, BookGetViewModel bookView)
        //{
        //    if (bookView != null && ModelState.IsValid)
        //    {
        //        _bookService.CreateBook(bookView);
        //    }

        //    return Json(new[] { bookView }.ToDataSourceResult(request, ModelState));
        //}

        //[HttpPost]
        //public ActionResult Update([DataSourceRequest] DataSourceRequest request, BookGetViewModel bookView)
        //{
        //    if (bookView != null && ModelState.IsValid)
        //    {
        //        _bookService.Update(bookView);
        //    }

        //    return Json(new[] { bookView }.ToDataSourceResult(request, ModelState));
        //}

        //[HttpPost]
        //public ActionResult Delete([DataSourceRequest] DataSourceRequest request, BookGetViewModel bookView)
        //{
        //    if (bookView != null)
        //    {
        //        _bookService.Delete(bookView.Book.Id);
        //    }

        //    return Json(new[] { bookView }.ToDataSourceResult(request, ModelState));
        //}
    }
    public class BookView
    {
        [System.ComponentModel.DataAnnotations.ScaffoldColumn(false)]
        public int Id { get; set; }
        public string NameBook { get; set; }
        public int NumberPages { get; set; }
        public int DatePublishing { get; set; }
        public string PublishingCompany { get; set; }

    }
}