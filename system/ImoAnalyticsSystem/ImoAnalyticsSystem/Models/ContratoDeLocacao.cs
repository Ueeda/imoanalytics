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
        
        public double Valor { get; set; }
        
        public DateTime DataInicio { get; set; }
        
        public DateTime DataFim { get; set; }
        
        public int DataPagamento { get; set; }
    }
}