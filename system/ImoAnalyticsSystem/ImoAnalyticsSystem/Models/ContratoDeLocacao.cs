using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Models
{
    public class ContratoDeLocacao
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Valor da locação")]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        [Display(Name = "Taxa de administração")]
        [DataType(DataType.Currency)]
        public decimal ValorTaxaAdm { get; set; }

        [Display(Name = "Data de início do contrato")]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Data de fim do contrato")]
        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        [Display(Name = "Dia de pagamento do contrato")]
        public int DataPagamento { get; set; }
    }
}