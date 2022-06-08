using System.Collections;
using UnityEngine;

namespace Tiles {
    public class WaterTile : BaseTile {
        public WaterTile(GameObject gameObject) : base("Assets/Farming Asset Pack/Split Assets/farming_tileset_023.png", gameObject) {
        }

        public override BaseTile Clicked(UsableItem usable) {
            base.Clicked(usable);
            BaseTile rv = null;

            ItemContainer ic = ItemContainer.Instance;

            if (usable.Id == ic.GetItemIdByName("Fishing Rod")) {
                FishingController fc = FishingController.instance;
                if (!fc.Fishing) {
                    fc.StartFishing();
                } else {
                    fc.TryCatch();
                }
            } else if (usable.Id == ic.GetItemIdByName("Shovel")) {
                rv = new GrassTile(_gameObject);
            }

            return rv;
        }
    }
}