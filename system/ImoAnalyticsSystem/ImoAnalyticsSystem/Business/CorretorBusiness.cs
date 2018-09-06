﻿using ImoAnalyticsSystem.Data;
using ImoAnalyticsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImoAnalyticsSystem.Business
{
    public class CorretorBusiness
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Corretor> GetCorretores()
        {
            return db.Users.ToList();
        }

        public List<Corretor> SearchCorretoresByNome(String searchString)
        {
            return db.Users.Where(p => p.NomeCompleto.Contains(searchString)).ToList();
        }

    }
}