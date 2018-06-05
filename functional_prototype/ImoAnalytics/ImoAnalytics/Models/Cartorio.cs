using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImoAnalytics.Models
{
    public class Cartorio
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O nome do cartóro precisa ser inserido.")]
        [MaxLength(100, ErrorMessage = "O tamanho máximo do nome do cartóro é de 100 caracteres.")]
        public String NomeCartorio { get; set; }
    }
}