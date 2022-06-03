using Tiles;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    private BaseTile _tile;

    // Start is called before the first frame update
    void Start()
    {
        SetTile(new GrassTile());
        
        HouseController.NewDayEvent.AddListener(_tile.DayLightStep);
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
        Debug.Log(_tile.Sprite);
        GetComponent<SpriteRenderer>().sprite = _tile.Sprite;
    }
}
