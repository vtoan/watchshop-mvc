using System;
using System.Collections.Generic;
using System.Linq;
using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;
using Microsoft.EntityFrameworkCore;

namespace aspcore_watchshop.Daos {
    public class PromotionDao : DAO<Promotion> {
        public bool InsertForProduct (watchContext context, Promotion newObj, PromProduct newObjProducts) {
            newObj.PromProduct = newObjProducts;
            newObj.Type = 0;
            base.Insert (context, newObj);
            return true;
        }

        public bool InsertForBill (watchContext context, Promotion newObj, PromBill newObjBill) {
            newObj.PromBill = newObjBill;
            newObj.Type = 1;
            base.Insert (context, newObj);
            return true;
        }

        public override Promotion Get (watchContext context, int id) {
            return context.Promotions.Where (item => item.Id == id)
                .Include (prom => prom.PromBill)
                .Include (prom => prom.PromProduct)
                .FirstOrDefault ();
        }

        public override bool Update (watchContext context, int id, PropModified<Promotion> modifieds) {
            var obj = context.Find<Promotion> (id);
            if (obj == null) return false;
            modifieds.UpdateFor (ref obj);
            context.SaveChanges ();
            return true;
        }

        public bool UpdateStatus (watchContext context, int id, bool stt) {
            Promotion p = context.Promotions.Where (item => item.Id == id).FirstOrDefault ();
            if (p == null) return false;
            p.Status = stt;
            context.SaveChangesAsync ();
            return true;
        }

        // ============== Client =============
        public List<Promotion> GetListAvaiable (watchContext context, int typeProm) {
            DateTime dNow = DateTime.Now;
            var refDb = context.Promotions
                .Where (prom => prom.Status == true)
                .Where (prom => prom.FromDate <= dNow && dNow <= prom.ToDate)
                .Where (prom => prom.Type == typeProm);
            if (typeProm == 0) return refDb.Include (prom => prom.PromProduct).ToList ();
            if (typeProm == 1) return refDb.Include (prom => prom.PromBill).ToList ();
            return refDb.ToList ();
        }

    }
}