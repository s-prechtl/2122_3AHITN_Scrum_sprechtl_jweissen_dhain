using System;
using Tiles;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

namespace DefaultNamespace {
    public class ActionInvoker {
        public static void InvokeAction(GameObject gameObject, UsableItem usableItem) {
            Type tileType = gameObject.GetComponent<TileBehaviour>().Tile.GetType();
            ItemContainer ic = ItemContainer.Instance;

            if(tileType == typeof(GrassTile)) {
                if(usableItem.ID == ic.GetItemIdByName("Hoe")) {
                    SetTile(gameObject, new FarmlandTile());
                } else if(usableItem.ID == ic.GetItemIdByName("Shovel")) {
                    SetTile(gameObject, new WaterTile());
                } else if(usableItem.ID == ic.GetItemIdByName("Fence")) {
                    SetFence(gameObject);
                }
            } else if(tileType == typeof(FarmlandTile)) {
                if(usableItem.ID == ic.GetItemIdByName("Shovel")) {
                    SetTile(gameObject, new GrassTile());
                } else if(usableItem.ID == ic.GetItemIdByName("Scythe")) {
                    HarvestIfPossible(gameObject);
                } else if(usableItem.ID == ic.GetItemIdByName("Watering Can")) {
                    Hydrate(gameObject);
                } else if(usableItem.ID == ic.GetItemIdByName("Wheat Seeds")) {
                    PlantIfPossible(gameObject);
                }
            } else if(tileType == typeof(WaterTile)) {
                if(usableItem.ID == ic.GetItemIdByName("Shovel")) {
                    SetTile(gameObject, new GrassTile());
                } else if(usableItem.ID == ic.GetItemIdByName("Fishing Rod")) {
                    Fish();
                }
            }
        }

        private static void Fish() {
            ItemContainer ic = ItemContainer.Instance;

            FishingController fc = FishingController.instance;
            if(!fc.Fishing) {
                fc.StartFishing();
            } else {
                fc.TryCatch();
            }
        }

        public static void InvokeDayLightStep(GameObject gameObject) {
            Type tileType = gameObject.GetComponent<TileBehaviour>().Tile.GetType();

            if(tileType == typeof(FarmlandTile)) {
                GrowIfHydrated(gameObject);
            }
        }

        private static void GrowIfHydrated(GameObject gameObject) {
            Crop crop = ((FarmlandTile)gameObject.GetComponent<TileBehaviour>().Tile).Crop;

            if(crop.Planted && crop.Hydrated) {
                crop.Grow();
            }
            crop.Hydrated = false;

            UpdateFarmlandSprites(gameObject);
        }

        private static void PlantIfPossible(GameObject gameObject) {
            Crop crop = ((FarmlandTile)gameObject.GetComponent<TileBehaviour>().Tile).Crop;
            if(!crop.Planted) {
                crop.Plant();
                Inventory.instance.RemoveElement(ItemContainer.Instance.GetItemByName("Wheat Seeds"), 1);
            }

            UpdateFarmlandSprites(gameObject);
        }

        private static void Hydrate(GameObject gameObject) {
            Crop crop = ((FarmlandTile)gameObject.GetComponent<TileBehaviour>().Tile).Crop;
            crop.Hydrated = true;
            UpdateFarmlandSprites(gameObject);
        }

        private static void UpdateFarmlandSprites(GameObject gameObject) {
            Crop crop = ((FarmlandTile)gameObject.GetComponent<TileBehaviour>().Tile).Crop;

            SpriteRenderer hydrationSR = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
            SpriteRenderer cropSR = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();

            if(crop.Planted) {
                if(crop.FullyGrown) {
                    //Debug.Log("sprite fg");
                    cropSR.sprite = Crop.FullyGrownCrop;
                } else {
                    //Debug.Log("sprite smallCrop");
                    cropSR.sprite = Crop.SmallCrop;
                }
            } else {
                cropSR.sprite = null;
            }

            if(crop.Hydrated) {
                //Debug.Log("sprite hydrated");
                hydrationSR.color = Crop.HydratedColor;
            } else {
                //Debug.Log("sprite no hydrated");
                hydrationSR.color = Color.clear;
            }
        }


        private static void HarvestIfPossible(GameObject gameObject) {
            Crop crop = ((FarmlandTile)gameObject.GetComponent<TileBehaviour>().Tile).Crop;
            if(crop.FullyGrown) {
                Inventory.instance.AddElement(ItemContainer.Instance.GetItemByName("Wheat Seeds"),
                    (int)(Random.Range(1, 300)));
                Inventory.instance.AddElement(ItemContainer.Instance.GetItemByName("Wheat"), 1);
                crop.ResetPlant();
                UpdateFarmlandSprites(gameObject);
            }
        }

        private static void SetTile(GameObject gameObject, BaseTile tileType) {
            gameObject.GetComponent<TileBehaviour>().Tile = tileType;
        }

        private static void SetFence(GameObject gameObject) {
            SpriteRenderer fenceRenderer = null;
            BoxCollider2D fenceCollider = null;
            foreach(Transform transChild in gameObject.GetComponentsInChildren<Transform>()) {
                if(transChild.gameObject.name.Equals("Fence")) {
                    fenceRenderer = transChild.gameObject.GetComponent<SpriteRenderer>();
                    fenceCollider = transChild.gameObject.GetComponent<BoxCollider2D>();
                }
            }

            if(fenceRenderer && fenceCollider) {
                if(!fenceRenderer.enabled || !fenceCollider.enabled) {
                    fenceRenderer.enabled = true;
                    fenceRenderer.color = new Color(1, 1, 1, 1);
                    fenceCollider.enabled = true;
                    Inventory.instance.RemoveElement(ItemContainer.Instance.GetItemByName("Fence"), 1);
                }
            } else {
                Debug.LogError("Fence Renderer or Fence Collider is null");
            }
        }
    }
}