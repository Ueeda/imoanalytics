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
    public class VisitaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        VisitaBusiness vb = new VisitaBusiness();

        // GET: Visita
        [Authorize]
        public ActionResult Index()
        {
            var visitas = db.Visita.Include(v => v.Corretor).Include(v => v.Imovel).Include(v => v.Interessado);
            return View(visitas.ToList());
        }

        // GET: Visita/Details/5
        [Authorize]
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
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CorretorId = new SelectList(db.Users, "Id", "NomeCompleto");
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "ID");
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto");
            return View();
        }

        // POST: Visita/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,InteressadoId,CorretorId,ImovelId,Data,Horario,Descricao")] Visita visita)
        {
            int create = 1;
            if (ModelState.IsValid)
            {
                create = vb.Create(visita);
                if(create == 0)
                    return RedirectToAction("Index");
            }

            if(create == 1)
                ModelState.AddModelError("Imovel", "O imóvel já possui visita agendada para essa data e hora");
            else if (create == 2)
                ModelState.AddModelError("Corretor", "O corretor já possui visita agendada para essa data e hora");
            else if (create == 3)
                ModelState.AddModelError("Imovel", "O imóvel e o corretor já possuem visita agendada para essa data e hora");
            ViewBag.CorretorId = new SelectList(db.Users, "ID", "NomeCompleto", visita.CorretorId);
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "ID", visita.ImovelId);
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto", visita.InteressadoId);
            return View(visita);
        }

        // GET: Visita/Edit/5
        [Authorize]
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
            ViewBag.CorretorId = new SelectList(db.Users, "ID", "NomeCompleto", visita.CorretorId);
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "ID", visita.ImovelId);
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto", visita.InteressadoId);
            return View(visita);
        }

        // POST: Visita/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,InteressadoId,CorretorId,ImovelId,Data,Descricao")] Visita visita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CorretorId = new SelectList(db.Users, "ID", "NomeCompleto", visita.CorretorId);
            ViewBag.ImovelId = new SelectList(db.Imovel, "ID", "ID", visita.ImovelId);
            ViewBag.InteressadoId = new SelectList(db.Interessado, "ID", "NomeCompleto", visita.InteressadoId);
            return View(visita);
        }

        // GET: Visita/Delete/5
        [Authorize]
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
        [Authorize]
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