using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] Tilemap layoutTileMap;
    [SerializeField] Tile tile;

    void Start()
    {
        Vector3Int tilePos = Vector3Int.zero;

        for (int i = 0; i < 10; i++)
        {
            layoutTileMap.SetTile(tilePos, tile);
            tilePos.x += 1;
        }
    }
}
