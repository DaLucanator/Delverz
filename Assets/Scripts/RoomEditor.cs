using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class RoomEditor : MonoBehaviour
{
    [SerializeField] private Tilemap myTileMap;
    [SerializeField] private TextAsset roomToLoad;
    [SerializeField] private bool loadRoom;
    [SerializeField] private User currentUser;
    [SerializeField] private string roomName;
    [SerializeField] private bool saveRoom, overwrite;

    [SerializeField] private RoomData doNotTouch;
    [SerializeField] private TileIDsScriptableObject tileIDs;

    void Update()
    {
        if(loadRoom)
        {
            LoadRoom();
            loadRoom = false;
        }
        if (saveRoom) 
        { 
            SaveRoom();
            saveRoom = false;
        }
    }
    public void LoadRoom()
    {
        RoomData dataToLoad = JsonUtility.FromJson<RoomData>(roomToLoad.ToString());

        for (int i = 0; i < dataToLoad.tilePoses.Count; i++)
        {
            myTileMap.SetTile(dataToLoad.tilePoses[i], tileIDs.tileIDs[dataToLoad.tileIDs[i]]);
        }
        roomToLoad = null;
    }

    public void SaveRoom()
    {
        string filePath = Application.dataPath + "/Rooms/" + currentUser.ToString() + "/" + currentUser.ToString() + "_" + roomName + ".json";
        if (roomName == "") 
        {
            Debug.Log("Please Name Level");
        }
        else if(File.Exists(filePath) && !overwrite)
        {
            Debug.Log("File name already exists. Please enable overwrite if intended");
        }
        else
        {
            RoomData myRoomData = doNotTouch;
            myRoomData.roomName = roomName;
            myRoomData.dateLastEdited = System.DateTime.Now.ToString();

            myTileMap.CompressBounds();
            BoundsInt myBounds = myTileMap.cellBounds;
            myRoomData.tilePoses.Clear();
            myRoomData.tileIDs.Clear();

            for (int x = myBounds.min.x; x < myBounds.max.x; x++)
            {
                for (int y = myBounds.min.y; y < myBounds.max.y; y++)
                {
                    Vector3Int tilePos = new Vector3Int(x, y, 0);

                    if (!myRoomData.tilePoses.Contains(tilePos))
                    {
                        TileBase tile = myTileMap.GetTile(tilePos);
                        int tileID = tileIDs.ReturnTileID(tile);

                        myRoomData.tilePoses.Add(tilePos);
                        myRoomData.tileIDs.Add(tileID);
                    }
                }
            }

            string json = JsonUtility.ToJson(myRoomData, true);
            File.WriteAllText(filePath, json);
            
            roomName = null;
            overwrite = false;
            Debug.Log("Room was saved successfully");
        }
    }

    public void SetUpTrapnetwork()
    {
        myTileMap.CompressBounds();
        BoundsInt myBounds = myTileMap.cellBounds;

        for (int x = myBounds.min.x; x < myBounds.max.x; x++)
        {
            for (int y = myBounds.min.y; y < myBounds.max.y; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                TileBase tile = myTileMap.GetTile(tilePos);
                //for each tile in the tile map if it's a network1 pressupre plate add it to this list

                //for each tile in the tile map if it's a network2 pressupre plate add it to this list

                //for each tile in the tilemap if it's one of the following tiles add it to this list
                //for each tile in the tilemap if it's one of the following tiles add it to this list
            }
        }
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
