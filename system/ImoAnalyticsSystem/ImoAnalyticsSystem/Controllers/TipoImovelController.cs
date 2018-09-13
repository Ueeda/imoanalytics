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
using PagedList;

namespace ImoAnalyticsSystem.Controllers
{
    public class TipoImovelController : Controller
    {
        private TipoImovelBusiness tipoImovelController = new TipoImovelBusiness();

        // GET: TipoImovel
        [Authorize]
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(tipoImovelController.GetTiposImovel().ToPagedList(pageNumber, pageSize));
        }

        // GET: TipoImovel/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoImovel tipoImovel = tipoImovelController.FindById(id);
            if (tipoImovel == null)
            {
                return HttpNotFound();
            }
            return View(tipoImovel);
        }

        // GET: TipoImovel/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoImovel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,Tipo")] TipoImovel tipoImovel)
        {
            string create = "";
            if (ModelState.IsValid)
            {
                create = tipoImovelController.Create(tipoImovel);
                if(create.Equals("OK"))
                    return RedirectToAction("Index");
            }

            if(!create.Equals(""))
                ModelState.AddModelError("Erro ao criar o tipo de imóvel: ", create);
            return View(tipoImovel);
        }

        // GET: TipoImovel/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoImovel tipoImovel = tipoImovelController.FindById(id);
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
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,Tipo")] TipoImovel tipoImovel)
        {
            string edit = "";
            if (ModelState.IsValid)
            {
                edit = tipoImovelController.Create(tipoImovel);
                if (edit.Equals("OK"))
                    return RedirectToAction("Index");
            }

            if (!edit.Equals(""))
                ModelState.AddModelError("Erro ao editar o tipo de imóvel: ", edit);
            return View(tipoImovel);
        }

        // GET: TipoImovel/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoImovel tipoImovel = tipoImovelController.FindById(id);
            if (tipoImovel == null)
            {
                return HttpNotFound();
            }
            return View(tipoImovel);
        }

        // POST: TipoImovel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoImovel tipoImovel = tipoImovelController.FindById(id);
            tipoImovelController.Delete(tipoImovel);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                tipoImovelController.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}