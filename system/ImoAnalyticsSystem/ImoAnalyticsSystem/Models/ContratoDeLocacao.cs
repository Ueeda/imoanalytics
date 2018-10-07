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
        
        public decimal Valor { get; set; }

        public decimal ValorTaxaAdm { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }
        
        public int DataPagamento { get; set; }
    }
}