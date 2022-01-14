using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildLevelWalls : MonoBehaviour
{
    [SerializeField] private TileMapMemory[] tileMapMemories;
    [SerializeField] private LevelSkeletGenerator levelBase;
    [SerializeField] private Tilemap[] tilemaps;
    [SerializeField] private int heightOfRoom = 105, widthOfRoom = 106;
    void Start()
    {
        
    }


    public void FillWalls()
    {
        int levelWidth  = levelBase.levelGrid.GetLength(0);
        int levelHeight = levelBase.levelGrid.GetLength(1);


        int wallTilelmapIndx = 1;
        int groundTilemapindx = 0;


        int tilemapWallMemIndx = 0;
        int[] doorsMemory = { 1, 2, 3, 4 };

        //tileMapMemories[0].ReadTile(0, 0); //just to be shure it unSerilized
        //TileBase[,] wallTiles = tileMapMemories[0].tilemapUnserializable;

        for (int x = 0; x < levelWidth; x++)
        {
            for(int y = 0; y < levelHeight; y++)
            {
                Vector3Int startPoint = new Vector3Int(x * widthOfRoom, y * heightOfRoom, 0);
                if (levelBase.levelGrid[x, y].exist)
                {
                    //Wright down wall
                    for (int y1 = 0; y1 < 2; y1++)
                    {
                        for (int x1 = 0; x1 < widthOfRoom; x1++)
                        {
                            tilemaps[wallTilelmapIndx].SetTile(new Vector3Int(startPoint.x + x1, startPoint.y + y1, startPoint.z), tileMapMemories[tilemapWallMemIndx].ReadTile(x1, y1));
                        }
                    }

                    //Wright up wall
                    for (int y1 = heightOfRoom - 1; y1 >= heightOfRoom - 1 - 2; y1--)
                    {
                        for (int x1 = 0; x1 < widthOfRoom; x1++)
                        {
                            tilemaps[wallTilelmapIndx].SetTile(new Vector3Int(startPoint.x + x1, startPoint.y + y1, startPoint.z), tileMapMemories[tilemapWallMemIndx].ReadTile(x1, y1));
                        }
                    }

                    //Wright left wall
                    for (int x1 = 0; x1 < 3; x1++)
                    {
                        for (int y1 = 2; y1 < heightOfRoom - 3; y1++)
                        {
                            tilemaps[wallTilelmapIndx].SetTile(new Vector3Int(startPoint.x + x1, startPoint.y + y1, startPoint.z), tileMapMemories[tilemapWallMemIndx].ReadTile(x1, y1));
                        }
                    }

                    //Wright right wall
                    for (int x1 = widthOfRoom - 1; x1 >= widthOfRoom - 1 - 2; x1--)
                    {
                        for (int y1 = 2; y1 < heightOfRoom - 3; y1++)
                        {
                            tilemaps[wallTilelmapIndx].SetTile(new Vector3Int(startPoint.x + x1, startPoint.y + y1, startPoint.z), tileMapMemories[tilemapWallMemIndx].ReadTile(x1, y1));
                        }
                    }

                    //build Top(0) door
                    if (levelBase.levelGrid[x, y].hasDoor[0])
                    {
                        Vector3Int doorStartPos = new Vector3Int(50, 102, 0);
                        for (int y1 = 0; y1 < 3; y1++)
                        {
                            for (int x1 = 0; x1 < 6; x1++)
                            {
                                tilemaps[groundTilemapindx].SetTile(new Vector3Int(doorStartPos.x + x1 + startPoint.x, doorStartPos.y + y1 + startPoint.y, doorStartPos.z + startPoint.z), tileMapMemories[doorsMemory[0]].ReadTile(x1, y1));
                                tilemaps[wallTilelmapIndx].SetTile(new Vector3Int(doorStartPos.x + x1 + startPoint.x, doorStartPos.y + y1 + startPoint.y, doorStartPos.z + startPoint.z), null);
                            }
                        }
                    }

                    //build Bottom(2) door
                    if (levelBase.levelGrid[x, y].hasDoor[2])
                    {
                        Vector3Int doorStartPos = new Vector3Int(50, 0, 0);
                        for (int y1 = 0; y1 < 2; y1++)
                        {
                            for (int x1 = 0; x1 < 6; x1++)
                            {
                                tilemaps[groundTilemapindx].SetTile(new Vector3Int(doorStartPos.x + x1 + startPoint.x, doorStartPos.y + y1 + startPoint.y, doorStartPos.z + startPoint.z), tileMapMemories[doorsMemory[2]].ReadTile(x1, y1));
                                tilemaps[wallTilelmapIndx].SetTile(new Vector3Int(doorStartPos.x + x1 + startPoint.x, doorStartPos.y + y1 + startPoint.y, doorStartPos.z + startPoint.z), null);
                            }
                        }
                    }

                    //build Left(3) door
                    if (levelBase.levelGrid[x, y].hasDoor[3])
                    {
                        Vector3Int doorStartPos = new Vector3Int(0, 49, 0);
                        for (int y1 = 0; y1 < 6; y1++)
                        {
                            for (int x1 = 0; x1 < 3; x1++)
                            {
                                tilemaps[groundTilemapindx].SetTile(new Vector3Int(doorStartPos.x + x1 + startPoint.x, doorStartPos.y + y1 + startPoint.y, doorStartPos.z + startPoint.z), tileMapMemories[doorsMemory[3]].ReadTile(x1, y1));
                                tilemaps[wallTilelmapIndx].SetTile(new Vector3Int(doorStartPos.x + x1 + startPoint.x, doorStartPos.y + y1 + startPoint.y, doorStartPos.z + startPoint.z), null);
                            }
                        }
                    }

                    //build Right(1) door
                    if (levelBase.levelGrid[x, y].hasDoor[1])
                    {
                        Vector3Int doorStartPos = new Vector3Int(103, 49, 0);
                        for (int y1 = 0; y1 < 6; y1++)
                        {
                            for (int x1 = 0; x1 < 3; x1++)
                            {
                                tilemaps[groundTilemapindx].SetTile(new Vector3Int(doorStartPos.x + x1 + startPoint.x, doorStartPos.y + y1 + startPoint.y, doorStartPos.z + startPoint.z), tileMapMemories[doorsMemory[1]].ReadTile(x1, y1));
                                tilemaps[wallTilelmapIndx].SetTile(new Vector3Int(doorStartPos.x + x1 + startPoint.x, doorStartPos.y + y1 + startPoint.y, doorStartPos.z + startPoint.z), null);
                            }
                        }
                    }

                }
            }
        }

    }
}
