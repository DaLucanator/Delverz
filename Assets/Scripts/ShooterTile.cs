using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTile : PoweredTile
{
    [SerializeField] private Direction projectileDirection;
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private GameObject projectileToSpawn;

    public override void PowerTile()
    {
        GameObject currentProjectile = Instantiate(projectileToSpawn, transform.position + spawnPos, Quaternion.identity);
        currentProjectile.GetComponent<ProjectileTile>().SetDirection(projectileDirection);
    }
}
