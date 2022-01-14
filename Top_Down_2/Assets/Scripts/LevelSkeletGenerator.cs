using System.Collections.Generic;
using UnityEngine;

public class LevelSkeletGenerator : MonoBehaviour
{
    [SerializeField] private RoomInfo firstRoomInfo;
    [SerializeField] private int lvlWidth = 2, lvlHeight = 2;
    [SerializeField] int doorSpawnChance = 50;
    [SerializeField] int minRoomAmmount = 8;

    List<Vector2Int> buildQueue = new List<Vector2Int>();
    public RoomInfo[,] levelGrid;

    void Start()
    {
        if(!(firstRoomInfo.position.x < lvlWidth))
        {
            firstRoomInfo.position.x = lvlWidth - 1;
        }
        if (!(firstRoomInfo.position.y < lvlWidth))
        {
            firstRoomInfo.position.y = lvlHeight - 1;
        }
        levelGrid = new RoomInfo[lvlWidth, lvlHeight];

        for(int x = 0; x < lvlWidth; x++)
        {
            for(int y = 0; y < lvlHeight; y++)
            {
                levelGrid[x,y] = new RoomInfo(x,y);
            }
        }

        
        GenerationBody();

        GetComponent<MapTester>().BuildMap();
        GetComponent<BuildLevelWalls>().FillWalls();
    }

    private void GenerationBody()
    {
        levelGrid[firstRoomInfo.position.x, firstRoomInfo.position.y] = firstRoomInfo;
        if (firstRoomInfo.hasDoor[0])
        {
            buildQueue.Add(firstRoomInfo.position + Vector2Int.up);
        }
        if (firstRoomInfo.hasDoor[1])
        {
            buildQueue.Add(firstRoomInfo.position + Vector2Int.right);
        }
        if (firstRoomInfo.hasDoor[2])
        {
            buildQueue.Add(firstRoomInfo.position + Vector2Int.down);
        }
        if (firstRoomInfo.hasDoor[3])
        {
            buildQueue.Add(firstRoomInfo.position + Vector2Int.left);
        }

        int chosenQueueInx = Random.Range(0, buildQueue.Count - 1);
        for(int x = 0; x < minRoomAmmount - 1; x++)
        {
            Vector2Int tempPos;
            if(chosenQueueInx != -1)
            {
                tempPos = buildQueue[buildQueue.Count - 1 - chosenQueueInx];
                buildQueue.RemoveAll(i => i == tempPos);
                chosenQueueInx = GenerateRoomFirstRun(tempPos.x, tempPos.y);
            }
            else
            {
                int tempInx = Random.Range(0, buildQueue.Count - 1);
                tempPos = buildQueue[tempInx];
                buildQueue.RemoveAll(i => i == tempPos);
                chosenQueueInx = GenerateRoomFirstRun(tempPos.x, tempPos.y);
            }
        }

        while(buildQueue.Count > 0)
        {
            int tempInx = Random.Range(0, buildQueue.Count - 1);
            Vector2Int tempPos = buildQueue[tempInx];
            buildQueue.RemoveAll(i => i == tempPos);
            GenerateRoom(tempPos.x, tempPos.y);
        }
       
    }

    void GenerateRoom(int x, int y)
    {
       
        ref RoomInfo freshRoom = ref levelGrid[x, y];

        if (levelGrid[x,y].exist)
        {
            buildQueue.RemoveAll(i => i == new Vector2Int(x,y));
            return;
        }

        freshRoom.exist = true;

        bool[] wantDoors = new bool[4];

        wantDoors[0] = Random.Range(0, 100) <= doorSpawnChance;
        wantDoors[1] = Random.Range(0, 100) <= doorSpawnChance;
        wantDoors[2] = Random.Range(0, 100) <= doorSpawnChance;
        wantDoors[3] = Random.Range(0, 100) <= doorSpawnChance;

        //scanTopPosition
        if (y < lvlHeight - 1)
        {
            if (levelGrid[x, y + 1].hasDoor[2])
            {
                freshRoom.hasDoor[0] = true;
            }
            else if (!levelGrid[x, y + 1].exist && y + 1 < lvlHeight && wantDoors[0])
            {
                freshRoom.hasDoor[0] = true;
                buildQueue.Add(freshRoom.position + Vector2Int.up);
            }
        }

        //scanBottomPosition
        if (y > 0)
        {
            if (levelGrid[freshRoom.position.x, freshRoom.position.y - 1].hasDoor[0])
            {
                freshRoom.hasDoor[2] = true;
            }
            else if (!levelGrid[x, y - 1].exist && freshRoom.position.y - 1 >= 0 && wantDoors[2])
            {
                freshRoom.hasDoor[2] = true;
                buildQueue.Add(freshRoom.position + Vector2Int.down);
            }
        }

        //scanRightPosition
        if (x < lvlWidth - 1)
        {
            if (levelGrid[x + 1, y].hasDoor[3])
            {
                freshRoom.hasDoor[1] = true;
            }
            else if (!levelGrid[x + 1, y].exist && freshRoom.position.x + 1 < lvlWidth && wantDoors[1])
            {
                freshRoom.hasDoor[1] = true;
                buildQueue.Add(freshRoom.position + Vector2Int.right);
            }
        }

        //scanLeftPosition
        if (x > 0)
        {
            if (levelGrid[x - 1, y].hasDoor[1])
            {
                freshRoom.hasDoor[3] = true;
            }
            else if (!levelGrid[x - 1, y].exist && freshRoom.position.x - 1 >= 0 && wantDoors[3])
            {
                freshRoom.hasDoor[3] = true;
                buildQueue.Add(freshRoom.position + Vector2Int.left);
            }
        }
    }

