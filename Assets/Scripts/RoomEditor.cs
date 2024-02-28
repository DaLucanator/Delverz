using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum User
{
    Null,
    Zion,
    Jayden,
    Luc,
    Bill,
    Elyes,
    Izzie
}

[ExecuteInEditMode]
public class RoomEditor : MonoBehaviour
{
    [SerializeField] private Tilemap myTileMap;
    [SerializeField] private TextAsset roomToLoad;
    [SerializeField] private bool loadRoom;
    [SerializeField] private User currentUser;
    [SerializeField] private string roomName;
    [SerializeField] private bool saveRoom, overwrite;

    [SerializeField] private TileIDsScriptableObject tileIDs;

    private List<Vector3Int> pressurePlate1Poses = new List<Vector3Int>();
    private List<Vector3Int> pressurePlate2Poses = new List<Vector3Int>();
    private List<Vector3Int> poweredTiles1Poses = new List<Vector3Int>();
    private List<Vector3Int> poweredTiles2Poses = new List<Vector3Int>();
    private Vector3 offset = new Vector3(0.5f, 0.5f, 0);

    void Update()
    {
        SetUpTrapnetwork();

        if (loadRoom)
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
        else if (currentUser == User.Null)
        {
            Debug.Log("Please select user");
        }
        else if(File.Exists(filePath) && !overwrite)
        {
            Debug.Log("File name already exists. Please enable overwrite if intended");
        }
        else
        {
            RoomData myRoomData = new RoomData();
            myRoomData.roomName = roomName;
            myRoomData.dateLastEdited = System.DateTime.Now.ToString();

            myTileMap.CompressBounds();
            BoundsInt myBounds = myTileMap.cellBounds;

            for (int x = myBounds.min.x; x < myBounds.max.x; x++)
            {
                for (int y = myBounds.min.y; y < myBounds.max.y; y++)
                {
                    Vector3Int tilePos = new Vector3Int(x, y, 0);

                    TileBase tile = myTileMap.GetTile(tilePos);
                    int tileID = tileIDs.ReturnTileID(tile);

                    myRoomData.tilePoses.Add(tilePos);
                    myRoomData.tileIDs.Add(tileID);

                    //for each tile in the tile map if it's a network1 pressure plate add it to this list
                    if (tileIDs.isPressurePlate1(tile)) { myRoomData.pressurePlate1Poses.Add(tilePos); }

                    //for each tile in the tile map if it's a network2 pressure plate add it to this list
                    if (tileIDs.isPressurePlate2(tile)) { myRoomData.pressurePlate2Poses.Add(tilePos); }

                    //for each tile in the tilemap if it's one of the following tiles add it to this list
                    if (tileIDs.isPoweredTile1(tile)) { myRoomData.poweredTiles1Poses.Add(tilePos); }

                    //for each tile in the tilemap if it's one of the following tiles add it to this list
                    if (tileIDs.isPoweredTile2(tile)) { myRoomData.poweredTiles2Poses.Add(tilePos); }

                }
            }

            string json = JsonUtility.ToJson(myRoomData, true);
            File.WriteAllText(filePath, json);
            
            roomName = null;
            overwrite = false;
            currentUser = User.Null;
            Debug.Log("Room was saved successfully");
        }
    }

    public void SetUpTrapnetwork()
    {
        myTileMap.CompressBounds();
        BoundsInt myBounds = myTileMap.cellBounds;

        pressurePlate1Poses.Clear();
        pressurePlate2Poses.Clear();
        poweredTiles1Poses.Clear();
        poweredTiles2Poses.Clear();

        for (int x = myBounds.min.x; x < myBounds.max.x; x++)
        {
            for (int y = myBounds.min.y; y < myBounds.max.y; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                TileBase tile = myTileMap.GetTile(tilePos);

                //for each tile in the tile map if it's a network1 pressure plate add it to this list
                if (tileIDs.isPressurePlate1(tile)) { pressurePlate1Poses.Add(tilePos); }

                //for each tile in the tile map if it's a network2 pressure plate add it to this list
                if (tileIDs.isPressurePlate2(tile)) { pressurePlate2Poses.Add(tilePos); }

                //for each tile in the tilemap if it's one of the following tiles add it to this list
                if (tileIDs.isPoweredTile1(tile)) { poweredTiles1Poses.Add(tilePos); }

                //for each tile in the tilemap if it's one of the following tiles add it to this list
                if (tileIDs.isPoweredTile2(tile)) { poweredTiles2Poses.Add(tilePos); }
            }
        }
    }

    private void OnDrawGizmos()
    {
        foreach (Vector3Int pressurePlate1 in pressurePlate1Poses)
        {
            foreach(Vector3Int poweredTile1 in poweredTiles1Poses)
            {
                Gizmos.color = Color.cyan;
                Vector3 point1 = pressurePlate1 + offset;
                Vector3 point2 = poweredTile1 + offset;

                Gizmos.DrawLine(point1, point2);
            }
        }

        foreach (Vector3Int pressurePlate2 in pressurePlate2Poses)
        {
            foreach (Vector3Int poweredTile2 in poweredTiles2Poses)
            {
                Gizmos.color = Color.magenta;
                Vector3 point1 = pressurePlate2 + offset;
                Vector3 point2 = poweredTile2 + offset;

                Gizmos.DrawLine(point1, point2);
            }
        }
    }
}
