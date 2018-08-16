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

        // GET: Proposta
        public ActionResult Index()
        {
            var propostas = db.Proposta.Include(p => p.Imovel).Include(p => p.Interessado);
            return View(propostas.ToList());
        }

        // GET: Proposta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proposta proposta = db.Proposta.Find(id);
            if (proposta == null)
            {
                return HttpNotFound();
            }
            return View(proposta);
        }

        // GET: Proposta/Create
        public ActionResult Create()
        {
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "TituloImovel");
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto");
            return View();
        }

        // POST: Proposta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,InteressadoId,ImovelId,Valor")] Proposta proposta)
        {
            if (ModelState.IsValid)
            {
                db.Proposta.Add(proposta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "TituloImovel", proposta.ImovelId);
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto", proposta.InteressadoId);
            return View(proposta);
        }

        // GET: Proposta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proposta proposta = db.Proposta.Find(id);
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
        public ActionResult Edit([Bind(Include = "ID,InteressadoId,ImovelId,Valor")] Proposta proposta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proposta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "TituloImovel", proposta.ImovelId);
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto", proposta.InteressadoId);
            return View(proposta);
        }

        // GET: Proposta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proposta proposta = db.Proposta.Find(id);
            if (proposta == null)
            {
                return HttpNotFound();
            }
            return View(proposta);
        }

        // POST: Proposta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proposta proposta = db.Proposta.Find(id);
            db.Proposta.Remove(proposta);
            db.SaveChanges();
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