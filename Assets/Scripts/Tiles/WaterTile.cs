using System.Collections;
using UnityEngine;

namespace Tiles {
    public class WaterTile : BaseTile {
        public WaterTile() : base("Assets/Farming Asset Pack/Split Assets/farming_tileset_023.png") {
        }

        public override BaseTile Clicked(UsableItem usable) {
            base.Clicked(usable);
            BaseTile rv = null;

            ItemContainer ic = ItemContainer.Instance;

            if (usable.ID == ic.GetItemIdByName("Fishing Rod")) {
                FishingController fc = FishingController.instance;
                if (!fc.Fishing) {
                    fc.StartFishing();
                } else {
                    fc.TryCatch();
                }
            } else if (usable.ID == ic.GetItemIdByName("Shovel")) {
                rv = new GrassTile();
            }

            return rv;
        }
    }
}