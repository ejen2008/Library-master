using AutoMapper;
using Kendo.Mvc.Extensions;
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
        BookService _bookService;

        public BookController()
        {
            _bookService = new BookService();
        }

        public ActionResult Index()
        {
            IEnumerable<BookGetViewModel> booksView = _bookService.GetBooks();
            return View(booksView);
        }

        [HttpPost]
        public ActionResult BooksCreate(BookCreateViewModel bookView)
        {
            if (ModelState.IsValid)
            {
                _bookService.CreateBook(bookView);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult BooksUpdate(BookGetViewModel bookView)
        {
            if (ModelState.IsValid)
            {
                _bookService.Update(bookView);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult BooksDestroy(BookGetViewModel bookView)
        {
            if (bookView.Id != 0)
            {
                _bookService.Delete(bookView.Id);
            }
            return RedirectToAction("Index");
        }
    }
}