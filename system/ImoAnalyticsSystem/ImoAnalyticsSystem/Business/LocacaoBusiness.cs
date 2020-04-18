using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using ImoAnalyticsSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class LocacaoBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Locacao> GetLocacoes()
        {
            return db.Locacao.Include(l => l.ContratoDeLocacao).Include(l => l.Fiador).Include(l => l.Imovel).Include(l => l.Interessado).ToList();
        }

        public List<Locacao> GetLocacoesByStartAndEndTime(DateTime? startTime, DateTime? endTime)
        {
            return db.Locacao.Include(l => l.ContratoDeLocacao).Include(l => l.Fiador).Include(l => l.Imovel).Include(l => l.Interessado)
                .Where(l => l.ContratoDeLocacao.DataInicio >= startTime && l.ContratoDeLocacao.DataFim <= endTime).ToList();
        }

        public List<Locacao> GetLocacoesByStartTime(DateTime? startTime)
        {
            return db.Locacao.Include(l => l.ContratoDeLocacao).Include(l => l.Fiador).Include(l => l.Imovel).Include(l => l.Interessado)
                .Where(l => l.ContratoDeLocacao.DataInicio >= startTime).ToList();
        }

        public List<Locacao> GetLocacoesByEndTime(DateTime? endTime)
        {
            return db.Locacao.Include(l => l.Interessado).Include(l => l.Imovel).Include(l => l.Fiador).Include(l => l.ContratoDeLocacao)
                .Where(l => l.ContratoDeLocacao.DataInicio <= endTime).ToList();
        }

        public Locacao FindById(int? id)
        {
            return db.Locacao.Find(id);
        }

        public string Create(LocacaoViewModel model)
        {
            var codigo = db.Locacao.Where
                (
                    c => String.Compare(model.CodigoLocacao, c.CodigoLocacao, false) == 0
                );

            if (codigo.Count() == 0)
            {
                ImovelBusiness imovelBusiness = new ImovelBusiness();
                var locacao = new Locacao { DataOperacao = model.DataOperacao, InteressadoId = model.InteressadoId, ImovelId = model.ImovelId, FiadorId = model.FiadorId, CodigoLocacao = model.CodigoLocacao };
                var contratoLocacao = new ContratoDeLocacao { Valor = model.Valor, DataInicio = model.DataInicio, DataFim = model.DataFim, DataPagamento = model.DataPagamento };

                locacao.ContratoDeLocacao = contratoLocacao;
                this.CalculaTaxaAdm(locacao);
                imovelBusiness.Unavailable(locacao.ImovelId);
                db.Locacao.Add(locacao);
                db.SaveChanges();
                return "OK";
            }

            string response = "";
            if (codigo.Count() != 0)
                response += "Já existe uma locação cadastrada com esse código.";
            return response;
        }

        public string Edit(LocacaoViewModel model, int locacaoId, int IdImovelAntigo)
        {
            ImovelBusiness imovelBusiness = new ImovelBusiness();
            var codigo = db.Locacao.Where
                (
                    c => String.Compare(model.CodigoLocacao, c.CodigoLocacao, false) == 0 && c.ID != locacaoId
                );

            if (codigo.Count() == 0)
            {
                var locacao = this.FindById(locacaoId);
                locacao.CodigoLocacao = model.CodigoLocacao;
                locacao.DataOperacao = model.DataOperacao;
                locacao.FiadorId = model.FiadorId;
                locacao.ImovelId = model.ImovelId;
                locacao.InteressadoId = model.InteressadoId;
                locacao.ContratoDeLocacao.DataFim = model.DataFim;
                locacao.ContratoDeLocacao.DataInicio = model.DataInicio;
                locacao.ContratoDeLocacao.DataPagamento = model.DataPagamento;
                locacao.ContratoDeLocacao.Valor = model.Valor;

                if (IdImovelAntigo != locacao.ImovelId)
                    imovelBusiness.ChangeUnavailable(IdImovelAntigo, model.ImovelId);
                this.CalculaTaxaAdm(locacao);
                db.Entry(locacao).State = EntityState.Modified;
                db.SaveChanges();
                return "OK";
            }

            string response = "";
            if (codigo.Count() != 0)
                response += "Já existe uma locação cadastrada com esse código.";
            return response;
        }

        public void Delete(Locacao locacao)
        {
            db.Locacao.Remove(locacao);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void CalculaTaxaAdm(Locacao locacao)
        {
            ImobiliariaBusiness imobiliariaBusiness = new ImobiliariaBusiness();
            Imobiliaria imobiliaria = imobiliariaBusiness.GetInstance();
            locacao.ContratoDeLocacao.ValorTaxaAdm = locacao.ContratoDeLocacao.Valor * imobiliaria.TaxaAdministracaoLocacao;
        }
    }
}