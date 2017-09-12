using Library.Domain.Entities;
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
        public DbSet<AuthorInBook> AuthorInBooks { get; set; }

        public DbSet<Article> Article { get; set; }
        public DbSet<AuthorInArticle> AuthorInArticle { get; set; }
        
        public DbSet<Journal> Journal { get; set; }
        public DbSet<ArticleInJournal> ArticleInJournal { get; set; }

        public LibraryContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LibraryContext, Library.DAL.Migrations.Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}