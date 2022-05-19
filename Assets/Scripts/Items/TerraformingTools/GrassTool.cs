using Tiles;

namespace Items.TerraformingTools
{
    public class GrassTool : TerraformingTool
    {
        public GrassTool(int id) : 
            base(typeof(GrassTile), "Grass Tool", "Sets clicked Tile to Grass", id) { }
    }
}