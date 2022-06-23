using System.Collections.Generic;
using UnityEngine;

namespace Actions {
    public interface ActionHandler {
        public void InvokeAction(GameObject gameObject, UsableItem usableItem);
        public bool Matches(GameObject gameObject, UsableItem usableItem);
    }
}