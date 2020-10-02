using System;
using System.Collections.Generic;
using aspcore_watchshop.EF;
using aspcore_watchshop.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace aspcore_watchshop.Daos
{
    public class PromotionDao : IDisposable
    {
        public void Dispose() { }

        public List<Promotion> GetList(watchContext context)
        {
            return context.Promotions.ToList();
        }

        public List<PromProduct> GetListPromProducts(watchContext context)
        {
            DateTime dNow = DateTime.Now;
            Promotion proms = context.Promotions
                .Where(prom => prom.Status == true)
                .Where(prom => prom.FromDate <= dNow && dNow <= prom.ToDate)
                .Where(prom => prom.Type == 0)
                .Include(prom => prom.PromProducts).FirstOrDefault();
            return proms == null ? null : proms.PromProducts;
        }

        public List<PromBill> GetListPromBills(watchContext context)
        {
            DateTime dNow = DateTime.Now;
            Promotion proms = context.Promotions
                .Where(prom => prom.Status == true)
                .Where(prom => prom.FromDate <= dNow && dNow <= prom.ToDate)
                .Where(prom => prom.Type == 1)
                .Include(prom => prom.PromBills).FirstOrDefault();
            return proms == null ? null : proms.PromBills;
        }
    }
}