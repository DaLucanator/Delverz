using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : DelverzTile
{
    protected override void Start()
    {
        base.Start();
        RespawnManager.current.AddRespawnPos(transform.position);
    }
}
