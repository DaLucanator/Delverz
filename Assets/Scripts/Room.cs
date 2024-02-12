using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class Room : MonoBehaviour
{
    [SerializeField] private Tilemap myTileMap;
    [SerializeField] private User currentUser;
    [SerializeField] private string roomName;
    [SerializeField] bool saveLevel;

    [SerializeField] private RoomData myRoomData;

    void Update()
    {
        if (saveLevel) 
        { 
            SaveRoom();
            saveLevel = false;
            Debug.Log("Room Data was updated successfully. Remember to save prefab :)");
        }
    }

    public void SaveRoom()
    {
        if(roomName == null) 
        {
            Debug.Log("Please Name Level");
        }
        else
        {
            myRoomData.roomName = roomName;
            myRoomData.dateLastEdited = System.DateTime.Now.ToString();

            myTileMap.CompressBounds();
            BoundsInt myBounds = myTileMap.cellBounds;
            myRoomData.tilePoses.Clear();
            myRoomData.tiles.Clear();

            for (int x = myBounds.min.x; x < myBounds.max.x; x++)
            {
                for (int y = myBounds.min.y; y < myBounds.max.y; y++)
                {
                    Vector3Int tilePos = new Vector3Int(x, y, 0);

                    if (!myRoomData.tilePoses.Contains(tilePos))
                    {
                        TileBase tile = myTileMap.GetTile(tilePos);
                        myRoomData.tilePoses.Add(tilePos);
                        myRoomData.tiles.Add(tile);
                    }
                }
            }

            string json = JsonUtility.ToJson(myRoomData, true);
            File.WriteAllText(Application.dataPath + "/Rooms/" + currentUser.ToString() + "/" + currentUser.ToString() + "_" + roomName + ".json", json);
        }

        roomName = null;
    }

    public enum User
    {
        Zion,
        Jayden,
        Luc,
        Bill,
        Elyes
    }
}
