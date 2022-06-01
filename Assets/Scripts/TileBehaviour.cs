using System;
using System.Collections;
using System.Collections.Generic;
using Items.TerraformingTools;
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
        
        UsableItem usable = PlayerController.instance.GetSelectedItem();
        BaseTile tileToSetTo = null;
        if (usable.GetType() == typeof(TerraformingTool))
        {
            TerraformingTool terraformingTool = (TerraformingTool) usable;
            Type tileTypeToSetTo = terraformingTool.TileType;
            tileToSetTo = (BaseTile) Activator.CreateInstance(tileTypeToSetTo);
        }
        else
        {
            tileToSetTo = tile.Clicked(usable);
            Debug.Log("AMOGUS " + tileToSetTo.ToString());
        }
        if (tileToSetTo != null)
        {
            SetTile(tileToSetTo);
        }
    }

    void SetTile(BaseTile tileToSet)
    {
        Debug.Log("Set tile to " + tileToSet.ToString());
        tile = tileToSet;
        GetComponent<SpriteRenderer>().color = tile.getColor; // TODO: Change to Sprite 
    }
}
