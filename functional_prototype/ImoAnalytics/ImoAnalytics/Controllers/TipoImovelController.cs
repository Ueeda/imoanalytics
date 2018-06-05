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
    public class TipoImovelController : Controller
    {
        private ImoAnalyticsContext db = new ImoAnalyticsContext();

        // GET: TipoImovel
        public ActionResult Index()
        {
            return View(db.TipoImovel.ToList());
        }

        // GET: TipoImovel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoImovel tipoImovel = db.TipoImovel.Find(id);
            if (tipoImovel == null)
            {
                return HttpNotFound();
            }
            return View(tipoImovel);
        }

        // GET: TipoImovel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoImovel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Tipo")] TipoImovel tipoImovel)
        {
            if (ModelState.IsValid)
            {
                db.TipoImovel.Add(tipoImovel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoImovel);
        }

        // GET: TipoImovel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoImovel tipoImovel = db.TipoImovel.Find(id);
            if (tipoImovel == null)
            {
                return HttpNotFound();
            }
            return View(tipoImovel);
        }

        // POST: TipoImovel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Tipo")] TipoImovel tipoImovel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoImovel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoImovel);
        }

        // GET: TipoImovel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoImovel tipoImovel = db.TipoImovel.Find(id);
            if (tipoImovel == null)
            {
                return HttpNotFound();
            }
            return View(tipoImovel);
        }

        // POST: TipoImovel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoImovel tipoImovel = db.TipoImovel.Find(id);
            db.TipoImovel.Remove(tipoImovel);
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
