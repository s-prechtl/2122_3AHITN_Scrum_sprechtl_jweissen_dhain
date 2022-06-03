using UnityEngine;

namespace Tiles
{
    public abstract class BaseTile
    {
        protected Color color;
        public Color getColor => color;
        // TODO: Change to Sprite, also in subclasses
         public Sprite Sprite;
        
        protected BaseTile(Sprite sprite)
        {
            this.Sprite = sprite;
        }

        protected void Start()
        {

        }

        protected void Update()
        {

        }

        public void DayLightStep()
        {
            
        }

        public virtual BaseTile Clicked(UsableItem usable)
        {
            Debug.Log(usable.ToString() + " used on " + this.ToString());
            return null;
        }
/*
        static protected Sprite GenerateSpriteFromFile()
        {
            
        }
        */
    }
}