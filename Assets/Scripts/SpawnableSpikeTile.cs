using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableSpikeTile : SpikeTile
{
    private bool spawned;


    protected override void Start()
    {

    }

    public void PopulateTile()
    {
        if (!spawned)
        {
            //normal start stuff is done here becuase running it on a disabled object was very buggy
            colliderType = ColliderType.groundObject;
            myCollider = this.GetComponent<BoxCollider2D>();
            bounds = myCollider.bounds;
            bounds.center = transform.position;

            tileLayer = 1;

            spawned = true;
        }

        //kill any player that's standing in the way (normal spike tile trigger)
        tilesToTrigger = null;
        GridManager.current.AddToTileDictionary(tileLayer, bounds, this);
        TileIntersect intersectData = GridManager.current.ReturnIntersectTiles(bounds, this);
        tilesToTrigger = intersectData.tilesToTrigger;

        foreach (DelverzTile tile in intersectData.tilesToTrigger)
        {
            tile.Die();
        }
    }
    public void DePopulateTile()
    {
        GridManager.current.RemoveTileFromDictionary(tileLayer, bounds);
    }
}
