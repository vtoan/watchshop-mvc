using System.Collections.Generic;
using aspcore_watchshop.Daos;
using aspcore_watchshop.EF;
using aspcore_watchshop.Entities;
using aspcore_watchshop.Hepler;

namespace aspcore_watchshop.Models
{
    public interface IFeeModel
    {
        List<FeeVM> GetFeeVMs(watchContext context);

    }

    public class FeeModel : IFeeModel
    {
        public List<FeeVM> GetFeeVMs(watchContext ctext)
        {
            List<Fee> asset = null;
            using (FeeDao db = new FeeDao())
                asset = db.GetList(ctext);
            return asset == null ? null : Helper.LsObjectToLsVM<FeeVM, Fee>(asset);
        }

    }

    public class FeeVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
    }
}