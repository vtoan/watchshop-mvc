using System;
using aspcore_watchshop.EF;
using aspcore_watchshop.Entities;
using System.Linq;
namespace aspcore_watchshop.Daos
{
    public class InfoDao : IDisposable
    {
        public void Dispose() { }

        public Info Get(watchContext context)
        {
            return context.Infos.FirstOrDefault();
        }
    }
}