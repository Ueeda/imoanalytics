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
            var codigo = db.Imovel.Where
                (
                    i => String.Compare(i.CodigoReferencia, imovel.CodigoReferencia, false) == 0
                );
            if (codigo.Count() == 0)
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

                imovel.DataCadastro = DateTime.Now.Date;
                MudancaPrecoBusiness mudancaPrecoBusiness = new MudancaPrecoBusiness();
                mudancaPrecoBusiness.StartNewHistory(imovel);

                db.Imovel.Add(imovel);
                db.SaveChanges();
                proprietarioBusiness.UpdateActive(imovel.ProprietarioId);
                return "OK";
            }
            string response = "";
            if (codigo.Count() != 0)
                response += "Já existe um imóvel com o código registrado no sistema.";
            return response;
        }

        public string Edit(Imovel imovel, HttpPostedFileBase upload)
        {
            var codigo = db.Imovel.Where
                (
                    i => String.Compare(i.CodigoReferencia, imovel.CodigoReferencia, false) == 0 && i.ID != imovel.ID
                );
            if (codigo.Count() == 0)
            {
                MudancaPrecoBusiness mudancaPrecoBusiness = new MudancaPrecoBusiness();
                mudancaPrecoBusiness.Create(imovel);
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
            string response = "";
            if (codigo.Count() != 0)
                response += "Já existe um imóvel com o código registrado no sistema.";
            return response;
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