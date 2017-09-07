using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class LibraryContext : DbContext
    {
        static LibraryContext()
        {
            //Database.SetInitializer<LibraryContext>(new LibraryContextInitializer());
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}