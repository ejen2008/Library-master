using AutoMapper;
using Library.BLL.AutoMapperConfig;
using Library.DAL.Repositories;
using Library.Domain.Entities;
using Library.ViewModels.AuthorViewModels;
using Library.ViewModels.BookViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Library.BLL.Services
{
    public class AuthorService
    {
        private AuthorInBookRepository _authorInBookRepository;
        private AuthorRepository _authorRepository;
        private BookRepository _bookRepository;

        public AuthorService()
        {
            var context = new DAL.EF.LibraryContext();
            _authorInBookRepository = new AuthorInBookRepository(context);
            _authorRepository = new AuthorRepository(context);
            _bookRepository = new BookRepository(context);
        }

        public AuthorUpdateViewModel GetAuthor(int id)
        {
            Author authorRepository = _authorRepository.Get(id);
            var mapp = new AutoMapperForAuthor();
            AuthorUpdateViewModel authorView = mapp.Mapp(authorRepository);

            //List<AuthorInBook> authors = _authorInBookRepository.GetAuthor(id).ToList();
            //var authorView = new AuthorGetViewModel();
            //if (authors != null)
            //{
            //    authorView = authors.GroupBy(x => x.Author.Id).Select(x => new AuthorGetViewModel()
            //    {
            //        Author = x.First().Author,
            //        Books = x.Select(a => a.Book).ToList()
            //    }).First();
            //}
            return authorView;
        }

        public IEnumerable<AuthorGetViewModel> GetAuthors()
        {
            List<AuthorInBook> authors = _authorInBookRepository.GetAll();
            var getAuthorsViewModel = authors.GroupBy(a => a.Author.Id).Select(x => new AuthorGetViewModel()
            {
                Author = x.FirstOrDefault()?.Author,
                Books = x.Select(z => z.Book).ToList()
            });

            return getAuthorsViewModel;
        }

        public List<BookTitleViewModel> GetBooks()
        {
            var mapperAuthor = new AutoMapperForBook();
            List<BookTitleViewModel> authorView = mapperAuthor.Mapp(_bookRepository.GetAll()).ToList();
            return authorView;
        }


        public void CreateAuthor(AuthorUpdateViewModel authorView/*, IEnumerable<int> bookId*/)
        {
            IEnumerable<AuthorInBook> authorInBook = GetAuthorInBook(authorView/*, bookId*/);
            _authorInBookRepository.Create(authorInBook);





            //var authorInBook = new List<AuthorInBook>();
            //if (authorView.Books.Count != 0)
            //{
            //    foreach (Book b in authorView.Books)
            //    {
            //        authorInBook.Add(new AuthorInBook { Author = authorView.Author, Book = b });
            //    }
            //}

            //if (authorView.Books.Count == 0)
            //{
            //    authorInBook.Add(new AuthorInBook { Author = authorView.Author });
            //}
            //_authorInBookRepository.Create(authorInBook);
        }


        public void Update(AuthorUpdateViewModel authorView)
        {
            Author author = _authorRepository.Get(authorView.Id);
            if (author != null)
            {
                author.FirstName = authorView.FirstName;
                author.LastName = authorView.LastName;
                author.DaiedDate = authorView.DaiedDate;
                author.BirthDate = authorView.BirthDate;
                _authorRepository.Update(author);
            }

            //IEnumerable<AuthorInBook> authorInBook = GetAuthorInBook(authorView);
            //IEnumerable<AuthorInBook> author = _authorInBookRepository.GetAuthor(authorView.Id).ToList();
            //var updateAuthor =  authorInBook.GroupBy(x => x.Id).Select(z => z.First().Id = author.First().Id);
            //foreach (AuthorInBook a in authorInBook)
            //{
            //    a.Book.Id = author.First().Author.Id;
            //}

            //if(author != null)
            //{
            //    _authorInBookRepository.Update(authorInBook);
            //}
        }



        public AuthorUpdateViewModel GetBookEdit(int id)
        {
            List<AuthorInBook> bookInAurhor = _authorInBookRepository.GetAuthor(id).ToList();
            var authorView = new AuthorUpdateViewModel();
            if (bookInAurhor != null)
            {
                authorView = bookInAurhor.GroupBy(x => x.Author.Id).Select(x => new AuthorUpdateViewModel()
                {
                    Id = x.First().Author.Id,
                    BirthDate = x.First().Author.BirthDate,
                    DaiedDate = x.First().Author.DaiedDate,
                    FirstName = x.First().Author.FirstName,
                    LastName = x.First().Author.LastName
                }).First();
                authorView.Books = new List<BookTitleViewModel>();
                foreach (AuthorInBook authorInBook in bookInAurhor)
                {
                    authorView.Books.Add(new BookTitleViewModel() {
                        Id = authorInBook.Book.Id,
                        NameBook = authorInBook.Book.NameBook,
                        NumberPages = authorInBook.Book.NumberPages });
                }
            }
            return authorView;
        }
        private List<AuthorInBook> CreateAuthorInBook(AuthorGetViewModel authorView)
        {
            var updateAuthor = new List<AuthorInBook>();
            if (authorView.Books.Count != 0)
            {
                foreach (Book book in authorView.Books)
                {
                    updateAuthor.Add(new AuthorInBook { Author = authorView.Author, Book = book });
                }
            }
            if (authorView.Books.Count == 0)
            {
                updateAuthor.Add(new AuthorInBook { Author = authorView.Author });
            }
            return updateAuthor;
        }

        public void Delete(int id)
        {
            List<AuthorInBook> author = _authorInBookRepository.GetAuthor(id).ToList();
            if (author != null)
            {
                _authorInBookRepository.Delete(author);
            }
        }


        private List<AuthorInBook> GetAuthorInBook(AuthorUpdateViewModel bookView/*, IEnumerable<int> authorId*/)
        {
            var authorInBook = new List<AuthorInBook>();
            var mapper = new AutoMapperForAuthor();
            Author author = mapper.Mapp(bookView);
            authorInBook.Add(new AuthorInBook { Author = author });
            return authorInBook;
        }
    }
}