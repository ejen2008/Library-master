using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Kendo.Mvc.Extensions;
using Library.Models;
using System.Data.Entity;

namespace Library.Controllers
{
    //public class BookService : IDisposable
    //{
    //    private LibraryContext db;

    //    public BookService(LibraryContext db)
    //    {
    //        this.db = db;
    //    }

    //    public IEnumerable<Book> Read()
    //    {
    //        return db.Books;  
    //    }

    //    public void Create(Book book)
    //    {
    //        var entity = new Book
    //        {
    //            Authors = book.Authors,
    //            NameBook = book.NameBook,
    //            NumberPages = book.NumberPages,
    //            DatePublishing = book.DatePublishing,
    //            PublishingCompany = book.PublishingCompany
    //        };

    //        db.Books.Add(entity);
    //        db.SaveChanges();

    //        book.Id = entity.Id;
    //    }

    //    public void Update(Book book)
    //    {
    //        var entity = new Book
    //        {
    //            Id = book.Id,
    //            Authors = book.Authors,
    //            NameBook = book.NameBook,
    //            NumberPages = book.NumberPages,
    //            DatePublishing = book.DatePublishing,
    //            PublishingCompany = book.PublishingCompany
    //        };

    //        db.Books.Attach(entity);
    //        db.Entry(entity).State = EntityState.Modified;
    //        db.SaveChanges(); 
    //    }

    //    public void Destroy(Book book)
    //    {
    //        var entity = new Book
    //        {
    //            Id = book.Id,
    //            Authors = book.Authors,
    //            NameBook = book.NameBook,
    //            NumberPages = book.NumberPages,
    //            DatePublishing = book.DatePublishing,
    //            PublishingCompany = book.PublishingCompany
    //        };

    //        db.Books.Attach(entity);
    //        db.Books.Remove(entity);
    //        db.SaveChanges();
    //    }

    //    public void Dispose()
    //    {
    //        db.Dispose();
    //    }
    //}

    public class BookController : Controller
    {
        private BookService bookService;

        public BookController()
        {
            bookService = new BookService(new LibraryContext());
        }

        protected override void Dispose(bool disposing)
        {
            bookService.Dispose();

            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            return View(bookService.Read());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Books_Create(Book book)
        {
            if (ModelState.IsValid)
            {
                bookService.Create(book);

                RouteValueDictionary routeValues = this.GridRouteValues();
                return RedirectToAction("Index", routeValues);
            }

            return View("Index", bookService.Read());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Books_Update(Book book)
        {
            if (ModelState.IsValid)
            {
                bookService.Update(book);

                RouteValueDictionary routeValues = this.GridRouteValues();
                return RedirectToAction("Index", routeValues);
            }

            return View("Index", bookService.Read());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Books_Destroy(Book book)
        {
            RouteValueDictionary routeValues;

            bookService.Destroy(book);

            routeValues = this.GridRouteValues();

            return RedirectToAction("Index", routeValues);
        }
    }
}
