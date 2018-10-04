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
        private TipoImovelBusiness tipoImovelBusiness = new TipoImovelBusiness();

        // GET: TipoImovel
        [Authorize]
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;
            List<TipoImovel> tiposImovel;

            if (!String.IsNullOrEmpty(searchString))
            {
                tiposImovel = tipoImovelBusiness.GetTiposImoveisByName(searchString);
                if (tiposImovel.Count() == 0)
                    ViewBag.noResults = true;
            }
            else
                tiposImovel = tipoImovelBusiness.GetTiposImovel();

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(tiposImovel.OrderBy(t => t.Tipo).ToPagedList(pageNumber, pageSize));
        }

        // GET: TipoImovel/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoImovel tipoImovel = tipoImovelBusiness.FindById(id);
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
                create = tipoImovelBusiness.Create(tipoImovel);
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
            TipoImovel tipoImovel = tipoImovelBusiness.FindById(id);
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
                edit = tipoImovelBusiness.Create(tipoImovel);
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
            TipoImovel tipoImovel = tipoImovelBusiness.FindById(id);
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
            TipoImovel tipoImovel = tipoImovelBusiness.FindById(id);
            tipoImovelBusiness.Delete(tipoImovel);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                tipoImovelBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}