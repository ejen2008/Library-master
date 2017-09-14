using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels.BookViewModels
{
    public class BookUpdateViewModel
    {
        //[ScaffoldColumn(false)]
        public int Id { get; set; }
        public string NameBook { get; set; }
        public int NumberPages { get; set; }
        public int DatePublishing { get; set; }
        public string PublishingCompany { get; set; }

        //public List<Domain.Entities.Author> Authors { get; set; }
    }
}
