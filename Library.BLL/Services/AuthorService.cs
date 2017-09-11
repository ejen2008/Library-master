using AutoMapper;
using Library.BLL.Infrastructure;
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
        private string _exceprionDosenotFound = "Author doesn't found";
        private string _exceptionDoesnotCreated = "Author doesn't created";

        public AuthorService()
        {
            var context = new DAL.EF.LibraryContext();
            _authorInBookRepository = new AuthorInBookRepository(context);
        }

        public AuthorGetViewModel GetAuthor(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("Don't installed id", "");
            }
            List<AuthorInBook> authors = _authorInBookRepository.GetAuthor(id.Value).ToList();

            ValidExceptionFound(authors);

            var authorView = authors.GroupBy(x => x.Author.Id).Select(x => new AuthorGetViewModel()
            {
                Author = x.First().Author,
                Books = x.Select(a => a.Book).ToList()
            }).First();

            return authorView;
        }

        public IEnumerable<AuthorGetViewModel> GetAuthors()
        {
            List<AuthorInBook> authors = _authorInBookRepository.GetAll();
            var getAuthorsViewModel = authors.GroupBy(a => a.Id).Select(x => new AuthorGetViewModel()
            {
                Author = x.FirstOrDefault()?.Author,
                Books = x.Select(z => z.Book).ToList()
            });

            return getAuthorsViewModel;
        }

        public void CreateAuthor(AuthorGetViewModel authorView)
        {
            ValidExceptionCreated(authorView);

            var authorInBook = new List<AuthorInBook>();
            if (authorView.Books.Count != 0)
            {
                foreach (Book b in authorView.Books)
                {
                    authorInBook.Add(new AuthorInBook { Author = authorView.Author, Book = b });
                }
            }

            if (authorView.Books.Count == 0)
            {
                authorInBook.Add(new AuthorInBook { Author = authorView.Author });
            }
            _authorInBookRepository.Create(authorInBook);
        }

        public void Update(AuthorGetViewModel authorView)
        {
            ValidExceptionCreated(authorView);

            List<AuthorInBook> bookInAurhor = _authorInBookRepository.GetAuthor(authorView.Author.Id).ToList();

            ValidExceptionFound(bookInAurhor);

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
            _authorInBookRepository.Update(updateAuthor);
        }

        private void ValidExceptionCreated(AuthorGetViewModel authorView)
        {
            if (authorView == null)
            {
                throw new ValidationException(_exceptionDoesnotCreated, "");
            }
        }
        private void ValidExceptionFound(List<AuthorInBook> author)
        {
            if (author == null)
            {
                throw new ValidationException(_exceprionDosenotFound, "");
            }
        }

        public void Delete(int id)
        {
            List<AuthorInBook> author = _authorInBookRepository.GetAuthor(id).ToList();
            ValidExceptionFound(author);
            _authorInBookRepository.Delete(author);
        }
    }
}












//public Configuration()
//{
//    AutomaticMigrationsEnabled = true;
//    AutomaticMigrationDataLossAllowed = true;
//}

//protected override void Seed(Library.DAL.EF.LibraryContext context)
//{
//    //  This method will be called after migrating to the latest version.

//    //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
//    //  to avoid creating duplicate seed data.
//    var evilHour = new Book
//    {
//        NameBook = "In Evil Hour",
//        NumberPages = 192,
//        DatePublishing = 1991,
//        PublishingCompany = "Harper&Row"
//    };
//    var authumn = new Book
//    {
//        NameBook = "The Autumn of the Patriarch",
//        NumberPages = 255,
//        DatePublishing = 2006,
//        PublishingCompany = "Harper Perennial Modern Classics"
//    };
//    var gabrielGarcia = new Author
//    {
//        FirstName = "Marquez",
//        LastName = "Gabriel Garcia",
//        BirthDate = new DateTime(1927, 3, 6),
//        DaiedDate = new DateTime(2014, 4, 17),
//        Books = new List<Book>() { evilHour, authumn }
//    };

//    var bluestEye = new Book
//    {
//        NameBook = "The Bluest Eye",
//        NumberPages = 172,
//        DatePublishing = 1999,
//        PublishingCompany = "New Ed edition"
//    };
//    var beloved = new Book
//    {
//        NameBook = "Beloved",
//        NumberPages = 352,
//        DatePublishing = 2007,
//        PublishingCompany = "Vintage Classics"
//    };
//    var toniMorison = new Author
//    {
//        FirstName = "Toni",
//        LastName = "Morrison",
//        BirthDate = new DateTime(1931, 2, 18),
//        Books = new List<Book>() { bluestEye, beloved }
//    };

//    var platform_Net3 = new Book
//    {
//        NameBook = "Pro C# with .NET 3.0, Special Edition",
//        NumberPages = 1456,
//        DatePublishing = 2007,
//        PublishingCompany = "Apress"
//    };
//    var platform_Net4_6 = new Book
//    {
//        NameBook = "C# 6.0 and the .NET 4.6 Framework",
//        NumberPages = 1625,
//        DatePublishing = 2015,
//        PublishingCompany = "Apress"
//    };
//    var andrewTroelsen = new Author
//    {
//        FirstName = "Andrew",
//        LastName = "Troelsen",
//        BirthDate = new DateTime(1969, 4, 25),
//        Books = new List<Book>() { platform_Net3, platform_Net4_6 }
//    };

//    var definitiveReference_5 = new Book
//    {
//        NameBook = "C# 5.0 in a Nutshell: The Definitive Reference",
//        NumberPages = 1064,
//        DatePublishing = 2012,
//        PublishingCompany = "O'Reilly Media"
//    };
//    var definitiveReference_6 = new Book
//    {
//        NameBook = "C# 6.0 in a Nutshell: The Definitive Reference",
//        NumberPages = 1136,
//        DatePublishing = 2015,
//        PublishingCompany = "O'Reilly Media"
//    };
//    var josephAlbahari = new Author
//    {
//        FirstName = "Joseph",
//        LastName = "Albahari",
//        Books = new List<Book>() { definitiveReference_5, definitiveReference_6 }
//    };
//    var benAlbahari = new Author
//    {
//        FirstName = "Ben",
//        LastName = "Albahari",
//        Books = new List<Book>() { definitiveReference_5, definitiveReference_6 }
//    };

//    context.Books.AddOrUpdate(evilHour, authumn, bluestEye, beloved, platform_Net3, platform_Net4_6, definitiveReference_5, definitiveReference_6);
//    context.Authors.AddOrUpdate(gabrielGarcia, toniMorison, andrewTroelsen, josephAlbahari, benAlbahari);

//    context.SaveChanges();
//}