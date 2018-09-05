using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class InteressadoBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Interessado> GetInteressados()
        {
            return db.Interessado.ToList();
        }

        public List<Interessado> SearchInteressadosByNome(String searchString)
        {
            return db.Interessado.Where(p => p.NomeCompleto.Contains(searchString)).ToList();
        }

        public Interessado FindById(int? id)
        {
            return db.Interessado.Find(id);
        }

        public string Create(Interessado interessado)
        {
            var cpf = db.Interessado.Where
                (
                    i => i.Cpf == interessado.Cpf
                );

            var rg = db.Interessado.Where
                (
                    i => i.Rg == interessado.Rg
                );

            var email = db.Interessado.Where
                (
                    i => i.Email == interessado.Email
                );

            if (cpf.Count() == 0 && rg.Count() == 0 && email.Count() == 0)
            {
                db.Interessado.Add(interessado);
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

        public string Edit(Interessado interessado)
        {
            var cpf = db.Interessado.Where
                (
                    i => i.Cpf == interessado.Cpf && i.ID != interessado.ID
                );

            var rg = db.Interessado.Where
                (
                    i => i.Rg == interessado.Rg && i.ID != interessado.ID
                );

            var email = db.Interessado.Where
                (
                    i => i.Email == interessado.Email && i.ID != interessado.ID
                );

            if (cpf.Count() == 0 && rg.Count() == 0 && email.Count() == 0)
            {
                db.Entry(interessado).State = EntityState.Modified;
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

        public void Delete(Interessado interessado)
        {
            db.Interessado.Remove(interessado);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}