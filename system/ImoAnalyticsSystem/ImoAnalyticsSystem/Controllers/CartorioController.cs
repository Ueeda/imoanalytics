using ImoAnalyticsSystem.Business;
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
    public class CartorioController : Controller
    {
        private CartorioBusiness cartorioBusiness = new CartorioBusiness();

        // GET: Cartorio
        [Authorize]
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;
            List<Cartorio> cartorios;

            if (!String.IsNullOrEmpty(searchString))
            {
                cartorios = cartorioBusiness.SearchCartoriosByNome(searchString);
                if (cartorios.Count() == 0)
                    ViewBag.noResults = true;
            }
            else
                cartorios = cartorioBusiness.GetCartorios();

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(cartorios.OrderBy(t => t.NomeCartorio).ToPagedList(pageNumber, pageSize));
        }

        // GET: Cartorio/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cartorio cartorio = cartorioBusiness.FindById(id);
            if (cartorio == null)
            {
                return HttpNotFound();
            }
            return View(cartorio);
        }

        // GET: Cartorio/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cartorio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,NomeCartorio")] Cartorio cartorio)
        {
            string create = "";
            if (ModelState.IsValid)
            {
                create = cartorioBusiness.Create(cartorio);
                if(create.Equals("OK"))
                    return RedirectToAction("Index");
            }

            if(!create.Equals(""))
                ModelState.AddModelError("Erro ao criar o cartório: ", create);
            return View(cartorio);
        }

        // GET: Cartorio/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cartorio cartorio = cartorioBusiness.FindById(id); ;
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
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,NomeCartorio")] Cartorio cartorio)
        {
            string edit = "";
            if (ModelState.IsValid)
            {
                edit = cartorioBusiness.Edit(cartorio);
                if(edit.Equals("OK"))
                    return RedirectToAction("Index");
            }

            if (!edit.Equals(""))
                ModelState.AddModelError("Erro ao editar o cartório: ", edit);
            return View(cartorio);
        }

        // GET: Cartorio/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cartorio cartorio = cartorioBusiness.FindById(id);
            if (cartorio == null)
            {
                return HttpNotFound();
            }
            return View(cartorio);
        }

        // POST: Cartorio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Cartorio cartorio = cartorioBusiness.FindById(id);
            cartorioBusiness.Delete(cartorio);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                cartorioBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}