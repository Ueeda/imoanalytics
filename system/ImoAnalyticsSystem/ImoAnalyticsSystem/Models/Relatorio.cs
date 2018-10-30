using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Models
{
    public class Relatorio
    {
        public String TituloRelatorio { get; set; }
        public Boolean Privado { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal ValorLocacao { get; set; }
        public int QntdDormitorios { get; set; }
        public int QntdVagasGaragem { get; set; }
        public int QntdBanheiros { get; set; }
        public int QntdSuites { get; set; }
        public String Bairro { get; set; }
        public int TipoImovel { get; set; }
    }

    public enum TipoRelatorio
    {
        Imovel = 1,
        Transacao = 2,
        Estatisticas = 3
    }
}