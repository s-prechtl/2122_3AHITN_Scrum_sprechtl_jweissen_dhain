using System;
using Actions;
using DefaultNamespace;
using Tiles;
using UnityEngine;

/// <summary>
/// Defining the behaviour of tiles
/// </summary>
public class TileBehaviour : MonoBehaviour {
    private BaseTile _tile;
    public virtual BaseTile Tile {
        get => _tile;
        set {
            _tile = value;
            GetComponent<SpriteRenderer>().sprite = _tile.Sprite;
        }
    }

    private SpriteRenderer _hoverIndicatorSpriteRenderer;
    private static Color _hoverIndicatorColor;

    /// Start is called before the first frame update
    void Start() {
        Tile = new GrassTile();

        foreach(Transform transChild in gameObject.GetComponentsInChildren<Transform>()) {
            if(transChild.gameObject.name.Equals("HoverIndicator")) {
                _hoverIndicatorSpriteRenderer = transChild.gameObject.GetComponent<SpriteRenderer>();
                _hoverIndicatorSpriteRenderer.color = Color.clear;
            }
            if(transChild.gameObject.name.Equals("HydrationIndicator")) {
                transChild.gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
            }
        }
        _hoverIndicatorColor = new Color(1, 1, 1, 0.3f);
        SetHoverIndicatorVisibility(false);

        HouseController.NewDayEvent.AddListener(NextDay);
    }

    /// <summary>
    /// is called when a NewDayEvent is dispersed
    /// </summary>
    private void NextDay() {
        ActionManager.Instance.NextDayAction(gameObject);
    }

    /// <summary>
    /// Used to invoke Click Actions
    /// </summary>
    void OnMouseDown() {
        UsableItem usableItem = PlayerController.instance.SelectedItem;
        if(usableItem != null) {
            ActionManager.Instance.ClickAction(gameObject, usableItem);
        }
    }

    /// <summary>
    /// used to set hover indicator
    /// </summary>
    private void OnMouseEnter() {
        SetHoverIndicatorVisibility(true);
    }

    private void OnMouseExit() {
        SetHoverIndicatorVisibility(false);
    }

    /// <summary>
    /// Sets the visibility of hover indicator
    /// </summary>
    /// <param name="visible"></param>
    private void SetHoverIndicatorVisibility(bool visible) {
        if(visible) {
            _hoverIndicatorSpriteRenderer.color = _hoverIndicatorColor;
        } else {
            _hoverIndicatorSpriteRenderer.color = Color.clear;
        }
    }
}
