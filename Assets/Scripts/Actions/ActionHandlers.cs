using Actions;
using Tiles;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Assets.Scripts.Actions {
    public abstract class AbstractTileActionHandler : ActionHandler {
        protected BaseTile _tile;
        protected int _usableItemId;
        protected ItemContainer _ic;
        public virtual void InvokeAction(GameObject gameObject, UsableItem usableItem) {
            throw new System.NotImplementedException();
        }

        public virtual bool Matches(GameObject gameObject, UsableItem usableItem) {
            bool rv = false;
            _ic = ItemContainer.Instance;
            try {
                _tile = gameObject.GetComponent<TileBehaviour>().Tile;
                rv = true;
            }
            catch {
            }
            return rv;
        }
    }
    public abstract class AbstractGrassTileActionHandler : AbstractTileActionHandler {
        public override bool Matches(GameObject gameObject, UsableItem usableItem) {
            bool rv = base.Matches(gameObject, usableItem);
            if(rv) {
                rv = (_tile.GetType() == typeof(GrassTile));
            }
            return rv;
        }
    }
    public abstract class AbstractFarmlandTileActionHandler : AbstractTileActionHandler {
        protected Crop crop;
        public override bool Matches(GameObject gameObject, UsableItem usableItem) {
            bool rv = base.Matches(gameObject, usableItem);
            if(rv) {
                crop = ((FarmlandTile)gameObject.GetComponent<TileBehaviour>().Tile).Crop;
                rv = (_tile.GetType() == typeof(FarmlandTile));
            }
            return rv;
        }

        protected void updateFarmlandSprites(GameObject gameObject) {
            SpriteRenderer hydrationSpriteRenderer = null;
            SpriteRenderer cropSpriteRenderer = null;

            foreach (Transform transChild in gameObject.transform.GetComponentInChildren<Transform>()) {
                if(transChild.gameObject.name.Equals("HydrationIndicator")) {
                    hydrationSpriteRenderer = transChild.gameObject.GetComponent<SpriteRenderer>();
                }
                if(transChild.gameObject.name.Equals("Crop")) {
                    cropSpriteRenderer = transChild.gameObject.GetComponent<SpriteRenderer>();
                }
            }

            if(crop.Planted) {
                if(crop.FullyGrown) {
                    //Debug.Log("sprite fg");
                    cropSpriteRenderer.sprite = Crop.FullyGrownCrop;
                } else {
                    //Debug.Log("sprite smallCrop");
                    cropSpriteRenderer.sprite = Crop.SmallCrop;
                }
            } else {
                cropSpriteRenderer.sprite = null;
            }

            if(crop.Hydrated) {
                //Debug.Log("sprite hydrated");
                hydrationSpriteRenderer.color = Crop.HydratedColor;
            } else {
                //Debug.Log("sprite no hydrated");
                hydrationSpriteRenderer.color = Color.clear;
            }
        }
    }
    public abstract class AbstractWaterTileActionHandler : AbstractTileActionHandler {
        public override bool Matches(GameObject gameObject, UsableItem usableItem) {
            bool rv = base.Matches(gameObject, usableItem);
            if(rv) {
                rv = (_tile.GetType() == typeof(WaterTile));
            }
            return rv;
        }
    }

    public abstract class AbstractAnimalActionHandler : ActionHandler {
        public virtual void InvokeAction(GameObject gameObject, UsableItem usableItem) {
            throw new System.NotImplementedException();
        }

        public virtual bool Matches(GameObject gameObject, UsableItem usableItem) {
            bool rv = false;
            try {
                gameObject.GetComponent<Animal>();
                rv = true;
            }
            catch {
            }
            return rv;
        }
    }

    public class GrassTileHoeActionHandler : AbstractGrassTileActionHandler {
        public override void InvokeAction(GameObject gameObject, UsableItem usableItem) {
            gameObject.GetComponent<TileBehaviour>().Tile = new FarmlandTile();
        }

        public override bool Matches(GameObject gameObject, UsableItem usableItem) {
            bool rv = base.Matches(gameObject, usableItem);
            if(rv) {
                rv = (usableItem.ID == _ic.GetItemIdByName("Hoe"));
            }
            return rv;
        }
    }

    public class GrassTileShovelActionHandler : AbstractGrassTileActionHandler {
        public override void InvokeAction(GameObject gameObject, UsableItem usableItem) {
            gameObject.GetComponent<TileBehaviour>().Tile = new WaterTile();
        }

        public override bool Matches(GameObject gameObject, UsableItem usableItem) {
            bool rv = base.Matches(gameObject, usableItem);
            if(rv) {
                rv = (usableItem.ID == _ic.GetItemIdByName("Shovel"));
            }
            return rv;
        }
    }

    public class GrassTileFenceActionHandler : AbstractGrassTileActionHandler {
        public override void InvokeAction(GameObject gameObject, UsableItem usableItem) {
            SpriteRenderer fenceRenderer = null;
            BoxCollider2D fenceCollider = null;
            foreach (Transform transChild in gameObject.GetComponentsInChildren<Transform>()) {
                if(transChild.gameObject.name.Equals("Fence")) {
                    fenceRenderer = transChild.gameObject.GetComponent<SpriteRenderer>();
                    fenceCollider = transChild.gameObject.GetComponent<BoxCollider2D>();
                }
            }

            if(fenceRenderer && fenceCollider) {
                if(!fenceRenderer.enabled || !fenceCollider.enabled) {
                    fenceRenderer.enabled = true;
                    Debug.Log("aaaaaaaaaaaaaaaaaaaaa");
                    fenceRenderer.color = new Color(1, 1, 1, 1);
                    fenceCollider.enabled = true;
                    Inventory.instance.RemoveElement(ItemContainer.Instance.GetItemByName("Fence"), 1);
                }
            } else {
                Debug.LogError("Fence Renderer or Fence Collider is null");
            }
        }

        public override bool Matches(GameObject gameObject, UsableItem usableItem) {
            bool rv = base.Matches(gameObject, usableItem);
            if(rv) {
                rv = (usableItem.ID == _ic.GetItemIdByName("Fence"));
            }
            return rv;
        }
    }

    public class FarmlandTileShovelActionHandler : AbstractFarmlandTileActionHandler {
        public override void InvokeAction(GameObject gameObject, UsableItem usableItem) {
            gameObject.GetComponent<TileBehaviour>().Tile = new GrassTile();
        }

        public override bool Matches(GameObject gameObject, UsableItem usableItem) {
            bool rv = base.Matches(gameObject, usableItem);
            if(rv) {
                rv = (usableItem.ID == _ic.GetItemIdByName("Shovel"));
            }
            return rv;
        }
    }

    public class FarmlandTileScytheActionHandler : AbstractFarmlandTileActionHandler {
        public override void InvokeAction(GameObject gameObject, UsableItem usableItem) {
            if(crop.FullyGrown) {
                Inventory.instance.AddElement(ItemContainer.Instance.GetItemByName("Wheat Seeds"),
                    (int)(Random.Range(1, 300)));
                Inventory.instance.AddElement(ItemContainer.Instance.GetItemByName("Wheat"), 1);
                crop.ResetPlant();
                updateFarmlandSprites(gameObject);
            }
        }

        public override bool Matches(GameObject gameObject, UsableItem usableItem) {
            bool rv = base.Matches(gameObject, usableItem);
            if(rv) {
                rv = (usableItem.ID == _ic.GetItemIdByName("Scythe"));
            }
            return rv;
        }
    }
    
    public class FarmlandTileWateringCanActionHandler : AbstractFarmlandTileActionHandler {
        public override void InvokeAction(GameObject gameObject, UsableItem usableItem) {
            crop.Hydrated = true;
            updateFarmlandSprites(gameObject);
        }

        public override bool Matches(GameObject gameObject, UsableItem usableItem) {
            bool rv = base.Matches(gameObject, usableItem);
            if(rv) {
                rv = (usableItem.ID == _ic.GetItemIdByName("Watering Can"));
            }
            return rv;
        }
    }
    
    public class FarmlandTileWheatSeedsActionHandler : AbstractFarmlandTileActionHandler {
        public override void InvokeAction(GameObject gameObject, UsableItem usableItem) {
            if(!crop.Planted) {
                crop.Plant();
                Inventory.instance.RemoveElement(ItemContainer.Instance.GetItemByName("Wheat Seeds"), 1);
            }

            updateFarmlandSprites(gameObject);
        }

        public override bool Matches(GameObject gameObject, UsableItem usableItem) {
            bool rv = base.Matches(gameObject, usableItem);
            if(rv) {
                rv = (usableItem.ID == _ic.GetItemIdByName("Wheat Seeds"));
            }
            return rv;
        }
    }
    
    public class WaterTileShovelActionHandler : AbstractWaterTileActionHandler {
        public override void InvokeAction(GameObject gameObject, UsableItem usableItem) {
            gameObject.GetComponent<TileBehaviour>().Tile = new GrassTile();
        }

        public override bool Matches(GameObject gameObject, UsableItem usableItem) {
            bool rv = base.Matches(gameObject, usableItem);
            if(rv) {
                rv = (usableItem.ID == _ic.GetItemIdByName("Shovel"));
            }
            return rv;
        }
    }
    
    public class WaterTileFishingRodActionHandler : AbstractWaterTileActionHandler {
        public override void InvokeAction(GameObject gameObject, UsableItem usableItem) {
            ItemContainer ic = ItemContainer.Instance;

            FishingController fc = FishingController.instance;
            if(!fc.Fishing) {
                fc.StartFishing();
            } else {
                fc.TryCatch();
            }
        }

        public override bool Matches(GameObject gameObject, UsableItem usableItem) {
            bool rv = base.Matches(gameObject, usableItem);
            if(rv) {
                rv = (usableItem.ID == _ic.GetItemIdByName("Fishing Rod"));
            }
            return rv;
        }
    }
}