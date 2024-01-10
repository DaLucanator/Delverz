using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Tilemaps;

public class DelverzTile : MonoBehaviour
{
    [SerializeField] protected ColliderType myColliderType;
    [ShowIf("myColliderType", ColliderType.player)]
    [SerializeField] private Player myPlayerRef;

    private int myTileLayer;
    private DelverzTileData myTileData;
    private Bounds myBounds;
    private Tile myTileMapTile;

    [SerializeField] private List<DelverzTileData> tilesToTraverseTo;

    protected virtual void Start()
    {
        if(myColliderType != ColliderType.player) { myPlayerRef = null; }

        if (myColliderType == ColliderType.groundObject) { myTileLayer = 1; }
        else if (myColliderType == ColliderType.ground || myColliderType == ColliderType.air) { myTileLayer = 0; }
        else { myTileLayer = 2; }

        myTileData = new DelverzTileData (myPlayerRef, myColliderType, this);

        myBounds = new Bounds(transform.position, Vector3.one * 0.96875f);

        GridManager.current.AddToTileDictionary(myTileLayer,myBounds, myTileData);
    }

    public virtual void Trigger(DelverzTileData tileData)
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }


    public bool CanMove(Bounds moveBounds)
    {
        tilesToTraverseTo = null;
        tilesToTraverseTo = GridManager.current.ReturnIntersectTiles(moveBounds,myTileData);

        if (tilesToTraverseTo == null) { return false; }

        else return true;
    }
    public void Move(Vector3 movePos)
    {
        transform.SetPositionAndRotation(movePos, Quaternion.identity);
        GridManager.current.TraverseTile(tilesToTraverseTo, myTileData);
    }
}

