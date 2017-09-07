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
    //public class AuthorService : IDisposable
    //{
    //    private LibraryContext db;

    //    public AuthorService(LibraryContext db)
    //    {
    //        this.db = db;
    //    }

    //    public IEnumerable<Author> Read()
    //    {
    //        return db.Authors;  
    //    }

    //    public void Create(Author author)
    //    {
    //        var entity = new Author
    //        {
    //            Books = author.Books,
    //            FirstName = author.FirstName,
    //            LastName = author.LastName,
    //            BirthDate = author.BirthDate,
    //            DaiedDate = author.DaiedDate
    //        };

    //        db.Authors.Add(entity);
    //        db.SaveChanges();

    //        author.Id = entity.Id;
    //    }

    //    public void Update(Author author)
    //    {
    //        var entity = new Author
    //        {
    //            Id = author.Id,
    //            Books = author.Books,
    //            FirstName = author.FirstName,
    //            LastName = author.LastName,
    //            BirthDate = author.BirthDate,
    //            DaiedDate = author.DaiedDate
    //        };

    //        db.Authors.Attach(entity);
    //        db.Entry(entity).State = EntityState.Modified;
    //        db.SaveChanges(); 
    //    }

    //    public void Destroy(Author author)
    //    {
    //        var entity = new Author
    //        {
    //            Id = author.Id,
    //            Books = author.Books,
    //            FirstName = author.FirstName,
    //            LastName = author.LastName,
    //            BirthDate = author.BirthDate,
    //            DaiedDate = author.DaiedDate
    //        };

    //        db.Authors.Attach(entity);
    //        db.Authors.Remove(entity);
    //        db.SaveChanges();
    //    }

    //    public void Dispose()
    //    {
    //        db.Dispose();
    //    }
    //}

    public class AuthorController : Controller
    {
        private AuthorService authorService;

        public AuthorController()
        {
            authorService = new AuthorService(new LibraryContext());
        }

        protected override void Dispose(bool disposing)
        {
            authorService.Dispose();

            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            return View(authorService.Read());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Authors_Create(Author author)
        {
            if (ModelState.IsValid)
            {
                authorService.Create(author);

                RouteValueDictionary routeValues = this.GridRouteValues();
                return RedirectToAction("Index", routeValues);
            }

            return View("Index", authorService.Read());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Authors_Update(Author author)
        {
            if (ModelState.IsValid)
            {
                authorService.Update(author);

                RouteValueDictionary routeValues = this.GridRouteValues();
                return RedirectToAction("Index", routeValues);
            }

            return View("Index", authorService.Read());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Authors_Destroy(Author author)
        {
            RouteValueDictionary routeValues;

            authorService.Destroy(author);

            routeValues = this.GridRouteValues();

            return RedirectToAction("Index", routeValues);
        }
    }
}
