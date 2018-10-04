using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Models
{
    public class Locacao
    {
        [Key]
        public int ID { get; set; }
        public String CodigoLocacao { get; set; }
        public DateTime DataOperacao { get; set; }        
        public int ImovelId { get; set; }
        public int InteressadoId { get; set; }
        public int ContratoDeLocacaoId { get; set; }        
        public int FiadorId { get; set; }
        
        public virtual Imovel Imovel { get; set; }
        public virtual Interessado Interessado { get; set; }
        public virtual ContratoDeLocacao ContratoDeLocacao { get; set; }
        public virtual Fiador Fiador { get; set; }
    }
}