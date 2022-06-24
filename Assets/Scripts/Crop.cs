using System;
using System.Security.Cryptography;
using Tiles;
using UnityEngine;
using Random = UnityEngine.Random;


/// <summary>
/// Controlling logic of crop on FarmlandTiles
/// </summary>
public class Crop {
    private const int FinalGrowthStage = 4;

    public static Sprite SmallCrop = BaseTile.GenerateSpriteFromFile("Assets/Farming Asset Pack/Split Assets/farming_tileset_026.png");
    public static Sprite FullyGrownCrop = BaseTile.GenerateSpriteFromFile("Assets/Farming Asset Pack/Split Assets/farming_tileset_039.png");

    public static Color HydratedColor = new Color(0.5f, 0.5f, 1.0f, 0.269420f);

    private bool _planted;
    public bool Planted => _planted;

    private int _growthStage;
    public bool FullyGrown => (_growthStage >= FinalGrowthStage);

    private bool _hydrated;
    public bool Hydrated
    {
        get => _hydrated;
        set => _hydrated = value;
    }
    
    
    public Crop() {
        ResetPlant();
        _hydrated = false;
    }

    public void Plant() {
        _planted = true;
    }
    
    public void Grow() {
        _growthStage++;
    }

    public void ResetPlant() {
        _planted = false;
        _growthStage = 0;
    }

    private void Dump() {
        Debug.Log("age: " + _growthStage + "\n" +
                  "hydrated: " + _hydrated + "\n" +
                  "planted: " + _planted);
    }
}