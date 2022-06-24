using UnityEngine;

namespace Actions {
    /// <summary>
    /// Implementing classes handle nextDayAction
    /// </summary>
    public interface NextDayActionHandler {
        public void InvokeAction(GameObject gameObject);
        public bool Matches(GameObject gameObject);
    }
}