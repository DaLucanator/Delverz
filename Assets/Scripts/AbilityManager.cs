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

    private Dictionary<Ability, Action<Vector3, Vector3>> Abilities = new Dictionary<Ability, Action<Vector3, Vector3>>();

    [SerializeField] private GameObject crossbowBolt, sword, shield, fireball;

    void Awake()
    {
        current = this;
    }

    public void UseAbility(Ability abilityToUse, Vector3 abilityDirection, Vector3 playerPos)
    {
        Abilities[abilityToUse](abilityDirection, playerPos);
    }

    private void CrossbowAbility(Vector3 abilityDirection, Vector3 playerPos)
    {
        //Instantiate a crossbow bolt at playerpos+abilitydirection with direction abilitydirection
        GameObject crossBowBolt = Instantiate(crossbowBolt, playerPos + abilityDirection, Quaternion.identity);
        ProjectileTile currentProjectile = crossbowBolt.GetComponent<ProjectileTile>();
        currentProjectile.SetDirection(abilityDirection);
    }

    private void SwordAbility()
    {
        //Instantiate a sword playerpos+abilitydirection with direction abilitydirection
        //the script on the sword follows the player (or maybe script here)
        //the sword listens for input
        //it kills any player it touches
        //afterwhich it crumbles away
    }

    private void ShieldAbility()
    {
        //Instantiate a shield at playerpos+abilitydirection with direction abilitydirection
        //the script on the shield follows the player (or maybe script here)
        //it reflects projectiles and stops sword blows
        //afterwhich it crumbles away
    }

    private void FireballAbility()
    {
        //Instantiate a fireball at playerpos+abilitydirection with direction abilitydirection
        //the fireball explodes and puts damaging fire on the ground if it has a triggering collision
    }


}