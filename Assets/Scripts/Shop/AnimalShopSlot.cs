using TMPro;
using UnityEngine;

namespace Shop {
    
    public class AnimalShopSlot : ElementStorageSlot<Animal> {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI costText;

        private AnimalShop _animalShop;
        private PlayerController _playerController;

        private void Start() {
            _animalShop = AnimalShop.instance;
            _playerController = PlayerController.instance;
        }

        /**
     * Clears the Shop Slot
     */
        public override void ClearSlot() {
            nameText.text = "";
            costText.text = "";
            amountText.text = "";
            base.ClearSlot();
        }

        /**
     * Gets called when the Shop Slot is clicked
     */
        public override void UseElement() {
            if(Element) {
                if(_playerController.Money >= Element.price) {
                    if(Element) {
                        _playerController.ChangeMoney(-Element.price);
                        // Debug.Log("Buying Animal: " + Element.displayName);
                    }

                    PlaceCowRandomlyOnScreen();
                    _animalShop.RemoveElement(Element, 1);
                } else {
                    // Debug.Log("Not enough money to buy Animal.");
                }

                _animalShop.onElementChangedCallback?.Invoke();
            }
        }

        private void PlaceCowRandomlyOnScreen() {
            float spawnY = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y,
                Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x,
                Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            Instantiate(Element.animalPrefab, spawnPosition, Quaternion.identity);
        }
    }
}