using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTile : PoweredTile
{
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private GameObject projectileToSpawn;

    public override void PowerTile()
    {
        GameObject currentProjectile = Instantiate(projectileToSpawn, transform.position + spawnPos, Quaternion.identity);
        currentProjectile.GetComponent<ProjectileTile>().SetDirection(spawnPos);

        if(SoundManager.current.CanPlaySound(SoundToPlay.arrowTrap)) { mySound.Play(); }
    }
}
