using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Models
{
    public class Interessado
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório")]
        [MaxLength(250, ErrorMessage = "O tamanho máximo do nome é de 250 caracteres.")]
        public String NomeCompleto { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [MaxLength(14, ErrorMessage = "O tamanho máximo do CPF é de 14 caracteres.")]
        public String Cpf { get; set; }

        [Required(ErrorMessage = "O RG é obrigatório.")]
        public String Rg { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone]
        public String Telefone { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        public String Cep { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public String Endereco { get; set; }

        [Required(ErrorMessage = "O número é obrigatório.")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "O bairro é obrigatório.")]
        public String Bairro { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        public String Cidade { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        public String Estado { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress]
        public String Email { get; set; }
    }
}