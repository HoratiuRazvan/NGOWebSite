using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace Licenta_V0.Models
{
    public class ArticleModels
    {
        [Required]
        public int Id { get; set; }
        [Required( ErrorMessage = "Titlu obligatoriu")]
        public String ArticleName { get; set; }
        [Required( ErrorMessage ="Autor obligatoriu")]
        public String AuthorName { get; set; }
        [Required(ErrorMessage ="Articol gol")]
        public String ArticleText { get; set; }
        public String ArticleDescription { get; set; }
        public DateTime ArticleDate { get; set; }
        public byte[] ArticleImages { get; set; }
        [Required]
        public int CategoryId { get; set; }
        //public IEnumerable<SelectListItem> Categories { get; set; }
        public CategoryModels Category { get; set; }
    }
}