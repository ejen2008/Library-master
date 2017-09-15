using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.DAL.EF;
using Library.DAL.Repositories;
using AutoMapper;
using Library.ViewModels.BookViewModels;
using Library.ViewModels.AuthorViewModels;
using Library.Domain.Entities;
using Library.BLL.AutoMapperConfig;

namespace Library.BLL.Services
{
    public class BookService
    {
        private AuthorInBookRepository _authorInBookRepository;
        private AuthorRepository _authorRepository;
        private BookRepository _bookRepository;
        public BookService()
        {
            var context = new DAL.EF.LibraryContext();
            _authorInBookRepository = new AuthorInBookRepository(context);
            _authorRepository = new AuthorRepository(context);
            _bookRepository = new BookRepository(context);
        }

        public BookGetViewModel GetBook(int id)
        {
            List<AuthorInBook> books = _authorInBookRepository.GetBook(id).ToList();
            var bookView = new BookGetViewModel();
            if (books != null)
            {
                bookView = books.GroupBy(x => x.Book.Id).Select(x => new BookGetViewModel()
                {
                    Book = x.First().Book,
                    Authors = x.Select(z => z.Author).ToList()
                }).First();
            }
            return bookView;
        }

        public IEnumerable<BookGetViewModel> GetBooks()
        {
            List<AuthorInBook> books = _authorInBookRepository.GetAll();
            var getBooksViewModel = books.GroupBy(x => x.Id).Select(x => new BookGetViewModel()
            {
                Book = x.FirstOrDefault()?.Book,
                Authors = x.Select(z => z.Author).ToList()
            });
            return getBooksViewModel;
        }

        public void CreateBook(BookUpdateViewModel bookView, IEnumerable<int> authorId)
        {
            IEnumerable<AuthorInBook> authorInBook = GetAuthorInBook(bookView, authorId);
            _authorInBookRepository.Create(authorInBook);
        }

        public void Update(BookGetViewModel bookView)
        {
            List<AuthorInBook> bookInAuthor = _authorInBookRepository.GetBook(bookView.Book.Id).ToList();

            if (bookInAuthor != null)
            {
                List<AuthorInBook> updateBook = CreateAuthorInBook(bookView, bookInAuthor.First().Id);

                _authorInBookRepository.Update(updateBook);
            }
        }

        private List<AuthorInBook> CreateAuthorInBook(BookGetViewModel bookView, int IdAuthorInBook)
        {
            var updateBook = new List<AuthorInBook>();

            if (bookView.Authors.Count != 0)
            {
                foreach (Author author in bookView.Authors)
                {
                    updateBook.Add(new AuthorInBook { Author = author, Book = bookView.Book, Id = IdAuthorInBook });
                }
            }
            if (bookView.Authors.Count == 0)
            {
                updateBook.Add(new AuthorInBook { Book = bookView.Book });
            }
            return updateBook;
        }

        public void Delete(int id)
        {
            List<AuthorInBook> aBook = _authorInBookRepository.GetBook(id).ToList();
            if (aBook != null)
            {
                _authorInBookRepository.Delete(aBook);
            }
        }

        public List<AuthorFullNameViewModel> GetAuthors()
        {
            var mapperAuthor = new AutoMapperForAuthor();
            List<AuthorFullNameViewModel> authorView = mapperAuthor.Mapp(_authorRepository.GetAll()).ToList();
            return authorView;
        }

        public BookUpdateViewModel GetBookEdit(int id)
        {
            List<AuthorInBook> books = _authorInBookRepository.GetBook(id).ToList();
            var bookView = new BookUpdateViewModel();
            if (books != null)
            {
                bookView = books.GroupBy(x => x.Book.Id).Select(x => new BookUpdateViewModel()
                {
                    Id = x.First().Book.Id,
                    NameBook = x.First().Book.NameBook,
                    NumberPages = x.First().Book.NumberPages,
                    DatePublishing = x.First().Book.DatePublishing,
                    PublishingCompany = x.First().Book.PublishingCompany
                }).First();

                bookView.Authors = new List<AuthorFullNameViewModel>();
                foreach (AuthorInBook authorInBook in books)
                {
                    bookView.Authors.Add(new AuthorFullNameViewModel(authorInBook.Author.Id, authorInBook.Author.FirstName, authorInBook.Author.LastName));
                }
            }
            return bookView;
        }

        public void Update(BookUpdateViewModel bookView, List<int> authorId)
        {
            //List<AuthorInBook> authorInBook = GetAuthorInBook(bookView, authorId);
            Book bookReposytoryUpdate = _bookRepository.Get(bookView.Id);
            if (bookReposytoryUpdate != null)
            {
                //var mapper = new AutoMapperForBook();
                //Book bookMapp = mapper.Mapp(bookView);
                bookReposytoryUpdate.NameBook = bookView.NameBook;
                bookReposytoryUpdate.NumberPages = bookView.NumberPages;
                bookReposytoryUpdate.PublishingCompany = bookView.PublishingCompany;
                bookReposytoryUpdate.DatePublishing = bookView.DatePublishing;
                _bookRepository.Update(bookReposytoryUpdate);
                UpdateRepository(bookView,authorId);
            }
            //authorInBook.GroupBy(x => x.Id).Select(z => z.First().Id = book.First().Id);
            //if (book != null)
            //{
            //    for (int i = 0; i < book.Count(); i++)
            //    {
            //        authorInBook[i].Id = bookReposytory[i].Id;
            //    }

            //    _authorInBookRepository.Update(authorInBook);
            //}
        }
        private void UpdateRepository(BookUpdateViewModel bookView, List<int> authorId)
        {
            List<AuthorInBook> authorInBookReposytory = _authorInBookRepository.GetBook(bookView.Id).ToList();
            if (authorInBookReposytory.Count == authorId.Count)
            {
                // количество авторов равно, изменение таблицы не требуется
            }
            if (authorInBookReposytory.Count > authorId.Count)//требуется удаление
            {
                var removedList = new List<AuthorInBook>(authorInBookReposytory);
                foreach (int id in authorId)
                {
                    AuthorInBook authorInBookRemove = removedList.Find(x => x.Author.Id == id);
                    removedList.Remove(authorInBookRemove);
                }
                _authorInBookRepository.Delete(removedList);
            }
            if (authorInBookReposytory.Count < authorId.Count)
            {
                Book bookAdd = _bookRepository.Get(bookView.Id);
                var addList = new List<AuthorInBook>();
                foreach (AuthorInBook idAuthorInBook in authorInBookReposytory)
                {
                    int id = authorId.Find(x => x == idAuthorInBook.Author.Id);
                    authorId.Remove(id);
                }
                foreach (int id in authorId)
                {
                    var author = _authorRepository.Get(id);
                    addList.Add(new AuthorInBook() { Book = bookAdd, Author = author });
                }
                _authorInBookRepository.Create(addList);
            }

        }



        private List<AuthorInBook> GetAuthorInBook(BookUpdateViewModel bookView, IEnumerable<int> authorId)
        {
            var authorInBook = new List<AuthorInBook>();
            var mapper = new AutoMapperForBook();
            Book book = mapper.Mapp(bookView);
            if (authorId != null)
            {
                foreach (int id in authorId)
                {
                    Author author = _authorRepository.Get(id);
                    authorInBook.Add(new AuthorInBook
                    {
                        Author = author,
                        Book = book
                    });
                }
            }

            if (authorId == null)
            {
                authorInBook.Add(new AuthorInBook { Book = book });
            }
            return authorInBook;
        }
    }
}