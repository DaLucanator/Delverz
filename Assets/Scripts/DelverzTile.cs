using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Scripting.APIUpdating;
using System.Security.Cryptography;

public class DelverzTile : MonoBehaviour
{
    [SerializeField] private protected ColliderType colliderType;
    private protected int tileLayer;

    private protected Bounds bounds;
    private protected List<DelverzTile> tilesToTraverseTo;
    private Tile myTileMapTile;

    protected virtual void Start()
    {

        if (colliderType == ColliderType.groundObject) { tileLayer = 1; }
        else if (colliderType == ColliderType.ground || colliderType == ColliderType.air) { tileLayer = 0; }
        else { tileLayer = 2; }

        bounds = new Bounds(transform.position, Vector3.one * 0.96875f);

        GridManager.current.AddToTileDictionary(tileLayer,bounds,this);
    }

    public ColliderType ReturnColliderType()
    {
        return colliderType;
    }

    public virtual void Trigger(DelverzTile incomingTile)
    {

    }

    public virtual void DestroySelf()
    {
        GridManager.current.RemoveTileFromDictionary(tileLayer, bounds);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }


    public bool CanMove(Bounds moveBounds)
    {
        tilesToTraverseTo = null;
        tilesToTraverseTo = GridManager.current.ReturnIntersectTiles(moveBounds,this);

        if (tilesToTraverseTo == null) { return false; }

        else return true;
    }
    public virtual void Move(Vector3 movePos)
    {

    }

    public virtual void Move()
    {

    }
}

