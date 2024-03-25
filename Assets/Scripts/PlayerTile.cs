using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTile : DelverzTile
{
    [SerializeField] PlayerInputScript myPlayerInputScript;
    private float treasureAmount;
    private int TreasureAmountDisplay;
    private bool isDead;
    private Ability currentAbility = Ability.Null;
    [SerializeField]private GameObject bloodSplat;

    private List<PressurePlateTile> pressurePlateTiles = new List<PressurePlateTile>();

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, bounds.size);
    }

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
        if (!isDead)
        {
            Die();
        }
    }
    public override void Die()
    {
        myPlayerInputScript.EnableSprite(false);
        Instantiate(bloodSplat, transform.position, Quaternion.identity);
        GridManager.current.RemoveTileFromDictionary(tileLayer, bounds);
        isDead = true;
        //Disable Input
        myPlayerInputScript.Die(true);

        //Deduct Treasure
        treasureAmount -= (treasureAmount * 0.1f);
        Mathf.RoundToInt(treasureAmount);
        TreasureAmountDisplay = (int)treasureAmount;

        StartCoroutine(RespawnTimer(5f));
    }

    public void UseAbility(Vector3 AbilityDirection)
    {
        AbilityManager.current.UseAbility(currentAbility, AbilityDirection, this);
    }

    public bool canPickupAbility()
    {
        if (currentAbility == Ability.Null) { return true; }
        else return false;
    }
    public void PickupAbility(Ability abilityToPickup)
    {
        currentAbility = abilityToPickup;
        Debug.Log("you picked up " + currentAbility.ToString());
    }

    public void SpendAbility()
    {
        currentAbility = Ability.Null;
    }

    private IEnumerator RespawnTimer(float timeToWait)
    {

        yield return new WaitForSeconds(5f);

        Vector3 positionToRespawn = RespawnManager.current.ReturnRespawnPos(this);

        if (positionToRespawn != new Vector3 (0,0, -1000)) 
        {
            myPlayerInputScript.EnableSprite(true);
            transform.SetPositionAndRotation(positionToRespawn, Quaternion.identity);
            bounds = new Bounds(transform.position, Vector3.one * 0.96875f);
            GridManager.current.AddToTileDictionary(tileLayer, bounds, this);
            myPlayerInputScript.Die(false);
            isDead = false;
        }

        else
        {
            RespawnTimer(0.5f);
        }
    }

    public override void DestroySelf()
    {
        if (!isDead)
        {
            Die();
        }
    }
}
