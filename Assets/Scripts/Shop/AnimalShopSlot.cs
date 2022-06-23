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
                    PlaceAnimalRandomlyOnScreen();
                    _animalShop.RemoveElement(Element, 1);
                } else {
                    // Debug.Log("Not enough money to buy Animal.");
                }

                _animalShop.onElementChangedCallback?.Invoke();
            }
        }

        /**
         * Places cow randomly on Screen where no other invalid object is
         */
        private void PlaceAnimalRandomlyOnScreen() {
            bool objectIsAtSpawnPos;
            Vector2 spawnPosition = new Vector2();
            do {
                objectIsAtSpawnPos = false;
                float spawnY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y,
                    Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
                float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x,
                    Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

                spawnPosition = new Vector2(spawnX, spawnY);

                // check if any Object is already at that position
                RaycastHit2D hit = Physics2D.Raycast(spawnPosition, Vector2.up, 0f);
                if(hit.collider != null) {
                    if(hit.collider.name == "House" 
                       || hit.collider.GetComponent<Animal>() 
                       || hit.collider.name == "Fence") {
                        objectIsAtSpawnPos = true;
                    }
                }
            } while(objectIsAtSpawnPos);

            Instantiate(Element.animalPrefab, spawnPosition, Quaternion.identity);
        }
    }
}