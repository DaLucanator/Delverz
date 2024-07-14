using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTile : DelverzTile
{
    public override void Trigger(PlayerTile incomingTile)
    {
        incomingTile.Die();
    }

    protected override void Start()
    {
        myCollider = this.GetComponent<BoxCollider2D>();
        Vector3 pos = transform.TransformPoint(bounds.center);
        bounds = myCollider.bounds;
        bounds.center = pos;
        bounds.center = new Vector3(bounds.center.x, bounds.center.y, 0);
        myCollider.enabled = false;
        GridManager.current.AddToTileDictionary(tileLayer, bounds, this);

        //kill any player that's standing in the way (normal air tile trigger)
        tilesToTrigger = null;

        TileIntersect intersectData = GridManager.current.ReturnIntersectTiles(bounds, this);
        tilesToTrigger = intersectData.tilesToTrigger;

        foreach (DelverzTile tile in intersectData.tilesToTrigger)
        {
            tile.Die();
        }
    }

    protected override void Update()
    {
        if (GameController.current.ReturnIsBelowScreen(transform.position)) { TrueDestroySelf(); }
    }

    public override void DestroySelf()
    {
    }



}
