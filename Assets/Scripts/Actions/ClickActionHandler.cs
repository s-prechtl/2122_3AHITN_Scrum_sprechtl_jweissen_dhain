using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

namespace Actions {
    public interface ClickActionHandler {
        public void InvokeAction(GameObject gameObject);
        public bool Matches(GameObject gameObject, UsableItem usableItem);
    }
}