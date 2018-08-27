using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Models
{
    public class Proprietario
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório")]
        [MaxLength(250, ErrorMessage = "O tamanho máximo do nome é de 250 caracteres.")]
        [Display(Name = "Nome completo")]
        public String NomeCompleto { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [MaxLength(14, ErrorMessage = "O tamanho máximo do CPF é de 14 caracteres.")]
        [Display(Name = "CPF")]
        public String Cpf { get; set; }

        [Required(ErrorMessage = "O RG é obrigatório.")]
        [Display(Name = "RG")]
        public String Rg { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [Display(Name = "Data de nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone]
        [Display(Name = "Número do celular")]
        public String Telefone { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [Display(Name = "CEP")]
        public String Cep { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        [Display(Name = "Endereço")]
        public String Endereco { get; set; }

        [Required(ErrorMessage = "O número é obrigatório.")]
        [Display(Name = "Número")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "O bairro é obrigatório.")]
        [Display(Name = "Bairro")]
        public String Bairro { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [Display(Name = "Cidade")]
        public String Cidade { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [Display(Name = "Estado")]
        public String Estado { get; set; }

        [Required(ErrorMessage = "A conta bancária é obrigatóra.")]
        [Display(Name = "Conta bancária")]
        public String ContaBancaria { get; set; }

        [Required(ErrorMessage = "A agência é obrigatória.")]
        [Display(Name = "Agência")]
        public String Agencia { get; set; }

        [Required(ErrorMessage = "O banco é obrigatório.")]
        [Display(Name = "Banco")]
        public String Banco { get; set; }

        [ForeignKey("ProprietarioId")]
        public virtual ICollection<Imovel> Imoveis { get; set; }

        [Required]
        public Boolean Ativo { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public String Email { get; set; }
    }
}