using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class RoomDepth
{
    public List<TextAsset> roomsToRandomize;
}

[System.Serializable]
public class RoomDepthList
{
    public List<RoomDepth> roomDepth;
}

public class RoomManager : MonoBehaviour
{
    //spawn first room
    //first room's tilemap becomes my tilemap

    [SerializeField] private Tilemap currentTilemap;
    [SerializeField] private RoomDepthList rooms = new RoomDepthList();
    private List<RoomData> roomsToSpawn = new List<RoomData>();
    private Vector3Int verticalOffset = new Vector3Int (0,15,0);
    private int roomsSpawned = 0;

    private void Start()
    {
        ConvertTextAssetsToRoomData();
    }

    private void ConvertTextAssetsToRoomData()
    {
        for (int i = 0; i < rooms.roomDepth.Count; i++)
        {
            RoomDepth currentDepth = rooms.roomDepth[i];
            TextAsset randomTextAsset = currentDepth.roomsToRandomize[Random.Range(0, currentDepth.roomsToRandomize.Count)];
            roomsToSpawn.Add(JsonUtility.FromJson<RoomData>(randomTextAsset.ToString()));
        }

        SpawnRoom();
        SpawnRoom();
        SpawnRoom();
    }

    void SpawnRoom()
    {
       Debug.Log("boop");
       RoomData currentRoomData = roomsToSpawn[roomsSpawned];

        for (int i = 0; i < currentRoomData.tilePoses.Count; i++)
        {
            currentTilemap.SetTile(currentRoomData.tilePoses[i] + (verticalOffset * roomsSpawned) , currentRoomData.tiles[i]);
        }
        
        roomsSpawned++;
    }
}

