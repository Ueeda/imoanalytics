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

        [Display(Name = "Código da locação")]
        public String CodigoLocacao { get; set; }

        [Display(Name = "Data da locação")]
        [DataType(DataType.Date)]
        public DateTime DataOperacao { get; set; }

        public int ImovelId { get; set; }

        public int InteressadoId { get; set; }

        public int ContratoDeLocacaoId { get; set; }
        
        public int FiadorId { get; set; }

        [Display(Name = "Imóvel")]
        public virtual Imovel Imovel { get; set; }

        [Display(Name = "Interessado")]
        public virtual Interessado Interessado { get; set; }

        public virtual ContratoDeLocacao ContratoDeLocacao { get; set; }

        [Display(Name = "Fiador")]
        public virtual Fiador Fiador { get; set; }
    }
}