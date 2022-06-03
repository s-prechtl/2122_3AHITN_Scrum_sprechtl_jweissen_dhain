using UnityEngine;

namespace Tiles
{
    public class FarmlandTile : BaseTile
    {
        private Crop _crop;
        
        public FarmlandTile() : base("Assets/Farming Asset Pack/Split Assets/farming_tileset_008.png")
        {
            _crop = null;
        }

        public new void DayLightStep()
        {
        }

        public new BaseTile Clicked(UsableItem usable)
        {
            base.Clicked(usable);

            ItemContainer ic = ItemContainer.Instance;

            if (usable.id == ic.GetItemIdByName("Hoe"))
            {
                Debug.Log("Farmland hydrated");
                //_hydrated = true;
            }
            
            if (usable.id == ic.GetItemIdByName("Wheat Seed") && _crop == null)
            {
                Plant();
            }

            if (usable.id == ic.GetItemIdByName("Scythe") && _crop != null && _crop.FullyGrown)  
            {
                Harvest();
            }

            return null;
        }

        private void Harvest()
        {
            Debug.Log("Farmland harvested");
            // add wheat to inventory
            _crop = null;
        }

        private void Plant()
        {
            Debug.Log("Farmland planted");
            // wheatSeeds-- in inventory
            _crop = new Crop();
        }
    }
}