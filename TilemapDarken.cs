using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TilemapDarken : MonoBehaviour
{
    Tilemap tileMap;
    // Start is called before the first frame update
    void Start()
    {
        tileMap = GetComponent<Tilemap>();

        /*
        TileBase t;
        for (int x = tilemap.origin.x; x < tilemap.origin.x + tilemap.size.x; x++)
        {
            for (int y = tilemap.origin.y; y < tilemap.origin.y + tilemap.size.y; y++)
            {
                tile
            }
        }*/


    }

    // Update is called once per frame
    void Update()
    {

    }
}
