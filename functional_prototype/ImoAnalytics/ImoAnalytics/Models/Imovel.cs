using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ImoAnalytics.Models
{
    public class Imovel
    {
        [Key]
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Tipo do imóvel é obrigatório.")]
        public int TipoImovelId { get; set; }
        
        [Required(ErrorMessage = "O cartório é obigatório.")]
        public int CartorioId { get; set; }

        [Required(ErrorMessage = "O proprietário precisa ser informado.")]
        public int ProprietarioId { get; set; }

        public virtual Proprietario Proprietario { get; set; }

        public virtual Cartorio Cartorio { get; set; }

        public virtual TipoImovel TipoImovel { get; set; }
    }
}