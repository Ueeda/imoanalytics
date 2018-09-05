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

        public List<Imovel> GetImoveis()
        {
            return db.Imovel.ToList();
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
            db.Imovel.Add(imovel);
            db.SaveChanges();
            return "OK";
        }

        public string Edit(Imovel imovel, HttpPostedFileBase upload)
        {
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
            return "OK";
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