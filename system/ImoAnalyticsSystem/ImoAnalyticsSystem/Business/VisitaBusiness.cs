using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class VisitaBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public string Create(Visita visita)
        {
            var imovel = db.Visita.Where
                (
                    v => v.Data == visita.Data && v.Horario == visita.Horario && v.ImovelId == visita.ImovelId
                ).ToList();

            var corretor = db.Visita.Where
                (
                    v => v.Data == visita.Data && v.Horario == visita.Horario && v.CorretorId == visita.CorretorId
                ).ToList();

            var interessado = db.Visita.Where
                (
                    v => v.Data == visita.Data && v.InteressadoId == visita.InteressadoId
                );

            if (imovel.Count() == 0 && corretor.Count() == 0 && interessado.Count() == 0)
            {
                db.Visita.Add(visita);
                db.SaveChanges();
                return "OK";
            }
            string response = "";
            if (imovel.Count() != 0)
                response += "o imóvel já possui visita agendada para esse horário. ";
            if (interessado.Count() != 0)
                response += "O interessado já possui visita agendada para esse horário. ";
            if (corretor.Count() != 0)
                response += "O corretor já possui visita agendada para esse horário. ";
            return response;
        }
    }
}