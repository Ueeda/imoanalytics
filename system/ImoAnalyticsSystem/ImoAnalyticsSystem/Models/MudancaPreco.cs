using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Models
{
    public class MudancaPreco
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataMudanca { get; set; }
        public decimal? ValorVenda { get; set; }
        public decimal? ValorLocacao { get; set; }
        public bool Venda { get; set; }
        public bool Locacao { get; set; }
        public bool FirstRegister { get; set; }

        public int ImovelId { get; set; }
        public virtual Imovel Imovel { get; set; }
    }
}