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

        [Display(Name = "Valor do aluguel")]
        [Required(ErrorMessage = "O valor da locação é obrigatória.")]
        public double Valor { get; set; }

        [Display(Name = "Data de início")]
        [Required(ErrorMessage = "A data de início da locacao é obrigatória.")]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Data de fim")]
        [Required(ErrorMessage = "A data de fim da locacação é obrigatória.")]
        public DateTime DataFim { get; set; }

        [Display(Name = "Data de pagamento")]
        [Required(ErrorMessage = "A data de pagamento é obrigatória.")]
        public int DataPagamento { get; set; }
    }
}