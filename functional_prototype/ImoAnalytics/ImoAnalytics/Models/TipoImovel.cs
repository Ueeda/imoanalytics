using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImoAnalytics.Models
{
    public class TipoImovel
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O tipo do imóvel precisa ser inserido")]
        [MaxLength(50)]
        public String Tipo { get; set; }
    }
}