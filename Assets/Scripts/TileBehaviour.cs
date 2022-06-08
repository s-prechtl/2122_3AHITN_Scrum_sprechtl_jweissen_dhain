using System;
using Tiles;
using UnityEngine;

public class TileBehaviour : MonoBehaviour {
    private BaseTile _tile;
    private SpriteRenderer _hoverIndicatorSpriteRenderer;

    // Start is called before the first frame update
    void Start() {
        //_hoverIndicatorSpriteRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        //SetHoverIndicatorVisibility(false);
        SetTile(new GrassTile());

        HouseController.NewDayEvent.AddListener(_tile.DayLightStep);
    }

    void OnMouseDown() {
        UsableItem usable = null;
        BaseTile tileToSetTo = null;

        if(PlayerController.instance.GetSelectedItem() != null) {
            usable = PlayerController.instance.GetSelectedItem();
        }

        tileToSetTo = _tile.Clicked(usable);

        if(tileToSetTo != null) {
            SetTile(tileToSetTo);
        }
    }

    void SetTile(BaseTile tileToSet) {
        _tile = tileToSet;
        GetComponent<SpriteRenderer>().sprite = _tile.Sprite;
    }

    private void OnMouseEnter()
    {
        //SetHoverIndicatorVisibility(true);
    }

    private void OnMouseExit()
    {
        //SetHoverIndicatorVisibility(false);
    }

    private void SetHoverIndicatorVisibility(bool visible)
    {
        _hoverIndicatorSpriteRenderer.enabled = visible;
    }
}