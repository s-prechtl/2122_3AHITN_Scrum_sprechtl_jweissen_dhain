using System;

namespace Items.TerraformingTools
{
    public abstract class TerraformingTool : UsableItem
    {
        public readonly Type TileType;

        protected TerraformingTool(Type tileType, string displayName, string description, int id) : 
            base(displayName, description, id)
        {
            this.TileType = tileType;
        }
        
        
    }
}