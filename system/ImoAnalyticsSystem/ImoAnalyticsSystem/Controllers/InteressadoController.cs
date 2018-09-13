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
    public class InteressadoController : Controller
    {
        private InteressadoBusiness interessadoBusiness = new InteressadoBusiness();

        // GET: Interessado
        [Authorize(Roles = "Gerente")]
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;
            List<Interessado> interessados;

            if(!String.IsNullOrEmpty(searchString))
            {
                interessados = interessadoBusiness.SearchInteressadosByNome(searchString);
                if(interessados.Count() == 0)
                    ViewBag.noResults = true;
            }
            else
                interessados = interessadoBusiness.GetInteressados();

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(interessados.OrderBy(i => i.NomeCompleto).ToPagedList(pageNumber, pageSize));
        }

        // GET: Interessado/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interessado interessado = interessadoBusiness.FindById(id);
            if (interessado == null)
            {
                return HttpNotFound();
            }
            return View(interessado);
        }

        // GET: Interessado/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Interessado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,NomeCompleto,Cpf,Rg,DataNascimento,Telefone,Cep,Endereco,Numero,Bairro,Cidade,Estado,Email")] Interessado interessado)
        {
            string create = "";
            if (ModelState.IsValid)
            {
                create = interessadoBusiness.Create(interessado);
                if(create.Equals("OK"))
                    return RedirectToAction("Index");
            }

            if (!create.Equals(""))
                ModelState.AddModelError("Erro ao criar o fiador: ", create);
            return View(interessado);
        }

        // GET: Interessado/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interessado interessado = interessadoBusiness.FindById(id);
            if (interessado == null)
            {
                return HttpNotFound();
            }
            return View(interessado);
        }

        // POST: Interessado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,NomeCompleto,Cpf,Rg,DataNascimento,Telefone,Cep,Endereco,Numero,Bairro,Cidade,Estado,Email")] Interessado interessado)
        {
            string edit = "";
            if (ModelState.IsValid)
            {
                edit = interessadoBusiness.Edit(interessado);
                if (edit.Equals("OK"))
                    return RedirectToAction("Index");
            }

            if (!edit.Equals(""))
                ModelState.AddModelError("Erro ao editar o interessado: ", edit);
            return View(interessado);
        }

        // GET: Interessado/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interessado interessado = interessadoBusiness.FindById(id);
            if (interessado == null)
            {
                return HttpNotFound();
            }
            return View(interessado);
        }

        // POST: Interessado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Interessado interessado = interessadoBusiness.FindById(id);
            interessadoBusiness.Delete(interessado);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                interessadoBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}