using System;
using Tiles;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    private BaseTile _tile;
    private SpriteRenderer _hoverIndicatorSpriteRenderer;
    private static Color _hoverIndicatorColor;

    // Start is called before the first frame update
    void Start()
    {
        _hoverIndicatorSpriteRenderer = gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>();
        SetHoverIndicatorVisibility(false);
        SetTile(new GrassTile(gameObject));
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.clear;
        _hoverIndicatorColor = new Color(1, 1, 1, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnMouseDown()
    {
        UsableItem usable = null;
        BaseTile tileToSetTo = null;

        if (PlayerController.instance.GetSelectedItem() != null)
        {
            usable = PlayerController.instance.GetSelectedItem();
        }

        tileToSetTo = _tile.Clicked(usable);

        if (tileToSetTo != null)
        {
            SetTile(tileToSetTo);
        }
    }

    void SetTile(BaseTile tileToSet)
    {
        _tile = tileToSet;
        GetComponent<SpriteRenderer>().sprite = _tile.Sprite;
    }

    private void OnMouseEnter()
    {
        SetHoverIndicatorVisibility(true);
    }

    private void OnMouseExit()
    {
        SetHoverIndicatorVisibility(false);
    }

    private void SetHoverIndicatorVisibility(bool visible)
    {
        if (visible)
        {
            _hoverIndicatorSpriteRenderer.color = _hoverIndicatorColor;
        }
        else
        {
            _hoverIndicatorSpriteRenderer.color = Color.clear;
        }
    }
}
