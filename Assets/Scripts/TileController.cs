using System;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {
    public GameObject tile;

    public GameObject cameraGameObject;

    public List<GameObject> Tiles;

    // Start is called before the first frame update
    void Start() {
        Camera camera = cameraGameObject.GetComponent<Camera>();
        Vector3 screen = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
        int x = Convert.ToInt32(Math.Ceiling(screen.x));
        int y = Convert.ToInt32(Math.Ceiling(screen.y));
        for(int xx = -x; xx <= x; xx++) {
            for(int yy = -y; yy <= y; yy++) {
                if(tile != null) {
                    Tiles.Add(Instantiate(tile, new Vector3(xx, yy, 0), Quaternion.identity));
                }
            }
        }
    }
}