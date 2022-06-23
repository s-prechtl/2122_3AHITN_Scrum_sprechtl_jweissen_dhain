using UnityEngine;

namespace Shop {
    public class AnimalShop : ElementStorage<Animal> {
        #region Singleton

        public static AnimalShop instance;

        private void Awake() {
            if(instance != null) {
                Debug.LogWarning("More than one instance of AnimalShop found");
            }

            instance = this;
        }

        #endregion
    }
}