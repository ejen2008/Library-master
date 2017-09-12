using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels.JournalViewModel
{
    public class JournalGetViewModel
    {
        public Journal Juornal { get; set; }
        public List<Article> Articles { get; set; }
    }
}
