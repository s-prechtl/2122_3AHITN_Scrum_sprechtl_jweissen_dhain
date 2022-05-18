using System;
using System.Collections;
using System.Collections.Generic;
using Tiles;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    private BaseTile tile;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Created");
        SetTile(new GrassTile());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnMouseDown()
    {
        Debug.Log("Clicked");
        tile.Clicked(/* Current tool */ null);
    }

    void SetTile(BaseTile tileToSet)
    {
        tile = tileToSet;
        GetComponent<SpriteRenderer>().color = tile.getColor;
    }
}
