using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tiles
{
    public abstract class BaseTile
    {
        protected Color color;
        public Color getColor => color;
        // Later to be replaced with
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

        }

        public void Clicked(UsableItem usable)
        {
            Debug.Log(usable.ToString() + " used on " + this.ToString());
        }

        
    }
}