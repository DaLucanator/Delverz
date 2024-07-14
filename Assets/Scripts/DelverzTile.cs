using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class DelverzTile : MonoBehaviour
{
    [SerializeField] private protected ColliderType colliderType;
    private protected int tileLayer;

    private protected Bounds bounds;
    private protected Vector3Int myTilemapPos = new Vector3Int(-1000,-1000,-1000);
    private protected List<DelverzTile> tilesToTrigger = new List<DelverzTile>();
    protected BoxCollider2D myCollider;

    //only PlayerTile & SwordTile uses this. It's kinda bad to have it here but it makes gridmanager less messy
    protected SwordTile mySword;
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
        Vector3 pos = transform.TransformPoint(bounds.center);
        bounds = myCollider.bounds;
        bounds.center = pos;
        bounds.center = new Vector3(bounds.center.x, bounds.center.y, 0);
        myCollider.enabled = false;
        GridManager.current.AddToTileDictionary(tileLayer, bounds, this);
    }

    public void SetTilemapPos( Vector3Int posToSet)
    {
        myTilemapPos = posToSet;
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
        if (myTilemapPos != new Vector3Int(-1000, -1000, -1000)) { RoomManager.current.RemoveTile(myTilemapPos); }
    }

    public void TrueDestroySelf()
    {
        GridManager.current.RemoveTileFromDictionary(tileLayer, bounds);
        RoomManager.current.TrueRemoveTile(myTilemapPos);
    }

    public virtual bool CanMove(Bounds moveBounds)
    {
        tilesToTrigger = null;
        TileIntersect intersectData = GridManager.current.ReturnIntersectTiles(moveBounds, this);
        tilesToTrigger = intersectData.tilesToTrigger;
        return intersectData.canTraverse;
    }

    public virtual bool CanMove (Bounds moveBounds, Bounds swordBounds)
    {
        Debug.Log("base CanMove method called. This shouldn't happen");
        return false;
    }

    public virtual void Move(Vector3 movePos)
    {

    }

    public virtual void Move()
    {

    }

    protected virtual void FixedUpdate()
    {
        /*
        if(SceneController.current.IsMainScene())
        {
            if (GameController.current.ReturnIsOffScreen(transform.position)) { DestroySelf(); }
        }
        */

    }
    protected virtual void Update()
    {

    }
}

