using UnityEngine;

namespace Actions {
    public interface NextDayActionHandler {
        public void InvokeAction(GameObject gameObject);
        public bool Matches(GameObject gameObject);
    }
}