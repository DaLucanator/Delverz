using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private Tilemap currentTilemap;
    [SerializeField] private RoomDepthList rooms = new RoomDepthList();
    [SerializeField] private TileIDsScriptableObject tileIDs;
    [SerializeField] private Transform Y3;
    private bool canDespawn = true;
    private List<RoomData> roomsToSpawn = new List<RoomData>();
    private int color1Int = 0, color2Int = 1;
    private Vector3Int verticalOffset = new Vector3Int (0,15,0);
    private int roomsSpawned = 0;
    private int roomsDeSpawned = 0;
    private float despawnSpeed = 0.1f;
    List<Vector3Int> tilesToDespawn = new List<Vector3Int>();
    List<Vector3Int> tilesToActuallyDespawn = new List<Vector3Int>();

    public static RoomManager current;

    private void Awake()
    {
        current = this;
    }

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

        for (int i = 0; i < 2; i++)
        {
            SpawnRoom();
        }
    }

    public void SpawnRoom()
    {
       RoomData currentRoomData = roomsToSpawn[roomsSpawned];

        for (int i = 0; i < currentRoomData.tilePoses.Count; i++)
        {
            TileBase currentTile = tileIDs.tileIDs[currentRoomData.tileIDs[i]];
            Vector3Int currentPos = currentRoomData.tilePoses[i] + (verticalOffset * roomsSpawned);
            currentTilemap.SetTile(currentPos, currentTile);
            if( currentTilemap.GetInstantiatedObject(currentPos).GetComponent<DelverzTile>() != null) 
            {
                DelverzTile currentObject = currentTilemap.GetInstantiatedObject(currentPos).GetComponent<DelverzTile>();
                currentObject.SetTilemapPos(currentPos);
            }

            tilesToDespawn.Add(currentPos);
        }

        //give the list of tiles in network to each pressure plate
        List<PoweredTile> poweredTiles1 = new List<PoweredTile>();
        List<PoweredTile> poweredTiles2 = new List<PoweredTile>();

        foreach (Vector3Int poweredTile1Pos in currentRoomData.poweredTiles1Poses)
        {
            PoweredTile poweredTile1 = currentTilemap.GetInstantiatedObject(poweredTile1Pos + (verticalOffset * roomsSpawned)).GetComponent<PoweredTile>();
            poweredTile1.SetColor(color1Int);
            poweredTiles1.Add(poweredTile1);
        }

        foreach (Vector3Int poweredTile2Pos in currentRoomData.poweredTiles2Poses)
        {
            PoweredTile poweredTile2 = currentTilemap.GetInstantiatedObject(poweredTile2Pos + (verticalOffset * roomsSpawned)).GetComponent<PoweredTile>();
            poweredTile2.SetColor(color2Int);
            poweredTiles2.Add(poweredTile2);
        }

        foreach (Vector3Int pressurePlate1Pos in currentRoomData.pressurePlate1Poses)
        {
            PressurePlateTile pressurePlate1 = currentTilemap.GetInstantiatedObject(pressurePlate1Pos + (verticalOffset * roomsSpawned)).GetComponent<PressurePlateTile>();
            pressurePlate1.SetColor(color1Int);
            pressurePlate1.SetPowerTiles(poweredTiles1);
        }

        foreach (Vector3Int pressurePlate2Pos in currentRoomData.pressurePlate2Poses)
        {
            PressurePlateTile pressurePlate2 = currentTilemap.GetInstantiatedObject(pressurePlate2Pos + (verticalOffset * roomsSpawned)).GetComponent<PressurePlateTile>();
            pressurePlate2.SetColor(color2Int);
            pressurePlate2.SetPowerTiles(poweredTiles2);
        }

        if(color1Int >= 3) { color1Int = 0; }
        else { color1Int++; }

        if (color2Int >= 3) { color2Int = 0; }
        else { color2Int++; }

        roomsSpawned++;
        Debug.Log(roomsSpawned);
    }

    private void Update()
    {
        if (canDespawn && roomsSpawned >= 2) { DeSpawnRoom(); }

    }

    private void DeSpawnRoom()
    {
        canDespawn = false;
        for (int i = 0; i < tilesToDespawn.Count; i++)
        {
            int randInt = Random.Range(0, tilesToDespawn.Count);
            Vector3Int currentPos = tilesToDespawn[randInt];
            if (currentPos.y < Y3.position.y) 
            {
                tilesToActuallyDespawn.Add(currentPos);
                tilesToDespawn.Remove(currentPos);
            }
        }

        if(tilesToActuallyDespawn.Count > 1)
        {
            int randInt = Random.Range(0, tilesToActuallyDespawn.Count);
            Vector3Int currentPos = tilesToActuallyDespawn[randInt];
            if (currentTilemap.GetInstantiatedObject(currentPos) != null)
            {
                DelverzTile currentObject = currentTilemap.GetInstantiatedObject(currentPos).GetComponent<DelverzTile>();
                currentObject.DestroySelf();
            }

            else { currentTilemap.SetTile(currentPos, tileIDs.tileIDs[0]); }
            tilesToActuallyDespawn.Remove(currentPos);

            StartCoroutine(Wait());
        }

        else { canDespawn = true;}
    }

    private IEnumerator Wait()
    {
        float speednum = tilesToActuallyDespawn.Count;
        despawnSpeed = 1f / speednum;

        yield return new WaitForSeconds(despawnSpeed);

        canDespawn = true;
    }

    public void RemoveTile(Vector3Int tilePos)
    {
        currentTilemap.SetTile(tilePos, tileIDs.tileIDs[0]);
        if (currentTilemap.GetInstantiatedObject(tilePos).GetComponent<DelverzTile>() != null)
        {
            DelverzTile currentObject = currentTilemap.GetInstantiatedObject(tilePos).GetComponent<DelverzTile>();
            currentObject.SetTilemapPos(tilePos);
        }
    }

    public void TrueRemoveTile(Vector3Int tilePos)
    {
        currentTilemap.SetTile(tilePos, null);
    }

    public void SetTile(TileBase tileToSet, Vector3Int posToSet)
    {
        currentTilemap.SetTile(posToSet, tileToSet);
    }


}

