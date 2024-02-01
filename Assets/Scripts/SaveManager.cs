using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SaveManager : MonoBehaviour
{
    [SerializeField] BoundsInt bounds;
    //save tiles in Room Jsons as references to tile scriptable objects via ids
    //save Rooms as Room Jsons
    //save Levels as a Level scriptable object to reference via IDs
    //before leaving editor save data as JSON

    //Load Level
    //Load the Level with this name

    //Load Room
    //Load from the list of lists of rooms, a random room and the tiles from that room, adding them to the tilemap.

    //a level is a list of a list of room
    //a room is a list of tilemap ids and positions
    //a tile is a list of tile ids and tilebases

    //tileSO = tile id, tilebase
    //roomSO = room id, room prefab
    //leve

    public Tilemap tilemap;

    //Instantiate the room
    //load all the tiles from my tilemap onto the base tilemap

    //levels are saved as JSON

    public void SaveRoom()
    {
        bounds = tilemap.cellBounds;
        RoomData roomData = new RoomData();
        for(int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(tilePos);
                roomData.tiles.Add(tile);
                roomData.pos.Add(tilePos);

            }
        }

        string json = JsonUtility.ToJson(roomData, true);
        File.WriteAllText(Application.dataPath  + "/Saves/Rooms/testRoom 2.json", json);

        Debug.Log("saved room");
    }

    public void SetCurrentRoom()
    {
        string json = File.ReadAllText(Application.dataPath + "/Saves/Rooms/testRoom 1.json");
        RoomData data = JsonUtility.FromJson<RoomData>(json);

        for(int i = 0; i < data.pos.Count; i++ )
        {
            tilemap.SetTile(data.pos[i], data.tiles[i]);
        }

        //Load Complete
    }

    public class RoomData
    {
        //maybe change this to dictionary
        public List<TileBase> tiles = new List<TileBase>();
        public List<Vector3Int> pos = new List<Vector3Int>();
        public string roomName;
        public int roomID;
    }

    public class LevelData
    {
        public List<List<int>> rooms = new List<List<int>>();
        public string levelName;
        public int levelID;
    }
}
