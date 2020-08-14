using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta_V0.Models
{
    public class ArticleCategoryViewModel
    {
        public ArticleModels Article { get; set; }
        public List<CategoryModels> Category { get; set; }
    }
}