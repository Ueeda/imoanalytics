using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class CorretorBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Corretor> GetCorretores()
        {
            return db.Users.ToList();
        }

        public List<Corretor> SearchCorretoresByNome(String searchString)
        {
            return db.Users.Where(p => p.NomeCompleto.Contains(searchString)).ToList();
        }

        public Corretor GetCurrentUser(string id)
        {
            return db.Users.Single(u => u.Id == id);
        }

        public String ValidateInformation(string cpf, int creci, string rg, string email)
        {
            var cpfValidate = db.Users.Where
                (
                    c => c.Cpf == cpf
                );

            var creciValidate = db.Users.Where
                (
                    c => c.Creci == creci
                );

            var rgValidate = db.Users.Where
                (
                    c => c.Rg == rg
                );

            var emailValidate = db.Users.Where
                (
                    c => c.Email == email
                );

            if (cpfValidate.Count() == 0 && rgValidate.Count() == 0 && creciValidate.Count() == 0)
                return "OK";

            string response = "";
            if (cpfValidate.Count() != 0)
                response += "O CPF informado já foi cadastrado. ";
            if (rgValidate.Count() != 0)
                response += "O RG informado já foi cadastrado. ";
            if (creciValidate.Count() != 0)
                response += "O CRECI informado já foi cadastrado. ";
            if (emailValidate.Count() != 0)
                response += "O e-mail informado já foi cadastrado. ";
            return response;
        }

        public string Edit(Corretor corretor)
        {
            var cpf = db.Users.Where
                (
                    c => c.Cpf == corretor.Cpf && c.Id != corretor.Id
                );

            var rg = db.Users.Where
                (
                    c => c.Rg == corretor.Rg && c.Id != corretor.Id
                );

            var creci = db.Users.Where
                (
                    c => c.Creci == corretor.Creci && c.Id != corretor.Id
                );

            if (cpf.Count() == 0 && rg.Count() == 0 && creci.Count() == 0)
            {
                db.Entry(corretor).State = EntityState.Modified;
                db.SaveChanges();
                return "OK";
            }
            string response = "";
            if (cpf.Count() != 0)
                response += "O CPF informado já foi cadastrado. ";
            if (rg.Count() != 0)
                response += "O RG informado já foi cadastrado. ";
            if (creci.Count() != 0)
                response += "O CRECI informado já foi cadastrado. ";
            return response;
        }

    }
}