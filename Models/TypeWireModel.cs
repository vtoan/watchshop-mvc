using System.Collections.Generic;
using aspcore_watchshop.Daos;
using aspcore_watchshop.EF;
using aspcore_watchshop.Entities;
using aspcore_watchshop.Hepler;

namespace aspcore_watchshop.Models
{
    public interface IWireModel
    {
        List<WireVM> GetWireVMs(watchContext context);
    }
    public class TypeWireModel : IWireModel
    {
        public List<WireVM> GetWireVMs(watchContext context)
        {
            List<TypeWire> asset = null;
            using (TypeWireDao db = new TypeWireDao())
                asset = db.GetList(context);
            return Helper.LsObjectMapperTo<WireVM, TypeWire>(asset);
        }
    }

    public class WireVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}