﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Models
{
    public class Visita
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Interessado")]
        [Required(ErrorMessage = "O interessado que marcou a visita é obrigatório.")]
        public int InteressadoId { get; set; }

        [Display(Name = "Corretor")]
        [Required(ErrorMessage = "O corretor responsável pela visita é obrigatório.")]
        public string CorretorId { get; set; }

        [Display(Name = "Imóvel")]
        [Required(ErrorMessage = "O imóvel que receberá a visita é obrigatório.")]
        public int ImovelId { get; set; }

        [Display(Name = "Data da visita")]
        [Required(ErrorMessage = "A data da visita é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [Display(Name = "Hora da visita")]
        [Required(ErrorMessage = "A hora da visita é obrigatória.")]
        [DataType(DataType.Time)]
        public DateTime Horario { get; set; }

        [Display(Name = "Descrição da visita")]
        public String Descricao { get; set; }

        [Display(Name = "Interessado")]
        public virtual Interessado Interessado { get; set; }

        [Display(Name = "Corretor")]
        public virtual Corretor Corretor { get; set; }

        [Display(Name = "Código do imóvel")]
        public virtual Imovel Imovel { get; set; }
    }
}