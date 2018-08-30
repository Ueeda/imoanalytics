using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class FiadorBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Fiador> GetFiadores()
        {
            return db.Fiador.ToList();
        }

        public Fiador FindById(int? id)
        {
            return db.Fiador.Find(id);
        }

        public string Create(Fiador fiador)
        {
            var cpf = db.Fiador.Where
                (
                    f => f.Cpf == fiador.Cpf
                );

            var rg = db.Fiador.Where
                (
                    f => f.Rg == fiador.Rg
                );

            var email = db.Fiador.Where
                (
                    f => f.Email == fiador.Email
                );

            if (cpf.Count() == 0 && rg.Count() == 0 && email.Count() == 0)
            {
                db.Fiador.Add(fiador);
                db.SaveChanges();
                return "OK";
            }
            string response = "";
            if (cpf.Count() != 0)
                response += "O CPF informado já foi cadastrado. ";
            if (rg.Count() != 0)
                response += "O RG informado já foi cadastrado. ";
            if (email.Count() != 0)
                response += "O e-mail informado já foi cadastrado. ";
            return response;
        }

        public string Edit(Fiador fiador)
        {
            var cpf = db.Fiador.Where
                (
                    f => f.Cpf == fiador.Cpf && f.ID != fiador.ID
                );

            var rg = db.Fiador.Where
                (
                    f => f.Rg == fiador.Rg && f.ID != fiador.ID
                );

            var email = db.Fiador.Where
                (
                    f => f.Email == fiador.Email && f.ID != fiador.ID
                );

            if (cpf.Count() == 0 && rg.Count() == 0 && email.Count() == 0)
            {
                db.Entry(fiador).State = EntityState.Modified;
                db.SaveChanges();
                return "OK";
            }
            string response = "";
            if (cpf.Count() != 0)
                response += "O CPF informado já foi cadastrado. ";
            if (rg.Count() != 0)
                response += "O RG informado já foi cadastrado. ";
            if (email.Count() != 0)
                response += "O e-mail informado já foi cadastrado. ";
            return response;
        }

        public void Delete(Fiador fiador)
        {
            db.Fiador.Remove(fiador);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}