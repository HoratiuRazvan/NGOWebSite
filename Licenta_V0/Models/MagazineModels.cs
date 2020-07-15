using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace Licenta_V0.Models
{
    public class MagazineModels
    {
        [Required]
        public int Id { get; set; }
        [Required]
        
        public String MagazineName { get; set; }
        public String MagazineDescription { get; set; } 
        public DateTime PublishDate { get; set; }
        public byte[] MagazineContaint { get; set; }
    }
}