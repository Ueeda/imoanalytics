using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class ImovelBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProprietarioBusiness proprietarioBusiness = new ProprietarioBusiness();

        public List<Imovel> GetImoveis()
        {
            return db.Imovel.ToList();
        }

        public List<Imovel> GetImoveisDisponiveis()
        {
            return db.Imovel.Where(v => v.Disponivel == true).ToList();
        }

        public List<Imovel> GetImoveisLocacao()
        {
            return GetImoveisDisponiveis().Where(v => v.Locacao == true).ToList();
        }

        public List<Imovel> GetImoveisVenda()
        {
            return GetImoveisDisponiveis().Where(v => v.Venda == true).ToList();
        }

        public Imovel FindById(int? id)
        {
            return db.Imovel.Include(s => s.Files).SingleOrDefault(s => s.ID == id);
        }

        public string Create(Imovel imovel, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                var imagem = new Imagem
                {
                    FileName = System.IO.Path.GetFileName(upload.FileName),
                    FileType = FileType.Foto,
                    ContentType = upload.ContentType
                };
                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    imagem.Content = reader.ReadBytes(upload.ContentLength);
                }
                imovel.Files = new List<Imagem> { imagem };
            }

            imovel.DataCadastro = DateTime.Now;
            var historico = new MudancaPreco();
            historico.Locacao = imovel.Locacao;
            historico.Venda = imovel.Venda;
            if (imovel.Venda)
                historico.ValorVenda = imovel.ValorVenda;
            else
                historico.ValorVenda = 0;

            if (imovel.Locacao)
                historico.ValorLocacao = imovel.ValorLocacao;
            else
                historico.ValorLocacao = 0;

            historico.DataMudanca = DateTime.Now;
            
            imovel.HistoricoPrecos = new List<MudancaPreco>();
            imovel.HistoricoPrecos.Add(historico);

            db.Imovel.Add(imovel);
            db.SaveChanges();
            proprietarioBusiness.UpdateActive(imovel.ProprietarioId);
            return "OK";
        }

        public string Edit(Imovel imovel, HttpPostedFileBase upload)
        {
            //MudancaPreco historico = null;
            //if (imovel.HistoricoPrecos.Count() > 0)
            //    historico = imovel.HistoricoPrecos.ElementAt(0);

            //if (imovel.Locacao != historico.Locacao || imovel.Venda != historico.Venda || imovel.ValorVenda != historico.ValorVenda || imovel.ValorLocacao != historico.ValorLocacao || historico == null)
            //{
            //    var historicoNew = new MudancaPreco();
            //    historicoNew.Locacao = imovel.Locacao;
            //    historicoNew.Venda = imovel.Venda;
            //    if (historicoNew.Venda)
            //        historicoNew.ValorVenda = imovel.ValorVenda;
            //    else
            //        historicoNew.ValorVenda = 0;

            //    if (historicoNew.Locacao)
            //        historicoNew.ValorLocacao = imovel.ValorLocacao;
            //    else
            //        historicoNew.ValorLocacao = 0;

            //    historicoNew.DataMudanca = DateTime.Now;
            //    imovel.HistoricoPrecos.Add(historicoNew);
            //}
            db.Entry(imovel).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            var imovelToUpdate = this.FindById(imovel.ID);
            if (upload != null && upload.ContentLength > 0)
            {
                if (imovelToUpdate.Files.Any(f => f.FileType == FileType.Foto))
                {
                    //Remover imagem antiga
                    db.Imagens.Remove(imovelToUpdate.Files.First(f => f.FileType == FileType.Foto));
                }
                var imagem = new Imagem
                {
                    FileName = System.IO.Path.GetFileName(upload.FileName),
                    FileType = FileType.Foto,
                    ContentType = upload.ContentType
                };
                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    imagem.Content = reader.ReadBytes(upload.ContentLength);
                }
                imovelToUpdate.Files = new List<Imagem> { imagem };
            }

            
            db.Entry(imovelToUpdate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            proprietarioBusiness.UpdateActive(imovel.ProprietarioId);
            return "OK";
        }

        public void Unavailable(int id)
        {
            Imovel imovel = FindById(id);
            imovel.Disponivel = false;
            db.Entry(imovel).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void ChangeUnavailable(int oldId, int newId)
        {
            Imovel oldImovel = this.FindById(oldId);
            Imovel newImovel = this.FindById(newId);
            oldImovel.Disponivel = true;
            newImovel.Disponivel = false;
            db.Entry(oldImovel).State = System.Data.Entity.EntityState.Modified;
            db.Entry(newImovel).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Imovel imovel)
        {
            db.Imovel.Remove(imovel);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}