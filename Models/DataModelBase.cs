using System.Collections.Generic;
using aspcore_watchshop.Daos;
using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;

namespace aspcore_watchshop.Models {
    public abstract class DataModelBase<T, V> where V : class // Define CURD methods  bassic for model class
    {
        // T is ViewModel
        // V is Entity
        protected DAO<V> db { get; set; }

        public DataModelBase (DAO<V> dAO = null) {
            db = dAO == null ? new DAO<V> () : dAO;
        }

        public virtual List<T> GetListVMs (watchContext context) {
            using (db)
            return DataHelper.Instance.LsObjectMapperTo<T, V> (db.GetList (context));
        }

        public virtual T GetVM (watchContext context, int id) {
            using (db)
            return DataHelper.Instance.ObjectMapperTo<T, V> (db.Get (context, id));
        }

        public virtual bool AddModel (watchContext context, T newObj) {
            if (newObj == null) return false;
            using (db)
            return db.Insert (context, DataHelper.Instance.ObjectMapperTo<V, T> (newObj));
        }

        public virtual bool AddRangeModel (watchContext context, List<T> newLsObj) {
            if (newLsObj == null || newLsObj.Count == 0) return false;
            using (db)
            return db.InsertRange (context, DataHelper.Instance.LsObjectMapperTo<V, T> (newLsObj));
        }

        public virtual bool UpdateModel (watchContext context, int idSrc, T objVM) {
            if (objVM == null) return false;
            V obj = DataHelper.Instance.ObjectMapperTo<V, T> (objVM);
            PropModified<V> modifieds = new PropModified<V> (obj);
            if (!modifieds.isChanged) return false;
            using (db)
            return db.Update (context, idSrc, modifieds);
        }

        public virtual bool RemoveModel (watchContext context, int id) {
            using (db)
            return db.Remove (context, id);
        }
    }
}