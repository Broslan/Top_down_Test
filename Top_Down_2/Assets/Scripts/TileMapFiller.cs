using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapFiller : MonoBehaviour
{
    [SerializeField] private Tilemap[] tileMaps;
    [SerializeField] private int width = 106, height = 105;
    [SerializeField] private TileMapMemory[] tileMemory;


    // Start is called before the first frame update
    void Awake()
    {
        WrihtTileMap(tileMaps[1], tileMaps[0], tileMemory[0].tilemapUnserializable, Vector3Int.zero);
        WrihtTileMap(tileMaps[1], tileMaps[0], tileMemory[0].tilemapUnserializable, new Vector3Int(0,height,0));
        WrihtTileMap(tileMaps[1], tileMaps[0], tileMemory[0].tilemapUnserializable, new Vector3Int(width, 0, 0));
        WriteLevel(tileMaps[0], tileMemory[1].tilemapUnserializable, Vector3Int.zero);
        WriteLevel(tileMaps[0], tileMemory[1].tilemapUnserializable, new Vector3Int(width, 0, 0));
    }


    //hardcoded RoomWall building function
    private void WrihtTileMap(Tilemap tileMapWall,Tilemap tileMapGround, TileBase[,] tileBase, Vector3Int originPosition)
    {
        int width = tileBase.GetLength(0);
        int height = tileBase.GetLength(1);

        //Wright down wall
        for (int y = 0; y <= 2; y++)
        {
            for (int x = 0; x <= width - 1; x++)
            {
                if (x<51 || x>54)
                {
                    tileMapWall.SetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z), tileBase[x, y]);
                }
                else
                {
                    tileMapGround.SetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z), tileBase[x, y]);
                }
            }
        }

        //Wright up wall
        for (int y = height - 1; y >= height - 1 - 2; y--)
        {
            for (int x = 0; x <= width - 1; x++)
            {
                if (x < 51 || x > 54)
                {
                    tileMapWall.SetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z), tileBase[x, y]);
                }
                else
                {
                    tileMapGround.SetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z), tileBase[x, y]);
                }
            }
        }

        //Wright left wall
        for (int x = 0; x <= 3; x++)
        {
            for (int y = 3; y <= height - 1 - 3; y++)
            {
                if (y < 50 || y > 53)
                {
                    tileMapWall.SetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z), tileBase[x, y]);
                }
                else
                {
                    tileMapGround.SetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z), tileBase[x, y]);
                }
            }
        }

        //Wright right wall
        for (int x = width - 1; x >= width - 1 - 2; x--)
        {
            for (int y = 3; y <= height - 1 - 3; y++)
            {
                if (y < 50 || y > 53)
                {
                    tileMapWall.SetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z), tileBase[x, y]);
                }
                else
                {
                    tileMapGround.SetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z), tileBase[x, y]);
                }
            }
        }
    }

    private void WriteLevel(Tilemap tileMap, TileBase[,] tileBase, Vector3Int originPosition)
    {
        originPosition = originPosition + new Vector3Int(3, 2, 0);
        for(int y = 0; y < 100; y++)
        {
            for(int x = 0; x < 100; x++)
            {
                tileMap.SetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z), tileBase[x, y]);
            }
        }
    }
}
