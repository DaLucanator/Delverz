using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "ScriptableObjects/TileIDs")]
public class TileIDsScriptableObject : ScriptableObject
{
    public List<TileBase> tileIDs = new List<TileBase>();

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
}
