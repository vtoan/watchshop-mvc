using System;
using aspcore_watchshop.EF;
using aspcore_watchshop.Entities;
using System.Linq;
using System.Collections.Generic;

namespace aspcore_watchshop.Daos
{
    public class FeeDao : IDisposable
    {
        public void Dispose() { }

        public List<Fee> GetList(watchContext ctext)
        {
            return ctext.Fees.ToList();
        }

    }
}