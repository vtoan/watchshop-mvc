using aspcore_watchshop.Daos;
using aspcore_watchshop.EF;
using aspcore_watchshop.Entities;


namespace aspcore_watchshop.Models
{
    public interface IInfoModel
    {
        Info GetInfo(watchContext context);
    }
    public class InfoModel : IInfoModel
    {
        public Info GetInfo(watchContext context)
        {
            Info asset = null;
            using (InfoDao db = new InfoDao())
                asset = db.Get(context);
            return asset;
        }
    }
}