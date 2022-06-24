using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

namespace Actions {
    /// <summary>
    /// Implementing classes handle ClickActions
    /// </summary>
    public interface ClickActionHandler {
        public void InvokeAction(GameObject gameObject);
        public bool Matches(GameObject gameObject, UsableItem usableItem);
    }
}