    int GenerateRoomFirstRun(int x, int y)
    {
        

        ref RoomInfo freshRoom = ref levelGrid[x, y];
        int newDoorsAmmount = -1;
        var doorsNotUsed = new List<int> { 0, 1, 2, 3 };

        if (freshRoom.exist) //chec for random error
        {
            buildQueue.RemoveAll(i => i == new Vector2Int(x, y));
            return -1; //tell to chose just random room from queue
        }

        freshRoom.exist = true;
        freshRoom.position = new Vector2Int(x, y); //it shoold already has right pos. But who knows

        bool[] wantDoors = new bool[4];
        
        
        //scanTopPosition
        if (y < lvlHeight - 1)
        {
            if(levelGrid[x, y + 1].exist)
            {
                if (levelGrid[x, y + 1].hasDoor[2])
                {
                    freshRoom.hasDoor[0] = true;
                }
                doorsNotUsed.Remove(0);
            }
        }
        else
        {
            doorsNotUsed.Remove(0);
        }

        //scanBottomPosition
        if (y > 0)
        {
            if (levelGrid[x, y - 1].exist)
            {
                if (levelGrid[x, y - 1].hasDoor[0])
                {
                    freshRoom.hasDoor[2] = true;
                }
                doorsNotUsed.Remove(2);
            }
        }
        else
        {
            doorsNotUsed.Remove(2);
        }

        
        //scanRightPosition
        if (x < lvlWidth - 1)
        {
            if (levelGrid[x + 1, y].exist)
            {
                if (levelGrid[x + 1, y].hasDoor[3])
                {
                    freshRoom.hasDoor[1] = true;
                }
                doorsNotUsed.Remove(1);
            }
        }
        else
        {
            doorsNotUsed.Remove(1);
        }

        //scanLeftPosition
        if (x > 0)
        {
            if (levelGrid[x - 1, y].exist)
            {
                if (levelGrid[x - 1, y].hasDoor[1])
                {
                    freshRoom.hasDoor[3] = true;
                }
                doorsNotUsed.Remove(3);
            }
        }
        else
        {
            doorsNotUsed.Remove(3);
        }

        //try to create at least one door
        if (doorsNotUsed.Count > 0)
        {
            int useThisDoor = Random.Range(0, doorsNotUsed.Count - 1);
            wantDoors[doorsNotUsed[useThisDoor]] = true;
            doorsNotUsed.RemoveAt(useThisDoor);
        }
        else //if fail tell to choose next room by clear rnd
        {
            return newDoorsAmmount; //here newDoorsAmmount always = -1 what means no new doors was created
        }

        //check rest posible doors if left
        while(doorsNotUsed.Count > 0)
        {
            if (Random.Range(0, 100) <= doorSpawnChance)
            {
                wantDoors[doorsNotUsed[0]] = true;
            }
            doorsNotUsed.RemoveAt(0);
        }

        //check to all planned doors
        if (wantDoors[0])
        {
            freshRoom.hasDoor[0] = true;
            buildQueue.Add(freshRoom.position + Vector2Int.up);
            newDoorsAmmount++;
        }
        if (wantDoors[1])
        {
            freshRoom.hasDoor[1] = true;
            buildQueue.Add(freshRoom.position + Vector2Int.right);
            newDoorsAmmount++;
        }
        if (wantDoors[2])
        {
            freshRoom.hasDoor[2] = true;
            buildQueue.Add(freshRoom.position + Vector2Int.down);
            newDoorsAmmount++;
        }
        if (wantDoors[3])
        {
            freshRoom.hasDoor[3] = true;
            buildQueue.Add(freshRoom.position + Vector2Int.left);
            newDoorsAmmount++;
        }
        if (newDoorsAmmount == -1)
        {
            return -1;
        }
        else
        {
            return Random.Range(0, newDoorsAmmount); //return rnd indx of created doors from created on this run
        }
    }

}

[System.Serializable]
public struct RoomInfo
{
    //top - 0, right - 1, bottom - 2, left - 3

    public bool[] hasDoor;

    public bool exist;

    public Vector2Int position;


    public RoomInfo(int x, int y)
    {
        hasDoor = new bool[4];
        exist = false;
        position = new Vector2Int(x,y);
    }

}

public struct RoomShouldBuild
{
    public Vector2Int roomIndx;
    public int doorDir;
    public Vector2Int roomFrom;

    public RoomShouldBuild(Vector2Int roomTocreate, int dir, Vector2Int createFrom)
    {
        roomIndx = roomTocreate;
        doorDir = dir;
        roomFrom = createFrom;
    }
    
}