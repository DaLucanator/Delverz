using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTile : DelverzTile
{
    [SerializeField] PlayerInputScript myPlayerInputScript;
    private float treasureAmount;
    private int TreasureAmountDisplay;
    private bool isDead;
    private Ability currentAbility;

    private List<PressurePlateTile> pressurePlateTiles = new List<PressurePlateTile>();

    public override void Move(Vector3 movePos)
    {
        GridManager.current.RemoveTileFromDictionary(tileLayer, bounds);

        transform.SetPositionAndRotation(movePos, Quaternion.identity);
        bounds = new Bounds(transform.position, Vector3.one * 0.96875f);
        GridManager.current.AddToTileDictionary(tileLayer, bounds, this);

        List<PressurePlateTile> tilesToRemove = new List<PressurePlateTile>();

        //depower any pressure plate tiles that aren't in tilesToTrigger anymore and remove them from that list
        foreach (PressurePlateTile pressurePlate in pressurePlateTiles)
        {
            if(!tilesToTrigger.Contains(pressurePlate)) 
            {
                pressurePlate.DePower();
                tilesToRemove.Add(pressurePlate);
            }
        }

        foreach (PressurePlateTile pressurePlateTile in tilesToRemove)
        {
            pressurePlateTiles.Remove(pressurePlateTile);
        }
        tilesToRemove.Clear();

        foreach (DelverzTile tileToTrigger in tilesToTrigger)
        {
            tileToTrigger.Trigger(this);

            //add any pressureplate tiles that are in tilesToTrigger to pressurePlateTiles
            if(tileToTrigger is PressurePlateTile && !pressurePlateTiles.Contains(tileToTrigger as PressurePlateTile)) { pressurePlateTiles.Add(tileToTrigger as PressurePlateTile); }
        }
    }

    public override void Trigger(DelverzTile incomingTile)
    {
        Die();
    }
    public override void Die()
    {
        //Disable Input
        myPlayerInputScript.Die(true);

        //Deduct Treasure
        treasureAmount -= (treasureAmount * 0.1f);
        Mathf.RoundToInt(treasureAmount);
        TreasureAmountDisplay = (int)treasureAmount;

        StartCoroutine(RespawnTimer());
    }

    public void UseAbility(Vector3 AbilityDirection)
    {
        AbilityManager.current.UseAbility(currentAbility, AbilityDirection, transform.position);
    }

    public bool canPickupAbility()
    {
        if (currentAbility == Ability.Null) { return false; }
        else return true;
    }
    public void PickupAbility(Ability abilityToPickup)
    {
        currentAbility = abilityToPickup;
    }

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(5f);

        myPlayerInputScript.Die(false);
    }

    public override void DestroySelf()
    {
        base.DestroySelf();
        Die();
    }
}
