using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTile : PoweredTile
{
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private GameObject projectileToSpawn;

    public override void PowerTile()
    {
        Instantiate(projectileToSpawn, transform.position + spawnPos, Quaternion.identity);
    }
}
