using System;
using System.Collections.Generic;
using System.Linq;
using aspcore_watchshop.EF;
using aspcore_watchshop.Entities;

namespace aspcore_watchshop.Daos
{
    public class TypeWireDao : IDisposable
    {
        public void Dispose() { }

        public List<TypeWire> GetList(watchContext context)
        {
            return context.TypeWires.ToList();
        }

    }
}