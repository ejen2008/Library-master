using AutoMapper;
using Kendo.Mvc.Extensions;
using Library.BLL.DTO;
using Library.BLL.Services;
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
    public class AuthorController : Controller
    {
        private AuthorService _authorService;
        public AuthorController()
        {
            
            _authorService = new AuthorService();
        }

        public ActionResult Index()
        {
            IEnumerable<AuthorViewModel> authorsView = GetAuthorsView();
            return View(authorsView);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Authors_Create(AuthorViewModel authorView)
        {
            if (ModelState.IsValid)
            {
                AuthorDTO authorDTO = GetAuthorDTO(authorView);
                _authorService.CreateAuthor(authorDTO);

                RouteValueDictionary routeValues = this.GridRouteValues();
                return RedirectToAction("Index", routeValues);
            }
            IEnumerable<AuthorViewModel> authorsView = GetAuthorsView();
            return View("Index", authorsView);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Authors_Update(AuthorViewModel authorView)
        {
            if (ModelState.IsValid)
            {
                AuthorDTO authorDTO = GetAuthorDTO(authorView);
                _authorService.Update(authorDTO);

                RouteValueDictionary routeValues = this.GridRouteValues();
                return RedirectToAction("Index", routeValues);
            }

            IEnumerable<AuthorViewModel> authorsView = GetAuthorsView();
            return View("Index", authorsView);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Authors_Destroy(AuthorViewModel authorView)
        {
            RouteValueDictionary routeValues;
            AuthorDTO authorDTO = GetAuthorDTO(authorView);
            _authorService.Update(authorDTO);
            routeValues = this.GridRouteValues();
            return RedirectToAction("Index", routeValues);
        }



        private IEnumerable<AuthorViewModel> GetAuthorsView()
        {
            IEnumerable<AuthorDTO> authorsDTO = _authorService.GetAuthors();
            List<AuthorViewModel> authors = new List<AuthorViewModel>();
            foreach (AuthorDTO author in authorsDTO)
            {
                authors.Add( GetAuthorView(author));
            }
            return authors;
        }
        private AuthorViewModel GetAuthorView(AuthorDTO authorDTO)
        {
            Mapper.Initialize(a => a.CreateMap<AuthorDTO, AuthorViewModel>());
            AuthorViewModel author = Mapper.Map<AuthorDTO, AuthorViewModel>(authorDTO);
            return author;
        }
        private AuthorDTO GetAuthorDTO(AuthorViewModel authorView)
        {
            Mapper.Initialize(a => a.CreateMap<AuthorViewModel, AuthorDTO>());
            AuthorDTO author = Mapper.Map<AuthorViewModel, AuthorDTO>(authorView);
            return author;
        }
    }
}