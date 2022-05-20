using UnityEngine;

namespace Tiles
{
    public class FarmlandTile : BaseTile
    {
        private Crop _crop;
        private bool _hydrated;
        
        public FarmlandTile() : base(Color.black)
        {
            _crop = null;
            _hydrated = false;
        }

        public new void DayLightStep()
        {
            if (_crop != null)
            {
                _crop.DayLightStep(_hydrated);
                if (_crop.MarkedForDeletion)
                {
                    Debug.Log("Farmland crop deleted");
                    _crop = null;
                }
            }
        }

        public new BaseTile Clicked(UsableItem usable)
        {
            base.Clicked(usable);

            ItemContainer ic = ItemContainer.Instance;

            if (usable.id == ic.GetItemIdByName("Hoe"))
            {
                Debug.Log("Farmland hydrated");
                _hydrated = true;
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