﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library.ViewModels.AuthorViewModels;

namespace Library.ViewModels.BookViewModels
{
    public class BookGetViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }
        public DateTime? DaiedDate { get; set; }
        public IEnumerable<AuthorGetViewModel> Authors { get; set; }
    }
}
