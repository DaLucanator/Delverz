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
    [SerializeField] private TileIDsScriptableObject tileIDs;
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

        for (int i = 0; i < rooms.roomDepth.Count; i++)
        {
            SpawnRoom();
        }
    }

    void SpawnRoom()
    {
       RoomData currentRoomData = roomsToSpawn[roomsSpawned];

        for (int i = 0; i < currentRoomData.tilePoses.Count; i++)
        {
            currentTilemap.SetTile(currentRoomData.tilePoses[i] + (verticalOffset * roomsSpawned) , tileIDs.tileIDs[currentRoomData.tileIDs[i]]);
        }

        // give the list of tiles in network to each pressure plate
        List<PoweredTile> poweredTiles1 = new List<PoweredTile>();
        List<PoweredTile> poweredTiles2 = new List<PoweredTile>();

        foreach (Vector3Int poweredTile1Pos in currentRoomData.poweredTiles1Poses)
        {
            PoweredTile poweredTile1 = currentTilemap.GetInstantiatedObject(poweredTile1Pos + (verticalOffset * roomsSpawned)).GetComponent<PoweredTile>();
            poweredTiles1.Add(poweredTile1);
        }

        foreach (Vector3Int poweredTile2Pos in currentRoomData.poweredTiles2Poses)
        {
            PoweredTile poweredTile2 = currentTilemap.GetInstantiatedObject(poweredTile2Pos + (verticalOffset * roomsSpawned)).GetComponent<PoweredTile>();
            poweredTiles2.Add(poweredTile2);
        }

        foreach (Vector3Int pressurePlate1Pos in currentRoomData.pressurePlate1Poses)
        {
            PressurePlateTile pressurePlate1 = currentTilemap.GetInstantiatedObject(pressurePlate1Pos + (verticalOffset * roomsSpawned)).GetComponent<PressurePlateTile>();
            pressurePlate1.SetPowerTiles(poweredTiles1);
        }

        foreach (Vector3Int pressurePlate2Pos in currentRoomData.pressurePlate2Poses)
        {
            PressurePlateTile pressurePlate2 = currentTilemap.GetInstantiatedObject(pressurePlate2Pos + (verticalOffset * roomsSpawned)).GetComponent<PressurePlateTile>();
            pressurePlate2.SetPowerTiles(poweredTiles2);
        }

        roomsSpawned++;
    }
}

