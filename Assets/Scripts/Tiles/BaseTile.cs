using System;
using System.IO;
using UnityEngine;

namespace Tiles {
    public abstract class BaseTile {
        protected Sprite _sprite;
        public virtual Sprite Sprite => _sprite;

        protected BaseTile(String pathToImageFile) {
            _sprite = GenerateSpriteFromFile(pathToImageFile);
            HouseController.NewDayEvent.AddListener(DayLightStep);
        }

        public virtual void DayLightStep() { }

        static public Sprite GenerateSpriteFromFile(String pathToImageFile) {
            byte[] data = File.ReadAllBytes(pathToImageFile);
            Texture2D texture = new Texture2D(32, 32, TextureFormat.ARGB32, false);
            texture.LoadImage(data);
            Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height),
                new Vector2(0.5f, 0.5f), 32);
            return sprite;
        }
    }
}