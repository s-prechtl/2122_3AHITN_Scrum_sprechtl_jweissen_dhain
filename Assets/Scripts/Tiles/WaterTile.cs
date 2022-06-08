using System.Collections;
using UnityEngine;

namespace Tiles
{
    public class WaterTile : BaseTile {
        private double _fishingTime;
        private bool _fishing = false;
        private bool _catchable = false;
        private int _lastCall = 0;
        public WaterTile() : base("Assets/Farming Asset Pack/Split Assets/farming_tileset_023.png") {
        }

        public override BaseTile Clicked(UsableItem usable) {
            base.Clicked(usable);

            ItemContainer ic = ItemContainer.Instance;

            if (usable.id == ic.GetItemIdByName("Fishing Rod")) {
                if (!_fishing) {
                    _fishing = true;
                    _fishingTime = 0f;
                    Fish();
                } else if (_catchable) {
                    _fishing = false;
                    Debug.Log("Fished for" + _fishingTime/1000);
                }
}

            return null;
        }

        private void Fish() {
            if (_fishing) {
                if (_catchable) {
                    // _fishingTime += _lastCall - System.DateTime.Now; deltaTime between last recursive call
                }
                //Fish();
            }
            
        }
    }
}