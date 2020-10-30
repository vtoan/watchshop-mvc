using aspcore_watchshop.Daos.ExtendDaos;
using aspcore_watchshop.EF;
using aspcore_watchshop.Interfaces;

namespace aspcore_watchshop.Models.ExtendModels
{
    public class InfoModel : DataModelBase<Info, Info>, IInfoModel
    {
        public InfoModel() : base(new InfoDao()) { }
    }
}