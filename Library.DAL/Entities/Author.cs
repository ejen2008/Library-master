using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.DAL.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DaiedDate { get; set; }
        public DateTime? DateAdd { get; set; }

        public IEnumerable<int> BookId { get; set; }
        //public virtual ICollection<Book> Books { get; set; }
    }
}