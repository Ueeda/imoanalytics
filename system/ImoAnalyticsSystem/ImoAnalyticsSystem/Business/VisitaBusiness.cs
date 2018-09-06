using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class VisitaBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Visita> GetVisitas()
        {
            return db.Visita.Include(v => v.Corretor).Include(v => v.Imovel).Include(v => v.Interessado).OrderBy(v => v.Data).ToList();
        }

        public List<Visita> GetVisitasByImovelId(int? idImovel)
        {
            return db.Visita.Include(v => v.Corretor).Include(v => v.Imovel).Include(v => v.Interessado).Where(v => v.ImovelId == idImovel).OrderBy(v=> v.Data).ToList();
        }

        public List<Visita> GetVisitasByStartAndEndTime(DateTime? startTime, DateTime? endTime)
        {
            return db.Visita.Include(v => v.Corretor).Include(v => v.Imovel).Include(v => v.Interessado).Where(v => v.Data >= startTime && v.Data <= endTime).ToList();
        }

        public List<Visita> GetVisitasByStartTime(DateTime? startTime)
        {
            return db.Visita.Include(v => v.Corretor).Include(v => v.Imovel).Include(v => v.Interessado).Where(v => v.Data >= startTime).ToList();
        }

        public List<Visita> GetVisitasByEndTime(DateTime? endTime)
        {
            return db.Visita.Include(v => v.Corretor).Include(v => v.Imovel).Include(v => v.Interessado).Where(v => v.Data <= endTime).ToList();
        }

        public Visita FindById(int? id)
        {
            return db.Visita.Find(id);
        }

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
                    v => v.Data == visita.Data && v.Horario == visita.Horario && v.InteressadoId == visita.InteressadoId
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

        public string Edit(Visita visita)
        {
            var imovel = db.Visita.Where
                (
                    v => v.Data == visita.Data && v.Horario == visita.Horario && v.ImovelId == visita.ImovelId && v.ID != visita.ID
                ).ToList();

            var corretor = db.Visita.Where
                (
                    v => v.Data == visita.Data && v.Horario == visita.Horario && v.CorretorId == visita.CorretorId && v.ID != visita.ID
                ).ToList();

            var interessado = db.Visita.Where
                (
                    v => v.Data == visita.Data && v.Horario == visita.Horario && v.InteressadoId == visita.InteressadoId && v.ID != visita.ID
                );

            if (imovel.Count() == 0 && corretor.Count() == 0 && interessado.Count() == 0)
            {
                db.Entry(visita).State = EntityState.Modified;
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

        public void Delete(Visita visita)
        {
            db.Visita.Remove(visita);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}