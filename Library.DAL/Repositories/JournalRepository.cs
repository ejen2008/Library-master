using Library.DAL.EF;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Library.DAL.Repositories
{
    public class JournalRepository
    {
        private LibraryContext _dbLibrary;

        public JournalRepository(LibraryContext context)
        {
            _dbLibrary = context;
        }

        public void Create(Journal journal)
        {
            _dbLibrary.Journal.Add(journal);
            _dbLibrary.SaveChanges();
        }

        public Journal Get(int id)
        {
            return _dbLibrary.Journal.Find(id);
        }

        public void Update(Journal journal)
        {
            _dbLibrary.Entry(journal).State = EntityState.Modified;
            _dbLibrary.SaveChanges();
        }

    }
}