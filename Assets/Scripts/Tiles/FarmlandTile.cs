using UnityEngine;

namespace Tiles
{
    public class FarmlandTile : BaseTile
    {
        private Crop _crop;
        
        public FarmlandTile(GameObject gameObject) : base("Assets/Farming Asset Pack/Split Assets/farming_tileset_008.png", gameObject)
        {
            _crop = new Crop(gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>(),
                             gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>());
        }   

        public override void DayLightStep()
        {
            _crop.DayLightStep();
        }

        public override BaseTile Clicked(UsableItem usable)
        {
            base.Clicked(usable);
            _crop.Clicked(usable);
            
            return null;
        }
    }
}