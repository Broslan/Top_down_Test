using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TileMapTetsting : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;
    private Tile tile;
    [SerializeField] private TileBase tileBase;
    [SerializeField] private Vector3Int tileVector;

    void Start()
    {
        for(int x = 0; x <= 12; x++)
        {
            tileMap.SetTile(new Vector3Int(x,0,0), tileBase);
        }
        for (int x = 0; x <= 12; x++)
        {
            tileMap.SetTile(new Vector3Int(x, 12, 0), tileBase);
        }
        for(int y = 1; y < 12; y++)
        {
            tileMap.SetTile(new Vector3Int(0, y, 0), tileBase);
        }
        for (int y = 1; y < 12; y++)
        {
            tileMap.SetTile(new Vector3Int(12, y, 0), tileBase);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
