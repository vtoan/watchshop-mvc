using System;
using System.Collections.Generic;
using System.Linq;
using aspcore_watchshop.EF;
using aspcore_watchshop.Entities;

namespace aspcore_watchshop.Daos
{
    public class PolicyDao : IDisposable
    {
        public void Dispose() { }
        public List<Policy> GetList(watchContext context)
        {
            return context.Policies.ToList();
        }

        public void Add() { }
        public void Remove() { }

    }
}