using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels.JournalViewModels
{
    public class JournalUpdateViewModel
    {
        public int Id { get; set; }
        public string NameJornal { get; set; }
        public DateTime DatePublishing { get; set; }
        public int NumberPage { get; set; }
    }
}
