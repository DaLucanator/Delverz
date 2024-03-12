using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum ColliderType
{
    player,
    wall,
    projectile,
    ground,
    groundObject,
    air
}

public struct TileIntersect
{
    public bool canTraverse;
    public List<DelverzTile> tilesToTrigger;
}

public class GridManager : MonoBehaviour
{
    public static GridManager current;

    public static Tilemap tilemap;

    static Dictionary<Bounds, DelverzTile> tileDictionaryLayer1 = new Dictionary<Bounds, DelverzTile>();
    static Dictionary<Bounds, DelverzTile> tileDictionaryLayer2 = new Dictionary<Bounds, DelverzTile>();
    static Dictionary<Bounds, DelverzTile> tileDictionaryLayer3 = new Dictionary<Bounds, DelverzTile>();
    static Dictionary<Bounds, DelverzTile> tileDictionaryLayer4 = new Dictionary<Bounds, DelverzTile>();

    private Dictionary<Bounds, DelverzTile>[] tileDictionaries = { tileDictionaryLayer1, tileDictionaryLayer2, tileDictionaryLayer3, tileDictionaryLayer4 };

    private void Awake()
    {
        current= this;
    }

    public void AddToTileDictionary (int tileLayer, Bounds tileBounds, DelverzTile tile)
    {
        tileDictionaries[tileLayer].Add (tileBounds, tile);
    }

    public void RemoveTileFromDictionary(int tileLayer, Bounds tileBounds)
    {
        tileDictionaries[tileLayer].Remove(tileBounds);
    }

    public TileIntersect ReturnIntersectTiles (Bounds moveBounds, DelverzTile myTile)
    {
        TileIntersect intersectData = new TileIntersect();
        intersectData.tilesToTrigger = new List<DelverzTile>();
        intersectData.canTraverse = true;

        //for each layer of the tile dictionary
        for (int i = 3; i >= 0; i--)
        {
            //if the bounding box generated by my vector contains a bounding box from this dictionary
            foreach (KeyValuePair<Bounds, DelverzTile> boundsY in tileDictionaries[i])
            {
                if (moveBounds.Intersects(boundsY.Key))
                {
                    DelverzTile tileIAmTraversingTo = boundsY.Value;
                    ColliderType otherColliderType = tileIAmTraversingTo.ReturnColliderType();
                    ColliderType myColliderType = myTile.ReturnColliderType();

                    //if I'm a player and incoming tile is a wall return hardcollisiom
                    if (myColliderType == ColliderType.player  && otherColliderType == ColliderType.wall) { intersectData.canTraverse = false; return intersectData; }
                    //if I'm a player and incoming tile is a another player that isn't me return hardcollision
                    else if (myColliderType == ColliderType.player && otherColliderType == ColliderType.player && tileIAmTraversingTo != myTile) { intersectData.canTraverse = false; return intersectData; }
                    //if I'm a player and incoming tile is not the ground and isn't me return triggeringcollision
                    else if (myColliderType == ColliderType.player && otherColliderType != ColliderType.ground && tileIAmTraversingTo != myTile) { intersectData.tilesToTrigger.Add(boundsY.Value); }

                    //if I'm a projectile and incoming tile is a projectile that isn't me return triggeringcollision
                    else if (myColliderType == ColliderType.projectile && otherColliderType == ColliderType.projectile && tileIAmTraversingTo != myTile) { intersectData.tilesToTrigger.Add(boundsY.Value); }
                    //if I'm a projectile and incoming tile is a player or a wall return triggeringcollision
                    else if (myColliderType == ColliderType.projectile && (otherColliderType == ColliderType.player || otherColliderType == ColliderType.wall)) { intersectData.tilesToTrigger.Add(boundsY.Value); }

                    //if I'm a ground object and incoming tile is a player return triggering collision
                    else if (myColliderType == ColliderType.groundObject && otherColliderType == ColliderType.projectile && tileIAmTraversingTo != myTile) { intersectData.tilesToTrigger.Add(boundsY.Value); }
                }
            }
        }
        return intersectData;
    }
}
