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
    public class VendaController : Controller
    {
        private VendaBusiness vendaBusiness = new VendaBusiness();
        private InteressadoBusiness interessadoBusiness = new InteressadoBusiness();
        private ImovelBusiness imovelBusiness = new ImovelBusiness();
        private CorretorBusiness corretorBusiness = new CorretorBusiness();
        
        // GET: Venda
        [Authorize]
        public ActionResult Index()
        {
            return View(vendaBusiness.getVendas());
        }

        // GET: Venda/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = vendaBusiness.FindById(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // GET: Venda/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CorretorId = new SelectList(corretorBusiness.GetCorretores(), "ID", "NomeCompleto");
            ViewBag.ImovelId = new SelectList(imovelBusiness.GetImoveisDisponiveis(), "ID", "ID");
            ViewBag.InteressadoId = new SelectList(interessadoBusiness.GetInteressados(), "ID", "NomeCompleto");
            return View();
        }

        // POST: Venda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,DataVenda,ImovelId,CorretorId,InteressadoId,ValorVenda,ComissaoImobiliaria,ComissaoCorretor")] Venda venda)
        {
            String create = "";
            if (ModelState.IsValid)
            {
                create = vendaBusiness.Create(venda);
                if (create.Equals("OK"))
                    return RedirectToAction("Index");
            }

            if (!create.Equals(""))
                ModelState.AddModelError("Erro ao cadastrar venda.", create);

            ViewBag.CorretorId = new SelectList(corretorBusiness.GetCorretores(), "ID", "NomeCompleto", venda.CorretorId);
            ViewBag.ImovelId = new SelectList(imovelBusiness.GetImoveisDisponiveis(), "ID", "ID", venda.ImovelId);
            ViewBag.InteressadoId = new SelectList(interessadoBusiness.GetInteressados(), "ID", "NomeCompleto", venda.InteressadoId);
            return View(venda);
        }

        // GET: Venda/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = vendaBusiness.FindById(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            ViewBag.CorretorId = new SelectList(corretorBusiness.GetCorretores(), "ID", "NomeCompleto", venda.CorretorId);
            ViewBag.ImovelId = new SelectList(imovelBusiness.GetImoveisDisponiveis(), "ID", "ID", venda.ImovelId);
            ViewBag.InteressadoId = new SelectList(interessadoBusiness.GetInteressados(), "ID", "NomeCompleto", venda.InteressadoId);
            return View(venda);
        }

        // POST: Venda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,DataVenda,ImovelId,CorretorId,InteressadoId,ValorVenda,ComissaoImobiliaria,ComissaoCorretor")] Venda venda)
        {

            string edit = "";
            if (ModelState.IsValid)
            {

                edit = vendaBusiness.Edit(venda);
                if (edit.Equals("OK"))
                    return RedirectToAction("Index");
            }

            if (!edit.Equals(""))
                ModelState.AddModelError("Erro ao cadastrar venda.", edit);

            ViewBag.CorretorId = new SelectList(corretorBusiness.GetCorretores(), "ID", "NomeCompleto", venda.CorretorId);
            ViewBag.ImovelId = new SelectList(imovelBusiness.GetImoveisDisponiveis(), "ID", "ID", venda.ImovelId);
            ViewBag.InteressadoId = new SelectList(interessadoBusiness.GetInteressados(), "ID", "NomeCompleto", venda.InteressadoId);
            return View(venda);
        }

        // GET: Venda/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = vendaBusiness.FindById(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // POST: Venda/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venda venda = vendaBusiness.FindById(id);
            vendaBusiness.Delete(venda);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                vendaBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}