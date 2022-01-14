using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapTester : MonoBehaviour
{

    [SerializeField] private LevelSkeletGenerator levelSkeletGenerator;
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private TileBase[] tiles;
    [SerializeField] private Vector3Int startPosition;

     public void BuildMap()
    {
        int width = levelSkeletGenerator.levelGrid.GetLength(0);
        int height = levelSkeletGenerator.levelGrid.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                int tileIndx = 0;
                for(int z = 0; z <= 3; z++)
                {
                    //Debug.Log("Scan levelGrid" + "[" + x + "," + y + "]");
                    if (levelSkeletGenerator.levelGrid[x, y].hasDoor[z]) tileIndx += Mathf.FloorToInt(Mathf.Pow(2, z));
                }
                tileMap.SetTile(startPosition + new Vector3Int(x, y, 0), tiles[tileIndx]);
            }
        }
    }
}
