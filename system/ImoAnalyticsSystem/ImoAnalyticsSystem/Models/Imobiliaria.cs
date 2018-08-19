using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Models
{
    public class Imobiliaria
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Nome da imobiliária")]
        [Required(ErrorMessage = "O campo nome da imobiliária é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O tamanho máximo do nome da imobiliária é de 50 caracteres.")]
        public String NomeImobiliaria { get; set; }

        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "O campo endereço é obrigatório.")]
        [MaxLength(250, ErrorMessage = "O tamanho máximo do campo endereço é de 250 caracteres.")]
        public String Endereco { get; set; }

        [Display(Name = "E-mail de contato")]
        [Required(ErrorMessage = "O campo e-mail de contato é obrigatório.")]
        [EmailAddress]
        public String EmailContato { get; set; }

        [Display(Name = "Telefone de contato")]
        [Required(ErrorMessage = "O campo telefone de contato é obrigatório.")]
        [Phone]
        public String TelefoneContato { get; set; }

        [Display(Name = "Comissão de venda da imobiliária")]
        [Required(ErrorMessage = "O campo comissão de venda da imobiliária é obrigatório.")]
        public double ComissaoImobiliariaVenda { get; set; }

        [Display(Name = "Comissão de venda do corretor")]
        [Required(ErrorMessage = "O campo comissão de venda do corretor é obrigatório.")]
        public double ComissaoCorretorVenda { get; set; }

        [Display(Name = "Taxa de administração da locação")]
        [Required(ErrorMessage = "O campo taxa de administração da locação é obrigatório.")]
        public double TaxaAdministracaoLocacao { get; set; }
    }
}