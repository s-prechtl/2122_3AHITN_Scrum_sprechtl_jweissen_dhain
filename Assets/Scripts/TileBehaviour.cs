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
        
        HouseController.NewDayEvent.AddListener(tile.DayLightStep);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnMouseDown()
    {
        Debug.Log("Clicked");
        
        // SelectedItem always null for now 
        //BaseTile temp = tile.Clicked(PlayerController.getInstance().SelectedItem);
        //if (temp != null)
        //{
        //    SetTile(temp);
        //}
    }

    void SetTile(BaseTile tileToSet)
    {
        tile = tileToSet;
        GetComponent<SpriteRenderer>().color = tile.getColor; // TODO: Change to Sprite 
    }
}
