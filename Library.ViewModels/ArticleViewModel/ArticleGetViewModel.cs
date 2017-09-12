using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels.ArticleViewModel
{
    public class ArticleGetViewModel
    {
        public Journal Journal { get; set; }
        public Article Article { get; set; }
        public List<Author> Authors { get; set; }
    }
}
