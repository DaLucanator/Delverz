using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableWallTile : DelverzTile
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
            colliderType = ColliderType.wall;
            myCollider = this.GetComponent<BoxCollider2D>();
            bounds = myCollider.bounds;
            bounds.center = transform.position;

            tileLayer = 4;
            spawned = true;
        }

        GridManager.current.AddToTileDictionary(tileLayer, bounds, this);

    }
    public void DePopulateTile()
    {
        GridManager.current.RemoveTileFromDictionary(tileLayer, bounds);
    }
}
