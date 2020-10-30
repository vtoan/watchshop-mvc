using System;
using System.Collections.Generic;
using System.Linq;
using aspcore_watchshop.Daos;
using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;
using aspcore_watchshop.Interfaces;
using aspcore_watchshop.ViewModels;

namespace aspcore_watchshop.Models {
    public enum PromType {
        PRODUCT,
        BILL
    }
    public class PromModel : DataModelBase<PromVM, Promotion>, IPromModel {
        public PromModel () : base (new PromotionDao ()) { }

        public new PromVM GetVM (watchContext context, int id) {
            Promotion asset = null;
            using (PromotionDao db = new PromotionDao ())
            asset = db.Get (context, id);
            if (asset == null) return null;
            dynamic subProm = null;
            if (asset.PromBill != null) subProm = DataHelper.Instance.ObjectMapperTo<PromBillVM, Promotion> (asset);
            if (asset.PromProduct != null) subProm = DataHelper.Instance.ObjectMapperTo<PromProductVM, Promotion> (asset);
            return subProm;
        }

        public bool AddModel (watchContext context, PromVM newObj, PromType promType) {
            var prom = DataHelper.Instance.ObjectMapperTo<Promotion, PromVM> (newObj);
            using (PromotionDao db = new PromotionDao ()) {
                switch (promType) {
                    case PromType.PRODUCT:
                        return db.InsertForProduct (context, prom, DataHelper.Instance.ObjectMapperTo<PromProduct, PromVM> (newObj));
                    case PromType.BILL:
                        return db.InsertForBill (context, prom, DataHelper.Instance.ObjectMapperTo<PromBill, PromVM> (newObj));
                }
            }
            return false;
        }

        public List<PromBillVM> GetListVMBills (watchContext context) {
            List<Promotion> asset = null;
            using (PromotionDao db = new PromotionDao ())
            asset = db.GetListAvaiable (context, 1).ToList ();
            return DataHelper.Instance.LsObjectMapperTo<PromBillVM, Promotion> (asset);
        }

        public List<PromProductVM> GetListVMProducts (watchContext context) {
            List<Promotion> asset = null;
            using (PromotionDao db = new PromotionDao ())
            asset = db.GetListAvaiable (context, 0).ToList ();
            return DataHelper.Instance.LsObjectMapperTo<PromProductVM, Promotion> (asset);
        }

        public bool UpdateStatus (watchContext context, int id, bool stt) {
            using (PromotionDao db = new PromotionDao ())
            return db.UpdateStatus (context, id, stt);
        }
    }
}