using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileMapAsset", menuName = "TileMapAsset", order = 52)]
public class TileMapMemory : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private int width = 1, height = 1;
    [SerializeField] private bool isSerialized = false;
    public TileBase[,] tilemapUnserializable = new TileBase[1,1];

    [System.Serializable]
    struct Package<TElement>
    {
        public int Index0;
        public int Index1;
        public TElement Element;
        public Package(int idx0, int idx1, TElement element)
        {
            Index0 = idx0;
            Index1 = idx1;
            Element = element;
        }
    }
    public TileBase[,] Create(int width, int height)
    {
        this.height = height;
        this.width = width;
        return tilemapUnserializable = new TileBase[width, height];
    }

    public void AddTile(int x, int y, TileBase tileBase)
    {
        if (isSerialized)
        {
            OnAfterDeserialize();
        }
        tilemapUnserializable[x, y] = tileBase;
    }

    public TileBase ReadTile(int x, int y)
    {
        if (isSerialized)
        {
            OnAfterDeserialize();
        }
        return tilemapUnserializable[x, y];
    }

    // A list that can be serialized
    [SerializeField, HideInInspector] private List<Package<TileBase>> serializable;
    // A package to store our stuff
    public void OnBeforeSerialize()
    {
        // Convert our unserializable array into a serializable list
        serializable = new List<Package<TileBase>>();
        for (int i = 0; i < tilemapUnserializable.GetLength(0); i++)
        {
            for (int j = 0; j < tilemapUnserializable.GetLength(1); j++)
            {
                serializable.Add(new Package<TileBase>(i, j, tilemapUnserializable[i, j]));
            }
        }
        isSerialized = true;
    }
    public void OnAfterDeserialize()
    {
        // Convert the serializable list into our unserializable array
        tilemapUnserializable = new TileBase[width, height];
        foreach (var package in serializable)
        {
            tilemapUnserializable[package.Index0, package.Index1] = package.Element;
        }
        isSerialized = false;
    }
}

