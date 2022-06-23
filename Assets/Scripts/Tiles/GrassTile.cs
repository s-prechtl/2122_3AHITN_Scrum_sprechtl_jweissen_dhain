using UnityEngine;

namespace Tiles
{
    public class GrassTile : BaseTile {
        private Sprite[] _sprites;
        public override Sprite Sprite
        {
            get
            {
                int rand = Random.Range(0, _sprites.Length);
                return _sprites[rand];
            }
        }
        public GrassTile() : base("Assets/Farming Asset Pack/Split Assets/farming_tileset_000.png") {
            _sprites = new[]
            {
                GenerateSpriteFromFile("Assets/Farming Asset Pack/Split Assets/farming_tileset_000.png"),
                GenerateSpriteFromFile("Assets/Farming Asset Pack/Split Assets/farming_tileset_001.png"),
                GenerateSpriteFromFile("Assets/Farming Asset Pack/Split Assets/farming_tileset_002.png")
            };
        }
    }
}