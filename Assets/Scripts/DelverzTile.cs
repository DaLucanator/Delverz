using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DelverzTile : MonoBehaviour
{
    [SerializeField] private protected ColliderType colliderType;
    private protected int tileLayer;

    private protected Bounds bounds;
    private protected List<DelverzTile> tilesToTrigger = new List<DelverzTile>();
    private Tile myTileMapTile;

    protected virtual void Start()
    {
        if (colliderType == ColliderType.ground || colliderType == ColliderType.air) { tileLayer = 0; }
        else if (colliderType == ColliderType.groundObject) { tileLayer = 1; }
        else if (colliderType == ColliderType.projectile) { tileLayer = 2; }
        else { tileLayer = 3; }

        bounds = new Bounds(transform.position, Vector3.one * 0.96875f);

        if(CanMove(bounds))
        {
            foreach (DelverzTile tileToTrigger in tilesToTrigger)
            {
                tileToTrigger.Die();
            }
            if (tilesToTrigger.Count > 0) { DestroySelf(); }

            else { GridManager.current.AddToTileDictionary(tileLayer, bounds, this); }
            
        }

        else 
        {
        }
    }

    public ColliderType ReturnColliderType()
    {
        return colliderType;
    }

    public void SetColliderType(ColliderType typeToSetTo)
    {
        colliderType = typeToSetTo;
    }

    public virtual void Trigger(DelverzTile incomingTile)
    {

    }

    public virtual void Die()
    {

    }

    public virtual void DestroySelf()
    {
        GridManager.current.RemoveTileFromDictionary(tileLayer, bounds);
    }

    public bool CanMove(Bounds moveBounds)
    {
        tilesToTrigger = null;
        TileIntersect intersectData = GridManager.current.ReturnIntersectTiles(moveBounds, this);
        tilesToTrigger = intersectData.tilesToTrigger;
        return intersectData.canTraverse;
    }

    public virtual void Move(Vector3 movePos)
    {

    }

    public virtual void Move()
    {

    }
}

