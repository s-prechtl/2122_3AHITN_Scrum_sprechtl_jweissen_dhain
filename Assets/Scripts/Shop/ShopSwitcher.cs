using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop {


    public class ShopSwitcher : MonoBehaviour {
        public Sprite animalShopSprite;
        public Sprite itemShopSprite;
        public Image switchButtonImage;
        public TextMeshProUGUI shopTitleText;
        public GameObject viewportItems;
        public GameObject viewportAnimals;
        public GameObject undoPurchaseButton;

        private void Start() {
            switchButtonImage.enabled = true;
            switchButtonImage.sprite = animalShopSprite;

            viewportItems.SetActive(true);
            viewportAnimals.SetActive(false);
        }

        public void SwitchShops() {
            // switch Shop Title text and Image of the Button
            if(shopTitleText.text.StartsWith("Item")) {
                shopTitleText.text = "Animal \nShop";
                switchButtonImage.sprite = itemShopSprite;
            } else {
                shopTitleText.text = "Item \nShop";
                switchButtonImage.sprite = animalShopSprite;
            }

            // switch the shown shops
            viewportItems.SetActive(!viewportItems.activeSelf);
            viewportAnimals.SetActive(!viewportAnimals.activeSelf);

            // turn off undoPurchaseButton when opening Animal Shop because of lack of undoing purchases in the Animal Shop
            undoPurchaseButton.SetActive(!undoPurchaseButton.activeSelf);

            // remove undo purchase button when switching to an animal shop and fix content alignement
        }
    }
}
