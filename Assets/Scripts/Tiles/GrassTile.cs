using UnityEngine;

namespace Tiles
{
    public class GrassTile : BaseTile
    {
        public GrassTile() : base("Assets/Farming Asset Pack/Split Assets/farming_tileset_000.png")
        {

        }
        
        /// <summary>
        /// to be invoked when the Tile is clicked, handles the actions following on the click
        /// </summary>
        /// <param name="usable">the UsableItem that the Tile was clicked on with</param>
        /// <returns>a subclass of BaseTile if the Tile has to change, null if it stays the same type</returns>
        public override BaseTile Clicked(UsableItem usable) {
            BaseTile rv = null;
            ItemContainer ic = ItemContainer.Instance;
            if (usable != null)
            {
                base.Clicked(usable);
                if (usable.ID == ic.GetItemIdByName("Hoe")) {
                    rv = new FarmlandTile();
                } else if (usable.ID == ic.GetItemIdByName("Shovel")) {
                    rv = new WaterTile();
                }
            }
            return rv;
        }
    }
}