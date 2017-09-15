using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels.ArticleViewModels
{
    public class ArticleUpdateViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DatePublishing { get; set; }
        public string Content { get; set; }

        public List<Author> Authors { get; set; }
    }
}
