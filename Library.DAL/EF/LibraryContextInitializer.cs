//using Library.DAL.Entities;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;

//namespace Library.DAL.EF
//{
//    public class LibraryContextInitializer : DropCreateDatabaseAlways<LibraryContext>
//    {
//        protected override void Seed(LibraryContext context)
//        {
//            var evilHour = new Book
//            {
//                NameBook = "In Evil Hour",
//                NumberPages = 192,
//                DatePublishing = 1991,
//                PublishingCompany = "Harper&Row"
//            };
//            var authumn = new Book
//            {
//                NameBook = "The Autumn of the Patriarch",
//                NumberPages = 255,
//                DatePublishing = 2006,
//                PublishingCompany = "Harper Perennial Modern Classics"
//            };
//            var gabrielGarcia = new Author
//            {
//                FirstName = "Marquez",
//                LastName = "Gabriel Garcia",
//                BirthDate = new DateTime(1927, 3, 6),
//                DaiedDate = new DateTime(2014, 4, 17),
//                Books = new List<Book>() { evilHour, authumn }
//            };

//            var bluestEye = new Book
//            {
//                NameBook = "The Bluest Eye",
//                NumberPages = 172,
//                DatePublishing = 1999,
//                PublishingCompany = "New Ed edition"
//            };
//            var beloved = new Book
//            {
//                NameBook = "Beloved",
//                NumberPages = 352,
//                DatePublishing = 2007,
//                PublishingCompany = "Vintage Classics"
//            };
//            var toniMorison = new Author
//            {
//                FirstName = "Toni",
//                LastName = "Morrison",
//                BirthDate = new DateTime(1931, 2, 18),
//                Books = new List<Book>() { bluestEye, beloved }
//            };

//            var platform_Net3 = new Book
//            {
//                NameBook = "Pro C# with .NET 3.0, Special Edition",
//                NumberPages = 1456,
//                DatePublishing = 2007,
//                PublishingCompany = "Apress"
//            };
//            var platform_Net4_6 = new Book
//            {
//                NameBook = "C# 6.0 and the .NET 4.6 Framework",
//                NumberPages = 1625,
//                DatePublishing = 2015,
//                PublishingCompany = "Apress"
//            };
//            var andrewTroelsen = new Author
//            {
//                FirstName = "Andrew",
//                LastName = "Troelsen",
//                BirthDate = new DateTime(1969, 4, 25),
//                Books = new List<Book>() { platform_Net3, platform_Net4_6 }
//            };

//            var definitiveReference_5 = new Book
//            {
//                NameBook = "C# 5.0 in a Nutshell: The Definitive Reference",
//                NumberPages = 1064,
//                DatePublishing = 2012,
//                PublishingCompany = "O'Reilly Media"
//            };
//            var definitiveReference_6 = new Book
//            {
//                NameBook = "C# 6.0 in a Nutshell: The Definitive Reference",
//                NumberPages = 1136,
//                DatePublishing = 2015,
//                PublishingCompany = "O'Reilly Media"
//            };
//            var josephAlbahari = new Author
//            {
//                FirstName = "Joseph",
//                LastName = "Albahari",
//                Books = new List<Book>() { definitiveReference_5, definitiveReference_6 }
//            };
//            var benAlbahari = new Author
//            {
//                FirstName = "Ben",
//                LastName = "Albahari",
//                Books = new List<Book>() { definitiveReference_5, definitiveReference_6 }
//            };

//            context.Books.Add(evilHour);
//            context.Books.Add(authumn);
//            context.Books.Add(bluestEye);
//            context.Books.Add(beloved);
//            context.Books.Add(platform_Net3);
//            context.Books.Add(platform_Net4_6);
//            context.Books.Add(definitiveReference_5);
//            context.Books.Add(definitiveReference_6);

//            context.Authors.Add(gabrielGarcia);
//            context.Authors.Add(toniMorison);
//            context.Authors.Add(andrewTroelsen);
//            context.Authors.Add(josephAlbahari);
//            context.Authors.Add(benAlbahari);

//            context.SaveChanges();
//        }
//    }
//}