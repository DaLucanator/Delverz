using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "ScriptableObjects/TileIDs")]
public class TileIDsScriptableObject : ScriptableObject
{
    public List<TileBase> tileIDs = new List<TileBase>();
    [SerializeField] private TileBase pressurePlate1, pressurePlate2;
    [SerializeField] private List<TileBase> poweredTiles1 = new List<TileBase>();
    [SerializeField] private List<TileBase> poweredTiles2 = new List<TileBase>();

    public int ReturnTileID(TileBase tile)
    {
        for (int i = 0; i < tileIDs.Count; i++)
        {
            if (tileIDs[i] == tile)
            {
                return i;
            }
        }

        Debug.Log("Tile ID not Found");
        return 0;
    }

    public bool isPressurePlate1(TileBase tile)
    {
        if (tile == pressurePlate1) { return true; }
        else return false;
    }

    public bool isPressurePlate2(TileBase tile)
    {
        if (tile == pressurePlate2) { return true; }
        else return false;
    }

    public bool isPoweredTile1(TileBase tile)
    {
        if (poweredTiles1.Contains(tile)) { return true; }
        else return false;
    }

    public bool isPoweredTile2(TileBase tile)
    {
        if (poweredTiles2.Contains(tile)) { return true; }
        else return false;
    }
}
