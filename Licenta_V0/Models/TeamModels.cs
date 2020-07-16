using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Licenta_V0.Models
{
    public class TeamModels
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Numele echipei este obligatoriu.")]
        [Display(Name = "Nume echipa")]
        public string Title { get; set; }

        public ApplicationUser TeamLeader { get; set; }
        [Required(ErrorMessage = "Liderul echipei trebuie ales.")]
        [MaxLength(128)]
        [Display(Name = "Liderul echipei")]
        public string TeamLeaderId { get; set; }

    }
}