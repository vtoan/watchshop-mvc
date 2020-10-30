using System;
using System.Collections.Generic;
using System.Linq;
using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;

namespace aspcore_watchshop.Daos {
    public class DAO<T> : IDisposable where T : class {
        public void Dispose () { }

        public virtual T Get (watchContext context, int id) {
            return context.Find<T> (id);;
        }

        public virtual List<T> GetList (watchContext context) {
            return context.Set<T> ().ToList ();
        }

        public virtual bool Insert (watchContext context, T newObj) {
            context.Add<T> (newObj);
            context.SaveChanges ();
            return true;
        }

        public virtual bool InsertRange (watchContext context, List<T> newLsObj) {
            context.Set<T> ().AddRange (newLsObj);
            context.SaveChangesAsync ();
            return true;
        }

        public virtual bool Update (watchContext context, int id, PropModified<T> modifieds) {
            T obj = context.Find<T> (id);
            if (obj == null) return false;
            modifieds.UpdateFor (ref obj);
            context.SaveChangesAsync ();
            return true;
        }

        public virtual bool Remove (watchContext context, int id) {
            T obj = context.Find<T> (id);
            if (obj == null) return false;
            context.Remove<T> (obj);
            context.SaveChangesAsync ();
            return true;
        }
    }
}