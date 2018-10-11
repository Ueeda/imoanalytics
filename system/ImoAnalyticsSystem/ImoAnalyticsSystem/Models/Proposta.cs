using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Models
{
    public class Proposta
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Interessado")]
        [Required(ErrorMessage = "O interessado para fazer a proposta é obrigatório.")]
        public int InteressadoId { get; set; }

        [Display(Name = "Imóvel")]
        [Required(ErrorMessage = "O imóvel para fazer a proposta é obrigatório.")]
        public int ImovelId { get; set; }

        [Display(Name = "Data da proposta")]
        [Required(ErrorMessage = "A data da proposta é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "O valor da proposta é obrigatória.")]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }


        public virtual Interessado Interessado { get; set; }

        public virtual Imovel Imovel { get; set; }
    }
}