using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EquipmentTile : DelverzTile
{
    [SerializeField] private Vector3 spawnOffset;
    [SerializeField] private PlayerTile playerTile;

    public Vector3 ReturnSpawnOffset()
    {
        return spawnOffset;
    }

    public override void Move()
    {

    }
}
