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

        [Display(Name = "Número")]
        [Required(ErrorMessage = "O campo número é obrigatório.")]
        public int Numero { get; set; }

        [Display(Name = "Complemento")]
        [MaxLength(50, ErrorMessage = "O tamanho máximo do campo complemento é de 50 caracteres.")]
        public String Complemento { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "O campo bairro é obrigatório.")]
        [MaxLength(150, ErrorMessage = "O tamanho máximo do campo bairro é de 150 caracteres.")]
        public String Bairro { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "O campo cidade é obrigatório.")]
        [MaxLength(250, ErrorMessage = "O tamanho máximo do campo cidade é de 250 caracteres.")]
        public String Cidade { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "O campo estado é obrigatório.")]
        [MaxLength(250, ErrorMessage = "O tamanho máximo do campo estado é de 100 caracteres.")]
        public String Estado { get; set; }

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
        public decimal ComissaoImobiliariaVenda { get; set; }

        [Display(Name = "Comissão de venda do corretor")]
        [Required(ErrorMessage = "O campo comissão de venda do corretor é obrigatório.")]
        public decimal ComissaoCorretorVenda { get; set; }

        [Display(Name = "Taxa de administração da locação")]
        [Required(ErrorMessage = "O campo taxa de administração da locação é obrigatório.")]
        public decimal TaxaAdministracaoLocacao { get; set; }
    }
}