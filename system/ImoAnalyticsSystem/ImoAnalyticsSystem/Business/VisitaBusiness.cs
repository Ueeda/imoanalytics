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

        public int Create(Visita visita)
        {
            var imovel = db.Visita.Where
                (
                    v => v.Data == visita.Data && v.Horario == visita.Horario && v.ImovelId == visita.ImovelId
                ).ToList();

            var corretor = db.Visita.Where
                (
                    v => v.Data == visita.Data && v.Horario == visita.Horario && v.CorretorId == visita.CorretorId
                ).ToList();

            if (imovel.Count() == 0 && corretor.Count() == 0)
            {
                db.Visita.Add(visita);
                db.SaveChanges();
                return 0;
            }

            if (corretor.Count() != 0 && imovel.Count() != 0)
                return 3;
            if (corretor.Count() != 0)
                return 2;

            return 1;
        }
    }
}