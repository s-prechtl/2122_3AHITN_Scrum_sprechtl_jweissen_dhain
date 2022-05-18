using UnityEngine;

namespace Tiles
{
    public class GrassTile : BaseTile
    {
        public GrassTile() : base(Color.green)
        {
            
        }
        
        /// <summary>
        /// to be invoked when the Tile is clicked, handles the actions following on the click
        /// </summary>
        /// <param name="usable">the UsableItem that the Tile was clicked on with</param>
        /// <returns>a subclass of BaseTile if the Tile has to change, null if it stays the same type</returns>
        new public BaseTile Clicked(UsableItem usable) {
            base.Clicked(usable);
            BaseTile rv = null;
            if (usable.GetType() == typeof(Items.Hoe))
            {
                rv = new FarmlandTile();
            }
            return rv;
        }
    }
}