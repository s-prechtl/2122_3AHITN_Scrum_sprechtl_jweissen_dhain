using System;
using System.Collections;
using System.Collections.Generic;
using Items.TerraformingTools;
using Tiles;
using UnityEngine;

public class TileBehaviour : MonoBehaviour {
    private BaseTile _tile;

    // Start is called before the first frame update
    private void Start() {
        Debug.Log("Created");
        SetTile(new GrassTile());

        HouseController.NewDayEvent.AddListener(_tile.DayLightStep);
    }

    private void OnMouseDown() {
        Debug.Log("Clicked");

        UsableItem usable = PlayerController.instance.GetSelectedItem();
        BaseTile tileToSetTo = null;
        if(usable.GetType() == typeof(TerraformingTool)) {
            TerraformingTool terraformingTool = (TerraformingTool)usable;
            Type tileTypeToSetTo = terraformingTool.TileType;
            tileToSetTo = (BaseTile)Activator.CreateInstance(tileTypeToSetTo);
        } else {
            tileToSetTo = _tile.Clicked(usable);
            Debug.Log("AMOGUS " + tileToSetTo.ToString());
        }

        if(tileToSetTo != null) {
            SetTile(tileToSetTo);
        }
    }

    private void SetTile(BaseTile tileToSet) {
        Debug.Log("Set tile to " + tileToSet.ToString());
        _tile = tileToSet;
        GetComponent<SpriteRenderer>().color = _tile.getColor; // TODO: Change to Sprite 
    }
}