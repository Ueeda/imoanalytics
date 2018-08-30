using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Models
{
    public class TipoImovel
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O tipo do imóvel precisa ser inserido")]
        [MaxLength(50)]
        [Display(Name = "Tipo do imóvel")]
        public String Tipo { get; set; }
    }
}