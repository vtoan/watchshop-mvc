using System;
using System.Collections.Generic;
using aspcore_watchshop.Daos;
using aspcore_watchshop.EF;
using aspcore_watchshop.Entities;
using aspcore_watchshop.Hepler;

namespace aspcore_watchshop.Models
{
    public interface IPromModel
    {
        List<PromVM> GetPromVMs(watchContext context);
        List<PromProductVM> GetPromProductVMs(watchContext context);
        List<PromBillVM> GetPromBillVMs(watchContext ctext);

    }

    public class PromModel : IPromModel
    {
        public List<PromVM> GetPromVMs(watchContext context)
        {

            List<Promotion> asset = null;
            //Database
            using (PromotionDao db = new PromotionDao())
                asset = db.GetList(context);
            if (asset == null) return null;
            //Convert result  
            return Helper.LsObjectMapperTo<PromVM, Promotion>(asset);
        }

        public List<PromProductVM> GetPromProductVMs(watchContext context)
        {
            List<PromProduct> asset = null;
            //Database
            using (PromotionDao db = new PromotionDao())
                asset = db.GetListPromProducts(context);
            //Convert result
            return Helper.LsObjectMapperTo<PromProductVM, PromProduct>(asset);
        }

        public List<PromBillVM> GetPromBillVMs(watchContext ctext)
        {
            List<PromBill> asset = null;
            //Database
            using (PromotionDao db = new PromotionDao())
                asset = db.GetListPromBills(ctext);
            //Convert result
            return Helper.LsObjectMapperTo<PromBillVM, PromBill>(asset);
        }
    }
    public class PromVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool Status { get; set; }
        public byte Type { get; set; }
    }

    public class PromProductVM
    {
        public int ID { get; set; }
        public int PromotionID { get; set; }
        public double Discount { get; set; }
        public int[] ProductIDs { get; set; }
        public int CategoryID { get; set; }
        public int BandID { get; set; }
    }

    public class PromBillVM
    {
        public int ID { get; set; }
        public int PromotionID { get; set; }
        public double Discount { get; set; }
        public string ItemFree { get; set; }
        public int ConditionItem { get; set; }
        public int ConditionAmount { get; set; }
    }
}