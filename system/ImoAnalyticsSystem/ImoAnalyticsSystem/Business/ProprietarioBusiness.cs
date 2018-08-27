using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class ProprietarioBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public string Create(Proprietario proprietario)
        {
            var cpf = db.Proprietario.Where
                (
                    p => p.Cpf == proprietario.Cpf
                );

            var rg = db.Proprietario.Where
                (
                    p => p.Rg == proprietario.Rg
                );

            var email = db.Proprietario.Where
                (
                    p => p.Email == proprietario.Email
                );

            if(cpf.Count() == 0 && rg.Count() == 0 && email.Count() == 0)
            {
                db.Proprietario.Add(proprietario);
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
    }
}