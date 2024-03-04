using System.Collections.Generic;
using System;
using UnityEngine;

public enum Ability
{
    crossbow,
}

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager current;

    private Dictionary<Ability, Action<Vector3, Vector3>> Abilities = new Dictionary<Ability, Action<Vector3, Vector3>>();

    void Awake()
    {
        current = this;
    }

    private void UseAbility(Ability abilityToUse, Vector3 abilityDirection, Vector3 playerPos)
    {
        Abilities[abilityToUse](abilityDirection, playerPos);
    }

    private void CrossbowAbility(Vector3 abilityDirection, Vector3 playerPos)
    {
        //Instantiate a crossbow bolt at playerpos+abilitydirection with direction abilitydirection
    }
}