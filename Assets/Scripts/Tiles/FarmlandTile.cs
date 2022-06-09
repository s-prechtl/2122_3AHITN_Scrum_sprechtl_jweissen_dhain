using UnityEngine;

namespace Tiles {
    public class FarmlandTile : BaseTile {
        private Crop _crop;
        public Crop Crop => _crop;
        
        public FarmlandTile() : base("Assets/Farming Asset Pack/Split Assets/farming_tileset_008.png") {
            _crop = new Crop();
        }
        
    }
}