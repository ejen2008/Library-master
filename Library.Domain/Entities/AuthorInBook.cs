﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class AuthorInBook
    {
        public int Id { get; set; }
        public virtual Book Book { get; set; }
        public virtual Author Author { get; set; }
    }
}
