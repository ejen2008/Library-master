using Library.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Library.DAL.EF
{
    public class LibraryContext:DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        static LibraryContext()
        {
            Database.SetInitializer<LibraryContext>(new LibraryContextInitializer());
        }
    }
}