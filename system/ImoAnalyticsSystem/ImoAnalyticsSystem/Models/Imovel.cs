using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Models
{
    public class Imovel
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Título do imóvel")]
        [Required(ErrorMessage = "O campo título do imóvel é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O tamanho máximo do campo título do imóvel é de 50 caracteres.")]
        public String TituloImovel { get; set; }

        [Display(Name = "Numero de referência")]
        [Required(ErrorMessage = "O campo código de referência é obrigatório")]
        public String CodigoReferencia { get; set; }

        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "O campo endereço é obrigatório.")]
        [MaxLength(250, ErrorMessage = "O tamanho máximo do campo endereço é de 250 caracteres.")]
        public String Endereco { get; set; }

        [Display(Name = "Complemento")]
        [MaxLength(100, ErrorMessage = "O tamanho máximo do campo complemento é de 100 caracteres.")]
        public String Complemento { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "O campo número é obrigatório.")]
        public int Numero { get; set; }

        [Display(Name = "CEP")]
        [Required(ErrorMessage = "O campo CEP é obrigatório.")]
        public String Cep { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "O campo bairro é obrigatório.")]
        [MaxLength(250, ErrorMessage = "O tamanho máximo do campo bairro é de 50 caracteres.")]
        public String Bairro { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de cadastro")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [Display(Name = "Ano de construção")]
        [Required(ErrorMessage = "O campo ano de construção é obrigatório.")]
        public int AnoConstrucao { get; set; }

        [Display(Name = "Venda")]
        public Boolean Venda { get; set; }

        [Display(Name = "Locação")]
        public Boolean Locacao { get; set; }

        [Display(Name = "Área privada")]
        [Required(ErrorMessage = "O campo área privada é obrigatório.")]
        public float AreaPrivada { get; set; }

        [Display(Name = "Área total")]
        [Required(ErrorMessage = "O campo área total é obrigatório.")]
        public float AreaTotal { get; set; }

        [Display(Name = "Número de vagas de garagem")]
        [Required(ErrorMessage = "O campo número de vagas de garagem é obrigatório.")]
        public int VagasGaragem { get; set; }

        [Display(Name = "Número de banheiros")]
        [Required(ErrorMessage = "O campo número de banheiros é obrigatório.")]
        public int QntBanheiros { get; set; }

        [Display(Name = "Número de dormitórios")]
        [Required(ErrorMessage = "O campo número de dormitórios é obrigatório.")]
        public int QntDormitorios { get; set; }

        [Display(Name = "Número de suítes")]
        [Required(ErrorMessage = "O campo número de suítes é obrigatório.")]
        public int QntSuites { get; set; }

        [Display(Name = "Disponível")]
        public Boolean Disponivel { get; set; } = true;

        [Display(Name = "Reservado")]
        public Boolean Reservado { get; set; } = false;

        [Display(Name = "Valor de venda")]
        [DataType(DataType.Currency)]
        public decimal ValorVenda { get; set; }

        [Display(Name = "Valor de locação")]
        [DataType(DataType.Currency)]
        public decimal ValorLocacao { get; set; }

        [Display(Name = "Valor do IPTU")]
        [Required(ErrorMessage = "O campo valor do IPTU é obrigatório.")]
        [DataType(DataType.Currency)]
        public decimal ValorIptu { get; set; }

        [Display(Name = "Nome do condomínio")]
        [Required(ErrorMessage = "O campo nome do condomínio é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O tamanho máximo do campo nome do condomínio é de 50 caracteres.")]
        public String NomeCondominio { get; set; }

        [Display(Name = "Valor do condomínio")]
        [Required(ErrorMessage = "O campo valor do condomínio é obrigatório.")]
        [DataType(DataType.Currency)]
        public decimal ValorCondominio { get; set; }

        [Display(Name = "Número de registro de imóvel")]
        [Required(ErrorMessage = "O campo número de registro de imóvel é obrigatório.")]
        public int NumeroRegistroImovel { get; set; }

        [Display(Name = "Descrição do imóvel")]
        [Required(ErrorMessage = "O campo descrição do imóvel é obrigatório.")]
        [MaxLength(500, ErrorMessage = "O tamanho máximo do campo descrição do imóvel é de 50 caracteres.")]
        public String DescricaoImovel { get; set; }

        [Display(Name = "Ativo")]
        public Boolean Ativo { get; set; } = true;

        [Display(Name = "Tipo de imóvel")]
        [Required(ErrorMessage = "Tipo do imóvel é obrigatório.")]
        public int TipoImovelId { get; set; }

        [Display(Name = "Cartório")]
        [Required(ErrorMessage = "O cartório é obigatório.")]
        public int CartorioId { get; set; }

        [Display(Name = "Proprietário")]
        [Required(ErrorMessage = "O proprietário precisa ser informado.")]
        public int ProprietarioId { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "A cidade precisa ser informada.")]
        public string Cidade { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "O estado precisa ser informado.")]
        public string Estado { get; set; }

        public virtual ICollection<Imagem> Files { get; set; }

        [Display(Name = "Proprietário")]
        public virtual Proprietario Proprietario { get; set; }

        [Display(Name = "Cartório")]
        public virtual Cartorio Cartorio { get; set; }

        [Display(Name = "Tipo de imóvel")]
        public virtual TipoImovel TipoImovel { get; set; }

     
        
    }
}