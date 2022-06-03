using Tiles;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    private BaseTile _tile;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Created");
        SetTile(new GrassTile());
        
        HouseController.NewDayEvent.AddListener(_tile.DayLightStep);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnMouseDown()
    {
        Debug.Log("Clicked");

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
        Debug.Log("Set tile to " + tileToSet.ToString());
        _tile = tileToSet;
        Debug.Log(_tile.Sprite);
        GetComponent<SpriteRenderer>().sprite = _tile.Sprite; // TODO: Change to Sprite 
    }
}
