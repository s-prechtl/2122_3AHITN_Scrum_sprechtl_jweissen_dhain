using Tiles;

namespace Items.TerraformingTools
{
    public class WaterTool : TerraformingTool
    {
        public WaterTool(int id) : 
            base(typeof(WaterTile), "Water Tool", "Sets clicked Tile to water", id) { }
    }
}