using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTile : DelverzTile
{
     protected override void Start()
    {
        base.Start();
    }

    public override void Trigger(DelverzTileData tileData)
    {
        Debug.Log("boop");
        tileData.playerRef.Die();
    }
}
