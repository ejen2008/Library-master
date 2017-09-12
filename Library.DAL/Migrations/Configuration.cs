namespace Library.DAL.Migrations
{
    using Library.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Library.DAL.EF.LibraryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Library.DAL.EF.LibraryContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var evilHour = new Book
            {
                NameBook = "In Evil Hour",
                NumberPages = 192,
                DatePublishing = 1991,
                PublishingCompany = "Harper&Row"
            };
            var authumn = new Book
            {
                NameBook = "The Autumn of the Patriarch",
                NumberPages = 255,
                DatePublishing = 2006,
                PublishingCompany = "Harper Perennial Modern Classics"
            };
            var gabrielGarcia = new Author
            {
                FirstName = "Marquez",
                LastName = "Gabriel Garcia",
                BirthDate = new DateTime(1927, 3, 6),
                DaiedDate = new DateTime(2014, 4, 17),
            };
            context.AuthorInBooks.AddOrUpdate(
                new AuthorInBook
                {
                    Author = gabrielGarcia,
                    Book = evilHour
                });
            context.AuthorInBooks.AddOrUpdate(
                new AuthorInBook
                {
                    Author = gabrielGarcia,
                    Book = authumn
                });
            var bluestEye = new Book
            {
                NameBook = "The Bluest Eye",
                NumberPages = 172,
                DatePublishing = 1999,
                PublishingCompany = "New Ed edition"
            };
            var beloved = new Book
            {
                NameBook = "Beloved",
                NumberPages = 352,
                DatePublishing = 2007,
                PublishingCompany = "Vintage Classics"
            };
            var toniMorison = new Author
            {
                FirstName = "Toni",
                LastName = "Morrison",
                BirthDate = new DateTime(1931, 2, 18)
            };
            context.AuthorInBooks.AddOrUpdate(
                new AuthorInBook
                {
                    Author = toniMorison,
                    Book = bluestEye
                });
            context.AuthorInBooks.AddOrUpdate(
                new AuthorInBook
                {
                    Author = toniMorison,
                    Book = beloved
                });

            var platform_Net3 = new Book
            {
                NameBook = "Pro C# with .NET 3.0, Special Edition",
                NumberPages = 1456,
                DatePublishing = 2007,
                PublishingCompany = "Apress"
            };
            var platform_Net4_6 = new Book
            {
                NameBook = "C# 6.0 and the .NET 4.6 Framework",
                NumberPages = 1625,
                DatePublishing = 2015,
                PublishingCompany = "Apress"
            };
            var andrewTroelsen = new Author
            {
                FirstName = "Andrew",
                LastName = "Troelsen",
                BirthDate = new DateTime(1969, 4, 25),
            };
            context.AuthorInBooks.AddOrUpdate(new AuthorInBook
            {
                Author = andrewTroelsen,
                Book = platform_Net3
            });
            context.AuthorInBooks.AddOrUpdate(new AuthorInBook
            {
                Author = andrewTroelsen,
                Book = platform_Net4_6
            });

            var definitiveReference_5 = new Book
            {
                NameBook = "C# 5.0 in a Nutshell: The Definitive Reference",
                NumberPages = 1064,
                DatePublishing = 2012,
                PublishingCompany = "O'Reilly Media"
            };
            var definitiveReference_6 = new Book
            {
                NameBook = "C# 6.0 in a Nutshell: The Definitive Reference",
                NumberPages = 1136,
                DatePublishing = 2015,
                PublishingCompany = "O'Reilly Media"
            };
            var josephAlbahari = new Author
            {
                FirstName = "Joseph",
                LastName = "Albahari",
            };
            var benAlbahari = new Author
            {
                FirstName = "Ben",
                LastName = "Albahari",
            };
            context.AuthorInBooks.AddOrUpdate(
                new AuthorInBook {
                    Author = josephAlbahari,
                    Book = definitiveReference_5
                });
            context.AuthorInBooks.AddOrUpdate(
                new AuthorInBook {
                    Author = josephAlbahari,
                    Book = definitiveReference_6
                });
            context.AuthorInBooks.AddOrUpdate(
                new AuthorInBook {
                    Author = benAlbahari,
                    Book = definitiveReference_5
                });
            context.AuthorInBooks.AddOrUpdate(
                new AuthorInBook
                {
                    Author = benAlbahari,
                    Book = definitiveReference_6
                });

            context.SaveChanges();
        }
    }
}