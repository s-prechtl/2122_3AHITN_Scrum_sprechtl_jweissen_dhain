using System;
using DefaultNamespace;
using Tiles;
using UnityEngine;

public class TileBehaviour : MonoBehaviour {
    private BaseTile _tile;

    public BaseTile Tile {
        get => _tile;
        set {
            _tile = value;
            GetComponent<SpriteRenderer>().sprite = _tile.Sprite;
        }
    }

    private SpriteRenderer _hoverIndicatorSpriteRenderer;
    private static Color _hoverIndicatorColor;

    // Start is called before the first frame update
    void Start() {
        _hoverIndicatorSpriteRenderer = gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>();
        SetHoverIndicatorVisibility(false);
        Tile = new GrassTile();
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.clear;
        _hoverIndicatorColor = new Color(1, 1, 1, 0.3f);

        HouseController.NewDayEvent.AddListener(DayLightStep);
    }

    private void DayLightStep() {
        ActionInvoker.InvokeDayLightStep(gameObject);
    }

    void OnMouseDown() {
        UsableItem usableItem = PlayerController.instance.SelectedItem;
        if(usableItem != null) {
            ActionInvoker.InvokeAction(gameObject, usableItem);
        }
    }

    private void OnMouseEnter() {
        SetHoverIndicatorVisibility(true);
    }

    private void OnMouseExit() {
        SetHoverIndicatorVisibility(false);
    }

    private void SetHoverIndicatorVisibility(bool visible) {
        if(visible) {
            _hoverIndicatorSpriteRenderer.color = _hoverIndicatorColor;
        } else {
            _hoverIndicatorSpriteRenderer.color = Color.clear;
        }
    }
}
