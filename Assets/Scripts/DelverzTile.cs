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

    private void Awake()
    {
        if (colliderType == ColliderType.ground || colliderType == ColliderType.air) { tileLayer = 0; }
        else if (colliderType == ColliderType.groundObject) { tileLayer = 1; }
        else if (colliderType == ColliderType.projectile) { tileLayer = 2; }
        else { tileLayer = 3; }
    }

    protected virtual void Start()
    {
        bounds = new Bounds(transform.position, Vector3.one * 0.96875f);
        GridManager.current.AddToTileDictionary(tileLayer, bounds, this);
    }

    public ColliderType ReturnColliderType()
    {
        return colliderType;
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

    //if I'm off the screen destroy me
    private void OffScreenCheck()
    {
        
    }

    //I put these here in case it fixed a bug. I left it because no harm.
    protected virtual void FixedUpdate()
    {

    }
    protected virtual void Update()
    {

    }
}

