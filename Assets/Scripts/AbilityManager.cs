using System.Collections.Generic;
using System;
using UnityEngine;

public enum Ability
{
    Null,
    Crossbow
}

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager current;

    private Dictionary<Ability, Action<Vector3, PlayerTile>> Abilities = new Dictionary<Ability, Action<Vector3, PlayerTile>>()
    {
        { Ability.Crossbow, (Vector3, playerTile) => current.CrossbowAbility(Vector3, playerTile) }
    };

    [SerializeField] private GameObject crossbowBolt, sword, shield, fireball;

    void Awake()
    {
        current = this;
    }

    public void UseAbility(Ability abilityToUse, Vector3 abilityDirection, PlayerTile playerTile)
    {
        Abilities[abilityToUse](abilityDirection, playerTile);
    }

    private void CrossbowAbility(Vector3 abilityDirection, PlayerTile playerTile)
    {
        GameObject currentCrossbowBolt = Instantiate(crossbowBolt, playerTile.transform.position + abilityDirection, Quaternion.identity);
        ProjectileTile currentProjectile = currentCrossbowBolt.GetComponent<ProjectileTile>();
        currentProjectile.SetDirection(abilityDirection);

        playerTile.SpendAbility();
    }

    private void SwordAbility(Vector3 abilityDirection, PlayerTile playerTile)
    {
        //Instantiate a sword playerpos+abilitydirection with direction abilitydirection
        //the player can change the direction of the shield whilst it exists
        //the script on the sword follows the player (or maybe script here)
        //the sword listens for input
        //it kills any player it touches
        //afterwhich it crumbles away
        //the ssword is also destroyed if it collides with a projectile
        //if the player has a sword change the direction of the sword otherwise spawn a sword

    }

    private void ShieldAbility()
    {
        //Instantiate a shield at playerpos+abilitydirection with direction abilitydirection
        //the player can change the direction of the shield whilst it exists
        //the script on the shield follows the player (or maybe script here)
        //it reflects projectiles and stops sword blows
        //afterwhich it crumbles away
    }

    private void FireballAbility()
    {
        //Instantiate a fireball at playerpos+abilitydirection with direction abilitydirection
        //the fireball explodes and puts damaging fire on the ground if it has a triggering collision
        //the fire is a groundobject that cannot spawn on other ground objects
    }


}