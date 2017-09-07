using Library.BLL.Services;
using Library.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class BookViewController : Controller
    {
        BookService _bookService = new BookService(new BookRepository(new DAL.EF.LibraryContext())); 
        public ActionResult Index()
        {
            _bookService.
            return View(_bookService.G);
        }
    }
}