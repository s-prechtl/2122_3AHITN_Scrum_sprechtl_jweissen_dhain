using System.Security.Cryptography;
using Tiles;
using UnityEngine;
using static CropAction;


enum CropAction {
    NextDay,
    Hoe,
    Scythe,
    WateringCan,
    Seeds
}

public class Crop {
    private const int FinalGrowthStage = 4;

    private static Sprite _smallCrop;
    private static Sprite _fullyGrownCrop;

    private static Color _hydratedColor;

    private bool _planted;

    private bool _fullyGrown => (_growthStage >= FinalGrowthStage);

    private bool _hydrated;

    private int _growthStage;

    private SpriteRenderer _cropSpriteRenderer;
    private SpriteRenderer _hydrationSpriteRenderer;

    public Crop(SpriteRenderer cropSpriteRenderer, SpriteRenderer hydrationSpriteRenderer) {
        _planted = false;
        _hydrated = false;

        _cropSpriteRenderer = cropSpriteRenderer;
        _hydrationSpriteRenderer = hydrationSpriteRenderer;

        _smallCrop = BaseTile.GenerateSpriteFromFile("Assets/Farming Asset Pack/Split Assets/farming_tileset_026.png");
        _fullyGrownCrop = BaseTile.GenerateSpriteFromFile("Assets/Farming Asset Pack/Split Assets/farming_tileset_039.png");

        _hydratedColor = new Color(0.5f, 0.5f, 1.0f, 0.269420f);

        UpdateSprite();
    }

    private void Grow() {
        _growthStage++;
    }

    public void DayLightStep() {
        ApplyAction(NextDay);
    }

    public void Clicked(UsableItem usableItem) {
        if(usableItem != null) {
            ItemContainer ic = ItemContainer.Instance;
            if(ic.GetItemIdByName("Hoe") == usableItem.ID) {
                ApplyAction(Hoe);
            } else if(ic.GetItemIdByName("Scythe") == usableItem.ID) {
                ApplyAction(Scythe);
            } else if(ic.GetItemIdByName("Wheat Seeds") == usableItem.ID) {
                ApplyAction(Seeds);
            } else if(ic.GetItemIdByName("Watering Can") == usableItem.ID) {
                ApplyAction(WateringCan);
            }
        }
    }

    private void ApplyAction(CropAction action) {
        if(_planted) {
            if(Hoe == action) {
                _planted = false;
            } else if(Scythe == action) {
                if(_fullyGrown) {
                    Harvest();
                }
            }
        } else if(!_planted) {
            if(Seeds == action) {
                _planted = true;
                Inventory.instance.RemoveItem(ItemContainer.Instance.GetItemByName("Wheat Seeds"), 1);
            }
        }

        if(_hydrated) {
            if(NextDay == action) {
                _hydrated = false;
                if(_planted) {
                    Grow();
                }
            }
        } else if(!_hydrated) {
            if(WateringCan == action) {
                _hydrated = true;
            }
        }

        UpdateSprite();
    }

    private void Harvest() {
        AddCropToInventory();
        _planted = false;
        _growthStage = 0;
    }

    private void AddCropToInventory() {
        Inventory.instance.AddItem(ItemContainer.Instance.GetItemByName("Wheat Seeds"), (int)(Random.Range(1,300)));
        Inventory.instance.AddItem(ItemContainer.Instance.GetItemByName("Wheat"), 1);
    }

    private void UpdateSprite() {
        if(_planted) {
            if(_fullyGrown) {
                //Debug.Log("sprite fg");
                _cropSpriteRenderer.sprite = _fullyGrownCrop;
            } else {
                //Debug.Log("sprite smallCrop");
                _cropSpriteRenderer.sprite = _smallCrop;
            }
        } else {
            _cropSpriteRenderer.sprite = null;
        }

        if(_hydrated) {
            //Debug.Log("sprite hydrated");
            _hydrationSpriteRenderer.color = _hydratedColor;
        } else {
            //Debug.Log("sprite no hydrated");
            _hydrationSpriteRenderer.color = Color.clear;
        }
    }

    private void Dump() {
        Debug.Log("age: " + _growthStage + "\n" +
                  "hydrated: " + _hydrated + "\n" +
                  "planted: " + _planted);
    }
}