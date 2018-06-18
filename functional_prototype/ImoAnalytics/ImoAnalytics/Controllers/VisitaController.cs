using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ImoAnalytics.Models;

namespace ImoAnalytics.Controllers
{
    public class VisitaController : Controller
    {
        private ImoAnalyticsContext db = new ImoAnalyticsContext();

        // GET: Visita
        public ActionResult Index()
        {
            var visitas = db.Visita.Include(v => v.Corretor).Include(v => v.Imovel).Include(v => v.Interessado);
            return View(visitas.ToList());
        }

        // GET: Visita/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visita visita = db.Visita.Find(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            return View(visita);
        }

        // GET: Visita/Create
        public ActionResult Create()
        {
            ViewBag.CorretorId = new SelectList(db.Corretor, "ID", "NomeCompleto");
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "ID");
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto");
            return View();
        }

        // POST: Visita/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,InteressadoId,CorretorId,ImovelId,Data,Descricao")] Visita visita)
        {
            if (ModelState.IsValid)
            {
                db.Visita.Add(visita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CorretorId = new SelectList(db.Corretor, "ID", "NomeCompleto", visita.CorretorId);
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "ID", visita.ImovelId);
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto", visita.InteressadoId);
            return View(visita);
        }

        // GET: Visita/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visita visita = db.Visita.Find(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            ViewBag.CorretorId = new SelectList(db.Corretor, "ID", "NomeCompleto", visita.CorretorId);
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "ID", visita.ImovelId);
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto", visita.InteressadoId);
            return View(visita);
        }

        // POST: Visita/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,InteressadoId,CorretorId,ImovelId,Data,Descricao")] Visita visita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CorretorId = new SelectList(db.Corretor, "ID", "NomeCompleto", visita.CorretorId);
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "ID", visita.ImovelId);
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto", visita.InteressadoId);
            return View(visita);
        }

        // GET: Visita/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visita visita = db.Visita.Find(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            return View(visita);
        }

        // POST: Visita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visita visita = db.Visita.Find(id);
            db.Visita.Remove(visita);
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
