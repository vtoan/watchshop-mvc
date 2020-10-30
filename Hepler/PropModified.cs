using System.Collections.Generic;

namespace aspcore_watchshop.Hepler {
    public class PropModified<T> {
        private Dictionary<string, dynamic> modifiedProps;

        public bool isChanged { get; }

        public PropModified (T target) {
            GetOf (target);
            isChanged = modifiedProps.Count == 0 ? false : true;
        }

        public bool UpdateFor (ref T target) {
            if (modifiedProps.Count == 0 || target == null) return false;
            //Update Prop Modifired to Target;
            var targetProps = target.GetType ();
            foreach (var item in modifiedProps) {
                var prop = targetProps.GetProperty (item.Key);
                if (prop != null) prop.SetValue (target, item.Value);
            }
            return true;
        }

        public Dictionary<string, dynamic> GetOf (T target) {
            modifiedProps = new Dictionary<string, dynamic> ();
            if (target == null) return modifiedProps;
            var srcProps = target.GetType ().GetProperties ();
            foreach (var p in srcProps) {
                if (p.GetValue (target) != null) {
                    if (p.Name != "Id")
                        modifiedProps.Add (p.Name, p.GetValue (target));

                }
            }
            return modifiedProps;
        }
    }
}