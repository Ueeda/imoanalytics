using ImoAnalyticsSystem.Business;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ImoAnalyticsSystem.ViewModels.IdentityViewModels;
using Microsoft.AspNet.Identity;

namespace ImoAnalyticsSystem.Controllers
{
    public class CorretorController : Controller
    {
        CorretorBusiness corretorBusiness = new CorretorBusiness();
        
        // GET: Corretor
        [Authorize]
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;
            List<Corretor> corretores;

            if (!String.IsNullOrEmpty(searchString))
            {
                corretores = corretorBusiness.SearchCorretoresByNome(searchString);
                if (corretores.Count() == 0)
                    ViewBag.noResults = true;
            }
            else
                corretores = corretorBusiness.GetCorretores();

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(corretores.OrderBy(c => c.NomeCompleto).ToPagedList(pageNumber, pageSize));
        }
    }
}