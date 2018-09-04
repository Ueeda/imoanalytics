using ImoAnalyticsSystem.Business;
using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ImoAnalyticsSystem.Controllers
{
    public class ImovelController : Controller
    {
        private ImovelBusiness imovelBusiness = new ImovelBusiness();
        private TipoImovelBusiness tipoImovelBusiness = new TipoImovelBusiness();
        private CartorioBusiness cartorioBusiness = new CartorioBusiness();
        private ProprietarioBusiness proprietarioBusiness = new ProprietarioBusiness();

        // GET: Imovel
        [Authorize]
        public ActionResult Index()
        {
            return View(imovelBusiness.GetImoveis());
        }

        // GET: Imovel/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imovel imovel = imovelBusiness.FindById(id);
            if (imovel == null)
            {
                return HttpNotFound();
            }
            return View(imovel);
        }

        // GET: Imovel/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ProprietarioId = new SelectList(proprietarioBusiness.GetProprietarios(), "ID", "NomeCompleto");
            ViewBag.TipoImovelId = new SelectList(tipoImovelBusiness.GetTiposImovel(), "ID", "Tipo");
            ViewBag.CartorioId = new SelectList(cartorioBusiness.GetCartorios(), "ID", "NomeCartorio");
            return View();
        }

        // POST: Imovel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,TituloImovel,Endereco,Complemento,Numero,Cep,Bairro,AnoConstrucao,Venda,Locacao,AreaPrivada,AreaTotal,VagasGaragem,QntBanheiros,QntDormitorios,QntSuites,Disponivel,Reservado,ValorVenda,ValorLocacao,ValorIptu,NomeCondominio,ValorCondominio,NumeroRegistroImovel,DescricaoImovel,ProprietarioId,TipoImovelId,CartorioId")] Imovel imovel, HttpPostedFileBase upload)
        {
            try
            {
                string create = "";
                if (ModelState.IsValid)
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
                    create = imovelBusiness.Create(imovel);
                    if (create.Equals("OK"))
                        return RedirectToAction("Index");
                }

                if (!create.Equals(""))
                    ModelState.AddModelError("Erro ao criar o imovel: ", create);
                ViewBag.ProprietarioId = new SelectList(proprietarioBusiness.GetProprietarios(), "ID", "NomeCompleto");
                ViewBag.TipoImovelId = new SelectList(tipoImovelBusiness.GetTiposImovel(), "ID", "Tipo");
                ViewBag.CartorioId = new SelectList(cartorioBusiness.GetCartorios(), "ID", "NomeCartorio");
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(imovel);
        }

        // GET: Imovel/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imovel imovel = imovelBusiness.FindById(id);
            if (imovel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProprietarioId = new SelectList(proprietarioBusiness.GetProprietarios(), "ID", "NomeCompleto", imovel.ProprietarioId);
            ViewBag.TipoImovelId = new SelectList(tipoImovelBusiness.GetTiposImovel(), "ID", "Tipo", imovel.TipoImovelId);
            ViewBag.CartorioId = new SelectList(cartorioBusiness.GetCartorios(), "ID", "NomeCartorio", imovel.CartorioId);
            return View(imovel);
        }

        // POST: Imovel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,,TituloImovel,Endereco,Complemento,Numero,Cep,Bairro,AnoConstrucao,Venda,Locacao,AreaPrivada,AreaTotal,VagasGaragem,QntBanheiros,QntDormitorios,QntSuites,Disponivel,Reservado,ValorVenda,ValorLocacao,ValorIptu,NomeCondominio,ValorCondominio,NumeroRegistroImovel,DescricaoImovel,ProprietarioId,TipoImovelId,CartorioId,Fotos")] Imovel imovel, HttpPostedFileBase upload)
        {
            string edit = "";
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    if(imovel.Files.Any(f => f.FileType == FileType.Foto))
                    {
                        //Remover imagem antiga
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
                    imovel.Files = new List<Imagem> { imagem };
                }
                edit = imovelBusiness.Edit(imovel);
                if (edit.Equals("OK")) 
                    return RedirectToAction("Index");
            }
            if (!edit.Equals(""))
                ModelState.AddModelError("Erro ao criar o imovel: ", edit);
            ViewBag.ProprietarioId = new SelectList(proprietarioBusiness.GetProprietarios(), "ID", "NomeCompleto", imovel.ProprietarioId);
            ViewBag.TipoImovelId = new SelectList(tipoImovelBusiness.GetTiposImovel(), "ID", "Tipo", imovel.TipoImovelId);
            ViewBag.CartorioId = new SelectList(cartorioBusiness.GetCartorios(), "ID", "NomeCartorio", imovel.CartorioId);
            return View(imovel);
        }

        // GET: Imovel/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imovel imovel = imovelBusiness.FindById(id);
            if (imovel == null)
            {
                return HttpNotFound();
            }
            return View(imovel);
        }

        // POST: Imovel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Imovel imovel = imovelBusiness.FindById(id);
            imovelBusiness.Delete(imovel);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                imovelBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}