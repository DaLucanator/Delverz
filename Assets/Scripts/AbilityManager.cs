using System.Collections.Generic;
using System;
using UnityEngine;

public enum Ability
{
    Null,
    Crossbow,
    Sword,
    SpeedPotion
}

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager current;

    private Dictionary<Ability, Action<Vector3, PlayerTile>> Abilities = new Dictionary<Ability, Action<Vector3, PlayerTile>>()
    {
        { Ability.Crossbow, (Vector3, playerTile) => current.CrossbowAbility(Vector3, playerTile) },
        { Ability.Sword, (Vector3, playerTile) => current.SwordAbility(Vector3, playerTile) }
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
        SwordTile currentSword;
        if(playerTile.ReturnSword()  == null)
        {
            currentSword = Instantiate(sword, playerTile.gameObject.transform).GetComponent<SwordTile>();
        }
        else { currentSword = playerTile.ReturnSword(); }

        currentSword.ChangeDirection(abilityDirection);
    }

    private void InvisibilityPotionAbility(Vector3 abilityDirection, PlayerTile playerTile)
    {
        playerTile.StartCoroutine(playerTile.InvisibilityPotion());
        playerTile.SpendAbility();
    }

    private void SpeedPotionAbility(Vector3 abilityDirection, PlayerTile playerTile)
    {
        playerTile.StartCoroutine(playerTile.SpeedPotion());
        playerTile.SpendAbility();
    }

    private void FireballAbility()
    {
        //Instantiate a fireball at playerpos+abilitydirection with direction abilitydirection
        //the fireball explodes and puts damaging fire on the ground if it has a triggering collision
        //the fire is a groundobject that cannot spawn on other ground objects
    }


}