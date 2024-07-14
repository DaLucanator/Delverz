using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTile : PoweredTile
{
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private GameObject projectileToSpawn;

    private float delay = 0.25f;
    private bool canPower = true;

    public override void PowerTile()
    {
        if(canPower)
        {
            canPower = false;
            GameObject currentProjectile = Instantiate(projectileToSpawn, transform.position + spawnPos, Quaternion.identity);
            currentProjectile.GetComponent<ProjectileTile>().SetDirection(spawnPos);

            if (SoundManager.current.CanPlaySound(SoundToPlay.arrowTrap)) { mySound.Play(); }

            StartCoroutine(Wait());
        }

    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(delay);
        canPower = true;
    }
}
