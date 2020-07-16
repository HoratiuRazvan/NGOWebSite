using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Licenta_V0.Models
{
    public class MemberModels
    {
        public int Id { get; set; }
        public ApplicationUser Member { get; set; }
        public TeamModels Team { get; set; }

        [Required(ErrorMessage = "Liderul echipei trebuie ales.")]
        [MaxLength(128)]
        [Display(Name = "Liderul echipei")]
        public string MemberId { get; set; }

        [Required(ErrorMessage = "Echipa trebuie aleasa.")]
        [Display(Name = "Echipa")]
        public int TeamId { get; set; }
    }
}