using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class VendaBusiness
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Venda> GetVendas()
        {
            return db.Venda.Include(v => v.Corretor).Include(v => v.Imovel).Include(v => v.Interessado).ToList();
        }

        public List<Venda> GetVendasByStartAndEndTime(DateTime? startTime, DateTime? endTime)
        {
            return db.Venda.Include(v => v.Corretor).Include(v => v.Imovel).Include(v => v.Interessado).Where(v => v.DataVenda >= startTime && v.DataVenda <= endTime).ToList();
        }

        public List<Venda> GetVendasByStartTime(DateTime? startTime)
        {
            return db.Venda.Include(v => v.Corretor).Include(v => v.Imovel).Include(v => v.Interessado).Where(v => v.DataVenda >= startTime).ToList();
        }

        public List<Venda> GetVendasByEndTime(DateTime? endTime)
        {
            return db.Venda.Include(v => v.Corretor).Include(v => v.Imovel).Include(v => v.Interessado).Where(v => v.DataVenda <= endTime).ToList();
        }

        public Venda FindById(int? id)
        {
            return db.Venda.Find(id);
        }

        public String Create(Venda venda)
        {
            ImovelBusiness imovelBusiness = new ImovelBusiness();
            var codigo = db.Venda.Where
                (
                    v => v.CodigoVenda == venda.CodigoVenda
                );
            if (codigo.Count() == 0)
            {
                imovelBusiness.Unavailable(venda.ImovelId);
                CalculaComissoes(venda);
                db.Venda.Add(venda);
                db.SaveChanges();
                return "OK";
            }
            string response = "";
            if (codigo.Count() != 0)
                response += "Já existe uma venda com o código registrado no sistema.";
            return response;
        }

        public String Edit(Venda venda, int IdImovelAntigo)
        {
            ImovelBusiness imovelBusiness = new ImovelBusiness();
            var codigo = db.Venda.Where
                (
                    v => v.CodigoVenda == venda.CodigoVenda && v.ID != venda.ID
                );
            if (codigo.Count() == 0)
            {
                if (IdImovelAntigo != venda.ImovelId)
                    imovelBusiness.ChangeUnavailable(IdImovelAntigo, venda.ImovelId);
                CalculaComissoes(venda);
                db.Entry(venda).State = EntityState.Modified;
                db.SaveChanges();
                return "OK";
            }
            string response = "";
            if (codigo.Count() != 0)
                response += "Já existe uma venda com o código registrado no sistema.";
            return response;
        }

        public void Delete(Venda venda)
        {
            db.Venda.Remove(venda);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
        
        public void CalculaComissoes(Venda venda)
        {
            ImobiliariaBusiness imobiliariaBusiness = new ImobiliariaBusiness();
            Imobiliaria imobiliaria = imobiliariaBusiness.GetInstance();
            venda.ComissaoImobiliaria = venda.ValorVenda * imobiliaria.ComissaoImobiliariaVenda;
            venda.ComissaoCorretor = venda.ComissaoImobiliaria * imobiliaria.ComissaoCorretorVenda;
        }
    }
}