using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Models
{
    public class Locacao
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Data da locação")]
        [Required(ErrorMessage = "A data de locação é obrigatória.")]
        public DateTime DataLocacao { get; set; }

        [Display(Name = "Imóvel")]
        [Required(ErrorMessage = "O imóvel locado é obrigatório.")]
        public int ImovelId { get; set; }

        [Display(Name = "Interessado")]
        [Required(ErrorMessage = "O interessado que alugou o imóvel é obrigatório.")]
        public int InteressadoId { get; set; }


        public int ContratoDeLocacaoId { get; set; }

        [Display(Name = "Fiador")]
        [Required(ErrorMessage = "O fiador é obrigatório.")]
        public int FiadorId { get; set; }


        public virtual Imovel Imovel { get; set; }


        public virtual Interessado Interessado { get; set; }


        public virtual ContratoDeLocacao ContratoDeLocacao { get; set; }


        public virtual Fiador Fiador { get; set; }
    }
}