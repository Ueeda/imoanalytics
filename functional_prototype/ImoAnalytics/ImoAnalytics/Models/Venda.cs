using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImoAnalytics.Models
{
    public class Venda
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Data da venda")]
        [Required(ErrorMessage = "A data da venda é obrigatória")]
        public DateTime DataVenda { get; set; }

        [Display(Name = "Imóvel")]
        [Required(ErrorMessage = "O imóvel vendido é obrigatório.")]
        public int ImovelId { get; set; }

        [Display(Name = "Corretor")]
        [Required(ErrorMessage = "O corretor que vendeu o imóvel é obrigatório.")]
        public int CorretorId { get; set; }

        [Display(Name = "Interessado")]
        [Required(ErrorMessage = "O interessado que comprou o imóvel é obrigatório.")]
        public int InteressadoId { get; set; }

        [Display(Name = "Valor de venda")]
        [Required(ErrorMessage = "O valor da venda é obrigatória.")]
        public double ValorVenda { get; set; }

        [Display(Name = "Comissão da imobiliária")]
        public double ComissaoImobiliaria { get; set; }

        [Display(Name = "Comissão do corretor")]
        public double ComissaoCorretor { get; set; }

        public virtual Imovel Imovel { get; set; }

        public virtual Corretor Corretor { get; set; }
        
        public virtual Interessado Interessado { get; set; }
    }
}