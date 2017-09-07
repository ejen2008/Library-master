using AutoMapper;
using Kendo.Mvc.Extensions;
using Library.BLL.DTO;
using Library.BLL.Services;
using Library.DAL.EF;
using Library.DAL.Repositories;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Library.Controllers
{
    public class BookViewController : Controller
    {
        BookService _bookService;

        public BookViewController()
        {
            var rep = new BookRepository(new DAL.EF.LibraryContext());
            _bookService = new BookService(rep);
        }

        public ActionResult Index()
        {

            IEnumerable<BookViewModel> booksView = GetBooksView(booksDTO);
            return View(booksView);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Books_Create(BookViewModel bookView)
        {
            if (ModelState.IsValid)
            {
                BookDTO bookDTO = GetBookDTO(bookView);
                _bookService.CreateBook(bookDTO);

                RouteValueDictionary routeValues = this.GridRouteValues();
                return RedirectToAction("Index", routeValues);
            }
            IEnumerable<BookViewModel> booksView = GetBooksView();
            return View("Index", booksView);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Books_Update(BookViewModel bookView)
        {
            if (ModelState.IsValid)
            {
                BookDTO bookDTO = GetBookDTO(bookView);
                _bookService.Update(bookDTO);

                RouteValueDictionary routeValues = this.GridRouteValues();
                return RedirectToAction("Index", routeValues);
            }
            IEnumerable<BookViewModel> booksView = GetBooksView();
            return View("Index", booksView);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Books_Destroy(BookViewModel bookView)
        {
            RouteValueDictionary routeValues;
            BookDTO bookDTO = GetBookDTO(bookView);
            _bookService.Update(bookDTO);
            routeValues = this.GridRouteValues();
            return RedirectToAction("Index", routeValues);
        }

        private BookViewModel GetBookView(BookDTO bookDTO)
        {
            Mapper.Initialize(b => b.CreateMap<BookDTO, BookViewModel>());
            BookViewModel book = Mapper.Map<BookDTO, BookViewModel>(bookDTO);
            return book;
        }
        private BookDTO GetBookDTO(BookViewModel bookView)
        {
            Mapper.Initialize(b => b.CreateMap<BookViewModel, BookDTO>());
            BookDTO book = Mapper.Map<BookViewModel, BookDTO>(bookView);
            return book;
        }
        private IEnumerable<BookViewModel> GetBooksView()
        {
            IEnumerable<BookDTO> booksDTO = _bookService.GetBooks();
            var booksView = new List<BookViewModel>();
            foreach (BookDTO book in booksDTO)
            {
                var bookV = GetBookView(book);
                booksView.Add(bookV);
            }
            return booksView;
        }
    }
}