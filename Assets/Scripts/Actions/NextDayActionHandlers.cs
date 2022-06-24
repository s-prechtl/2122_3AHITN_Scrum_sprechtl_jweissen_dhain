using Assets.Scripts.Actions;
using Tiles;
using UnityEngine;

/// Definitions of NextDayActionHandlers here
namespace Actions {
    public abstract class AbstractTileNextDayActionHandler : NextDayActionHandler {
        protected BaseTile _tile;
        protected ItemContainer _ic;
        public virtual void InvokeAction(GameObject gameObject) {
            throw new System.NotImplementedException();
        }

        public virtual bool Matches(GameObject gameObject) {
            bool rv = false;
            _ic = ItemContainer.Instance;
            try {
                _tile = gameObject.GetComponent<TileBehaviour>().Tile;
                rv = true;
            }
            catch { }
            
            return rv;
        }
    }
    
    public abstract class AbstractFarmlandTileNextDayActionHandler : AbstractTileNextDayActionHandler {
        protected Crop crop;
        public override bool Matches(GameObject gameObject) {
            bool rv = base.Matches(gameObject);
            try {
                crop = ((FarmlandTile)_tile).Crop;
            }
            catch {
                rv = false;
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
    
    public class FarmlandTileNextDayActionHandler : AbstractFarmlandTileNextDayActionHandler {
        public override void InvokeAction(GameObject gameObject) {
            if(crop.Planted && crop.Hydrated) {
                crop.Grow();
            }
            crop.Hydrated = false;

            updateFarmlandSprites(gameObject);
        }
        
        public override bool Matches(GameObject gameObject) {
            bool rv = base.Matches(gameObject);
            Debug.Log(_tile.ToString());
            return rv;
        }
    }
}