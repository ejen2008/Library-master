using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels.AuthorViewModels
{
    public class AuthorFullNameViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public AuthorFullNameViewModel()
        { }
        public AuthorFullNameViewModel(int id, string firstName, string lastName)
        {
            Id = id;
            FullName = firstName + " " + lastName;
        }
    }
}
