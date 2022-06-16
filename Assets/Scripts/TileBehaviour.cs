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
        Tile = new GrassTile();

        foreach(Transform transChild in gameObject.GetComponentsInChildren<Transform>()) {
            if(transChild.gameObject.name.Equals("HoverIndicator")) {
                _hoverIndicatorSpriteRenderer = transChild.gameObject.GetComponent<SpriteRenderer>();
                _hoverIndicatorSpriteRenderer.color = Color.clear;
            }
        }
        _hoverIndicatorColor = new Color(1, 1, 1, 0.3f);
        SetHoverIndicatorVisibility(false);

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
