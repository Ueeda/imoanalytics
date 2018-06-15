﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImoAnalytics.Models
{
    public class Corretor
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Nome do corretor")]
        [Required(ErrorMessage = "O nome completo é obrigatório")]
        [MaxLength(250, ErrorMessage = "O tamanho máximo do nome é de 250 caracteres.")]
        public String NomeCompleto { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [MaxLength(11, ErrorMessage = "O tamanho máximo do CPF é de 11 caracteres.")]
        public String Cpf { get; set; }

        [Display(Name = "RG")]
        [Required(ErrorMessage = "O RG é obrigatório.")]
        public String Rg { get; set; }

        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone]
        public String Telefone { get; set; }

        [Display(Name = "CEP")]
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        public String Cep { get; set; }

        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public String Endereco { get; set; }

        [Display(Name = "Numero")]
        [Required(ErrorMessage = "O número é obrigatório.")]
        public int Numero { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "O bairro é obrigatório.")]
        public String Bairro { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "A cidade é obrigatória.")]
        public String Cidade { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "O estado é obrigatório.")]
        public String Estado { get; set; }

        [Display(Name = "Creci")]
        [Required(ErrorMessage = "O creci é obrigatório.")]
        public int Creci { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress]
        public String Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "A senha é obrigatório.")]
        public String Senha { get; set; }
    }
}