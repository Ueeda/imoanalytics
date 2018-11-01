using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Models
{
    public class Relatorio
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Tipo da ação")]
        public TipoRelatorio TipoRelatorio { get; set; }

        [Display(Name = "Título do relatório")]
        public String TituloRelatorio { get; set; }

        [Display(Name = "Relatório privado")]
        public Boolean Privado { get; set; }

        [Display(Name = "Data início do relatório")]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Data fim do relatório")]
        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        [Display(Name = "Tipo da ação")]
        public TipoAcao TipoAcao { get; set; }

        [Display(Name = "Endereço")]
        public String Endereco { get; set; }

        [Display(Name = "Valor de venda")]
        public decimal ValorVenda { get; set; }

        [Display(Name = "Valor de locação")]
        public decimal ValorLocacao { get; set; }

        [Display(Name = "Quartos")]
        public int QntdDormitorios { get; set; }

        [Display(Name = "Vagas de garagem")]
        public int QntdVagasGaragem { get; set; }

        [Display(Name = "Banheiros")]
        public int QntdBanheiros { get; set; }

        [Display(Name = "Suítes")]
        public int QntdSuites { get; set; }

        [Display(Name = "Bairro")]
        public String Bairro { get; set; }

        [Display(Name = "Tipo de imóvel")]
        public int TipoImovelId { get; set; }

        [Display(Name = "Área total máxima")]
        public int AreaTotalMáxima { get; set; }

        [Display(Name = "Área total mínima")]
        public int AreaTotalMinima { get; set; }

        public String CorretorId { get; set; }

        [Display(Name = "Corretor")]
        public virtual Corretor Corretor { get; set; }
    }

    public enum TipoRelatorio
    {
        [Display(Name = "Imóvel")]
        Imovel = 1,

        [Display(Name = "Locação/Venda/Visita")]
        Acao = 2,

        [Display(Name = "Estatísticas gerais")]
        Estatisticas = 3    
    }

    public enum TipoAcao
    {
        [Display(Name = "Locação")]
        Locacao = 1,

        [Display(Name = "Venda")]
        Venda = 2,

        [Display(Name = "Visita")]
        Visita = 3
    }
}