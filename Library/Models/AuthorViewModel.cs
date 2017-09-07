using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }
        public DateTime? DaiedDate { get; set; }
        public virtual ICollection<BookViewModel> Books { get; set; }
    }
}