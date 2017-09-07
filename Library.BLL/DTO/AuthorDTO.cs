using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.BLL.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DaiedDate { get; set; }
        public ICollection<BookDTO> BookID { get; set; }
    }
}