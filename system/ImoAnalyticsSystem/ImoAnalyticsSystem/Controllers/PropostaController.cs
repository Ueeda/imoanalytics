using ImoAnalyticsSystem.Business;
using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ImoAnalyticsSystem.Controllers
{
    public class PropostaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private PropostaBusiness propostaBusiness = new PropostaBusiness();
        // GET: Proposta
        [Authorize]
        public ActionResult Index()
        {
            var propostas = propostaBusiness.GetPropostas();
            return View(propostas);
        }

        // GET: Proposta/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proposta proposta = propostaBusiness.FindById(id);
            if (proposta == null)
            {
                return HttpNotFound();
            }
            return View(proposta);
        }

        // GET: Proposta/Create
        //[Authorize]
        //public ActionResult Create()
        //{
        //    ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "TituloImovel");
        //    ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto");
        //    return View();
        //}

        // GET: Proposta/Create
        [Authorize]
        public ActionResult Create(int id)
        {
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "TituloImovel", id);
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto");
            return View();
        }

        // POST: Proposta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,InteressadoId,ImovelId,Data,Valor")] Proposta proposta)
        {

            string create = "";
            if (ModelState.IsValid)
            {
                create = propostaBusiness.Create(proposta);
                if (create.Equals("OK"))
                {
                    return RedirectToAction("Index");
                }
            }

            if (!create.Equals(""))
                ModelState.AddModelError("Erro ao criar o cartório: ", create);
            
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "TituloImovel", proposta.ImovelId);
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto", proposta.InteressadoId);
            return View(proposta);
        }

        // GET: Proposta/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proposta proposta = propostaBusiness.FindById(id);
            if (proposta == null)
            {
                return HttpNotFound();
            }
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "TituloImovel", proposta.ImovelId);
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto", proposta.InteressadoId);
            return View(proposta);
        }

        // POST: Proposta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,InteressadoId,ImovelId,Data,Valor")] Proposta proposta)
        {
            string edit = "";
            if (ModelState.IsValid)
            {
                edit = propostaBusiness.Edit(proposta);
                if (edit.Equals("OK"))
                    return RedirectToAction("Index");
               
            }

            if(!edit.Equals(""))
                ModelState.AddModelError("Erro ao editar o proposta: ", edit);

            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "TituloImovel", proposta.ImovelId);
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto", proposta.InteressadoId);
            return View(proposta);
        }

        // GET: Proposta/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proposta proposta = propostaBusiness.FindById(id);
            if (proposta == null)
            {
                return HttpNotFound();
            }
            return View(proposta);
        }

        // POST: Proposta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Proposta proposta = propostaBusiness.FindById(id);
            propostaBusiness.Delete(proposta);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}