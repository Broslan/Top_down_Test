using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TileMapSaveScript : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private Vector3Int originPosition;
    [SerializeField] private int width, height;
    [SerializeField] private TileMapMemory tileMapMemory;
    [SerializeField] private bool makeSave,makeWright;
    [SerializeField] private GameObject cube;


    private void Start()
    {
        tileMapMemory.Create(width, height);
        tileMapMemory.ReadTile(0, 0);
        SaveLevelTilemap(tileMap, tileMapMemory.tilemapUnserializable, originPosition,width, height);
        //SaveWallTilemap(tileMap, tileMapMemory.tilemapUnserializable, originPosition);
        EditorUtility.SetDirty(tileMapMemory);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        //Debug.Log(tileMapMemory.tileBases[0, 0]);
        //if (makeSave)
        //{
        //    tileMapMemory.Create(width, height);
        //    SaveWallTilemap(tileMap, tileMapMemory.tilemapUnserializable, originPosition);
        //    //SaveWallTilemapToTilemap(tileMap, tileMapMemory.tilemap, originPosition);
        //    EditorUtility.SetDirty(tileMapMemory);
        //    AssetDatabase.SaveAssets();
        //    AssetDatabase.Refresh();
        //}
        //if (makeWright)
        //{
        //    WrihtTileMap(tileMap, tileMapMemory.tilemapUnserializable, new Vector3Int(0, 0, 0));
        //    //WrightWallTilemapFromTilemap(tileMap, tileMapMemory.tilemap, new Vector3Int(20, 20, 0));
        //}
        
    }

    private void SaveWallTilemap(Tilemap tileMap, TileBase[,] tileBase, Vector3Int originPosition)
    {
        //save down wall
        for (int y = 0; y <= 2; y++)
        {
            for(int x = 0; x < width; x++)
            {
                tileBase[x, y] = tileMap.GetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z));
                //Instantiate(cube, tileMap.CellToWorld(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z)), Quaternion.identity);
            }
        }

        //save up wall
        for (int y = height-1; y >= height-1-2; y--)
        {
            for (int x = 0; x <= width-1; x++)
            {
                tileBase[x, y] = tileMap.GetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z));
                //Instantiate(cube, tileMap.CellToWorld(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z)), Quaternion.identity);
            }
        }

        //save left wall
        for (int x = 0; x <= 3; x++)
        {
            for (int y = 3; y < height - 3; y++)
            {
                tileBase[x, y] = tileMap.GetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z));
                //Instantiate(cube, tileMap.CellToWorld(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z)), Quaternion.identity);
            }
        }

        //save right wall
        for (int x = width-1; x >= width-1 - 2; x--)
        {
            for (int y = 3; y < height - 3; y++)
            {
                tileBase[x, y] = tileMap.GetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z));
                //Instantiate(cube, tileMap.CellToWorld(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z)), Quaternion.identity);
            }
        }

        //Debug.Log(tileMapMemory.tileBases[0, 0]);
    }


    private void WrihtTileMap(Tilemap tileMap, TileBase[,] tileBase, Vector3Int originPosition)
    {
        //Wright down wall
        for (int y = 0; y <= 2; y++)
        {
            for (int x = 0; x <= width-1; x++)
            {
                 tileMap.SetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z),tileBase[x, y]);
            }
        }

        //Wright up wall
        for (int y = height-1; y >= height-1 - 2; y--)
        {
            for (int x = 0; x <= width-1; x++)
            {
                tileMap.SetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z), tileBase[x, y]);
            }
        }

        //Wright left wall
        for (int x = 0; x <= 3; x++)
        {
            for (int y = 3; y <= height-1 - 3; y++)
            {
                tileMap.SetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z), tileBase[x, y]);
            }
        }

        //Wright right wall
        for (int x = width-1; x >= width-1 - 2; x--)
        {
            for (int y = 3; y <= height-1 - 3; y++)
            {
                tileMap.SetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z), tileBase[x, y]);
            }
        }
    }

    private void SaveLevelTilemap(Tilemap tileMap, TileBase[,] tileBase, Vector3Int originPosition, int width, int height)
    {
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x< width; x++)
            {
                tileBase[x, y] = tileMap.GetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z));
            }
        }
    }

    private void SaveWallTilemapToTilemap(Tilemap saveFrom, Tilemap saveTo, Vector3Int originPosition)
    {
        //save down wall
        for (int y = 0; y <= 2; y++)
        {
            for (int x = 0; x <= width - 1; x++)
            {
                saveTo.SetTile(new Vector3Int(x, y, 0), saveFrom.GetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z)));
                //Instantiate(cube, tileMap.CellToWorld(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z)), Quaternion.identity);
            }
        }

        //save up wall
        for (int y = height - 1; y >= height - 1 - 2; y--)
        {
            for (int x = 0; x <= width - 1; x++)
            {
                saveTo.SetTile(new Vector3Int(x, y, 0), saveFrom.GetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z)));
                //Instantiate(cube, tileMap.CellToWorld(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z)), Quaternion.identity);
            }
        }

        //save left wall
        for (int x = 0; x <= 3; x++)
        {
            for (int y = 3; y <= height - 1 - 3; y++)
            {
                saveTo.SetTile(new Vector3Int(x, y, 0), saveFrom.GetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z)));
                //Instantiate(cube, tileMap.CellToWorld(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z)), Quaternion.identity);
            }
        }

        //save right wall
        for (int x = width - 1; x >= width - 1 - 2; x--)
        {
            for (int y = 3; y <= height - 1 - 3; y++)
            {
                saveTo.SetTile(new Vector3Int(x, y, 0), saveFrom.GetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z)));
                //Instantiate(cube, tileMap.CellToWorld(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z)), Quaternion.identity);
            }
        }

        //Debug.Log(tileMapMemory.tileBases[0, 0]);
    }

    private void WrightWallTilemapFromTilemap(Tilemap tilemapTo, Tilemap tilemapFrom, Vector3Int originPosition)
    {
        //save down wall
        for (int y = 0; y <= 2; y++)
        {
            for (int x = 0; x <= width - 1; x++)
            {
                tilemapTo.SetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z), tilemapFrom.GetTile(new Vector3Int(x,y,0)));
                //Instantiate(cube, tileMap.CellToWorld(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z)), Quaternion.identity);
            }
        }

        //save up wall
        for (int y = height - 1; y >= height - 1 - 2; y--)
        {
            for (int x = 0; x <= width - 1; x++)
            {
                tilemapTo.SetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z), tilemapFrom.GetTile(new Vector3Int(x, y, 0)));
                //Instantiate(cube, tileMap.CellToWorld(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z)), Quaternion.identity);
            }
        }

        //save left wall
        for (int x = 0; x <= 3; x++)
        {
            for (int y = 3; y <= height - 1 - 3; y++)
            {
                tilemapTo.SetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z), tilemapFrom.GetTile(new Vector3Int(x, y, 0)));
                //Instantiate(cube, tileMap.CellToWorld(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z)), Quaternion.identity);
            }
        }

        //save right wall
        for (int x = width - 1; x >= width - 1 - 2; x--)
        {
            for (int y = 3; y <= height - 1 - 3; y++)
            {
                tilemapTo.SetTile(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z), tilemapFrom.GetTile(new Vector3Int(x, y, 0)));
                //Instantiate(cube, tileMap.CellToWorld(new Vector3Int(originPosition.x + x, originPosition.y + y, originPosition.z)), Quaternion.identity);
            }
        }

        //Debug.Log(tileMapMemory.tileBases[0, 0]);
    }
}
