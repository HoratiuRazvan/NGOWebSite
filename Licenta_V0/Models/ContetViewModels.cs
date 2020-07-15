using System.Web.Mvc;

namespace Licenta_V0.Models
{
    public class ContentViewModels
    {
        public int Id { get; set; }
        public string ArticleName { get; set; }
        public string ArticleDescription { get; set; }
        public int CategoryId { get; set; }
        [AllowHtml]
        public string ArticleText { get; set; }
        public byte[] ArticleImages { get; set; }
    }
}