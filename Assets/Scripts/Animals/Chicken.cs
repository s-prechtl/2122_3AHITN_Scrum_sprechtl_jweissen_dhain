using Actions;

namespace Animals {
    public class Chicken : Animal {
        void Start() {
            HouseController.NewDayEvent.AddListener(LayEgg);
        }
        
        /**
         * Gives a Random amount of eggs to the player
         * Directly into the Inventory
         */
        private void LayEgg() {
            ActionManager.Instance.NextDayAction(gameObject);
        }
    }
}
