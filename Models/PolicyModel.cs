using System.Collections.Generic;
using aspcore_watchshop.Daos;
using aspcore_watchshop.EF;
using aspcore_watchshop.Entities;

namespace aspcore_watchshop.Models
{
    public interface IPolicyModel
    {
        List<Policy> GetPolices(watchContext context);
    }
    public class PolicyModel : IPolicyModel
    {
        public List<Policy> GetPolices(watchContext context)
        {
            List<Policy> asset = null;
            using (PolicyDao db = new PolicyDao())
                asset = db.GetList(context);
            return asset == null || asset.Count == 0 ? null : asset;
        }

        public void AddPolicy() { }
        public void RemotePolicy() { }
    }
}