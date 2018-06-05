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
    public class CartorioController : Controller
    {
        private ImoAnalyticsContext db = new ImoAnalyticsContext();

        // GET: Cartorio
        public ActionResult Index()
        {
            return View(db.Cartorio.ToList());
        }

        // GET: Cartorio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cartorio cartorio = db.Cartorio.Find(id);
            if (cartorio == null)
            {
                return HttpNotFound();
            }
            return View(cartorio);
        }

        // GET: Cartorio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cartorio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NomeCartorio")] Cartorio cartorio)
        {
            if (ModelState.IsValid)
            {
                db.Cartorio.Add(cartorio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cartorio);
        }

        // GET: Cartorio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cartorio cartorio = db.Cartorio.Find(id);
            if (cartorio == null)
            {
                return HttpNotFound();
            }
            return View(cartorio);
        }

        // POST: Cartorio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NomeCartorio")] Cartorio cartorio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartorio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cartorio);
        }

        // GET: Cartorio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cartorio cartorio = db.Cartorio.Find(id);
            if (cartorio == null)
            {
                return HttpNotFound();
            }
            return View(cartorio);
        }

        // POST: Cartorio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cartorio cartorio = db.Cartorio.Find(id);
            db.Cartorio.Remove(cartorio);
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
