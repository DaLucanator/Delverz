using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : DelverzTile
{
    protected override void Start()
    {
        bounds = new Bounds(transform.position, Vector3.one * 0.96875f);
        GridManager.current.AddToTileDictionary(tileLayer, bounds, this);
        RespawnManager.current.AddRespawnPos(bounds);
    }
}
