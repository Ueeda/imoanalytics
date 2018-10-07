using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.ViewModels
{
    public class LocacaoViewModel
    {
        [Display(Name = "Valor do aluguel")]
        [Required(ErrorMessage = "O valor da locação é obrigatória.")]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        [Display(Name = "Data de início")]
        [Required(ErrorMessage = "A data de início da locacao é obrigatória.")]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Data de fim")]
        [Required(ErrorMessage = "A data de fim da locacação é obrigatória.")]
        public DateTime DataFim { get; set; }

        [Display(Name = "Data de pagamento")]
        [Required(ErrorMessage = "A data de pagamento é obrigatória.")]
        public int DataPagamento { get; set; }

        [Display(Name = "Código da locacação")]
        [Required(ErrorMessage = "O código da locação é obrigatória.")]
        public String CodigoLocacao { get; set; }

        [Display(Name = "Data da locação")]
        [Required(ErrorMessage = "A data de locação é obrigatória.")]
        public DateTime DataOperacao { get; set; }

        [Display(Name = "Imóvel")]
        [Required(ErrorMessage = "O imóvel locado é obrigatório.")]
        public int ImovelId { get; set; }

        [Display(Name = "Interessado")]
        [Required(ErrorMessage = "O interessado que alugou o imóvel é obrigatório.")]
        public int InteressadoId { get; set; }

        [Display(Name = "Fiador")]
        [Required(ErrorMessage = "O fiador é obrigatório.")]
        public int FiadorId { get; set; }

        public int LocacaoId { get; set; }
    }
}