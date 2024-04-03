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
    protected BoxCollider2D myCollider;

    //only PlayerTile & SwordTile uses this. It's kinda bad to have it here but it makes gridmanager less messy
    public SwordTile mySword;
    protected PlayerTile myPlayer;

    private void Awake()
    {
        if (colliderType == ColliderType.ground || colliderType == ColliderType.air) { tileLayer = 0; }
        else if (colliderType == ColliderType.groundObject) { tileLayer = 1; }
        else if (colliderType == ColliderType.projectile) { tileLayer = 2; }
        else if (colliderType == ColliderType.player) { tileLayer = 3; }
        else if (colliderType == ColliderType.wall) { tileLayer = 4; }
    }

    protected virtual void Start()
    {
        myCollider = this.GetComponent<BoxCollider2D>();
        bounds = myCollider.bounds;
        bounds.center = transform.position;
        myCollider.enabled = false;
        GridManager.current.AddToTileDictionary(tileLayer, bounds, this);
    }

    //only PlayerTile & SwordTile uses this. It's kinda bad to have it here but it makes gridmanager less messy
    public SwordTile ReturnSword()
    {
        return mySword;
    }

    public PlayerTile ReturnPlayer()
    {
        return myPlayer;
    }

    public ColliderType ReturnColliderType()
    {
        return colliderType;
    }

    public virtual void Trigger(PlayerTile incomingTile)
    {

    }

    public virtual void Die()
    {

    }

    public virtual void DestroySelf()
    {
        GridManager.current.RemoveTileFromDictionary(tileLayer, bounds);
        if (gameObject != null) { Destroy(gameObject); }
    }

    public virtual bool CanMove(Bounds moveBounds)
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

    protected virtual void FixedUpdate()
    {
        if (GameController.current.ReturnIsOffScreen(transform.position)) { DestroySelf(); }
    }
    protected virtual void Update()
    {

    }
}

