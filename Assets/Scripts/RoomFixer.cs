using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;


[ExecuteInEditMode]
public class RoomFixer : MonoBehaviour
{
    [SerializeField] private TextAsset roomToFix;
    [SerializeField] private User currentUser;
    [SerializeField] private bool fixRoom;

    [SerializeField] private TileIDsScriptableObject tileIDs;

    void Update()
    {
        if (fixRoom)
        {
            Debug.Log(roomToFix.name);
            FixRoom();
            fixRoom = false;
        }
    }
    public void FixRoom()
    {
        string filePath = Application.dataPath + "/Rooms/" + currentUser.ToString() + "/" + roomToFix.name + ".json";

        if (currentUser == User.Null)
        {
            Debug.Log("Please select user");
        }
        else if (!File.Exists(filePath))
        {
            Debug.Log("Existing Room not found. Wrong User?");
        }
        else
        {
            RoomData myRoomData = JsonUtility.FromJson<RoomData>(roomToFix.ToString());
            myRoomData.dateLastEdited = System.DateTime.Now.ToString();

            myRoomData.pressurePlate1Poses.Clear();
            myRoomData.pressurePlate2Poses.Clear();
            myRoomData.poweredTiles1Poses.Clear();
            myRoomData.poweredTiles2Poses.Clear();

            for (int i = 0; i < myRoomData.tileIDs.Count; i++)
            {
                TileBase currentTile = tileIDs.tileIDs[myRoomData.tileIDs[i]];

                //for each tile in the tile map if it's a network1 pressure plate add it to this list
                if (tileIDs.isPressurePlate1(currentTile)) { myRoomData.pressurePlate1Poses.Add(myRoomData.tilePoses[i]); }

                //for each tile in the tile map if it's a network2 pressure plate add it to this list
                if (tileIDs.isPressurePlate2(currentTile)) { myRoomData.pressurePlate2Poses.Add(myRoomData.tilePoses[i]); }

                //for each tile in the tilemap if it's one of the following tiles add it to this list
                if (tileIDs.isPoweredTile1(currentTile)) { myRoomData.poweredTiles1Poses.Add(myRoomData.tilePoses[i]); }

                //for each tile in the tilemap if it's one of the following tiles add it to this list
                if (tileIDs.isPoweredTile2(currentTile)) { myRoomData.poweredTiles2Poses.Add(myRoomData.tilePoses[i]); }
            }

            string json = JsonUtility.ToJson(myRoomData, true);
            File.WriteAllText(filePath, json);

            currentUser = User.Null;
            roomToFix = null;
            Debug.Log("Room was fixed succesfully");
        }
    }
}
