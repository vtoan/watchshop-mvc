using System.Collections.Generic;
using System.Linq;
using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;

namespace aspcore_watchshop.Daos.ExtendDaos {
    public class InfoDao : DAO<Info> {
        public override Info Get (watchContext context, int id) {
            return context.Infos.FirstOrDefault ();
        }

        public override bool Update (watchContext context, int id, PropModified<Info> modifieds) {
            Info obj = context.Infos.Find (1);
            if (obj == null) return false;
            modifieds.UpdateFor (ref obj);
            context.SaveChanges ();
            return true;
        }
    }
}