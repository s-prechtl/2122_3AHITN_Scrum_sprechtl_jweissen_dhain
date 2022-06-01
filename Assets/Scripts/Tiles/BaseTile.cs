using UnityEngine;

namespace Tiles
{
    public abstract class BaseTile
    {
        protected Color color;
        public Color getColor => color;
        // TODO: Change to Sprite, also in subclasses
        // public Sprite sprite;
        
        protected BaseTile(Color color)
        {
            this.color = color;
        }

        protected void Start()
        {

        }

        protected void Update()
        {

        }

        public void DayLightStep()
        {
            Debug.Log("I evolve");
        }

        public virtual BaseTile Clicked(UsableItem usable)
        {
            Debug.Log(usable.ToString() + " used on " + this.ToString());
            return null;
        }
    }
}