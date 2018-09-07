using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ImoAnalyticsSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace ImoAnalyticsSystem.ViewModels.IdentityViewModels
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
        public Corretor CurrentCorretor { get; set; }
        public bool IsGerente { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }

    public class EditViewModel
    {
        [Display(Name = "Nome do corretor")]
        [Required(ErrorMessage = "O nome completo é obrigatório")]
        [MaxLength(250, ErrorMessage = "O tamanho máximo do nome é de 250 caracteres.")]
        public string NomeCompleto { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [MaxLength(14, ErrorMessage = "O tamanho máximo do CPF é de 14 caracteres.")]
        public string Cpf { get; set; }

        [Display(Name = "RG")]
        [Required(ErrorMessage = "O RG é obrigatório.")]
        public string Rg { get; set; }

        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "CEP")]
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        public string Cep { get; set; }

        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public string Endereco { get; set; }

        [Display(Name = "Numero")]
        [Required(ErrorMessage = "O número é obrigatório.")]
        public int Numero { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "O bairro é obrigatório.")]
        public string Bairro { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "A cidade é obrigatória.")]
        public string Cidade { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "O estado é obrigatório.")]
        public string Estado { get; set; }

        [Display(Name = "Creci")]
        [Required(ErrorMessage = "O creci é obrigatório.")]
        public int Creci { get; set; }

        [Display(Name = "Gerente")]
        public Boolean IsGerente { get; set; }
    }
}