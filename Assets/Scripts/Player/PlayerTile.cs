using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTile : DelverzTile
{
    [SerializeField] PlayerInputScript myPlayerInputScript;
    private float treasureAmount;
    private float currentSpeed  = 1f;
    private int treasureAmountDisplay;
    private bool isDead, hasSword, isInvincible;
    private Ability currentAbility = Ability.Null;
    private Ability innateAbility = Ability.Null;
    private Ability pickupAbility = Ability.Null;
    private PlayerColour playerColour;
    [SerializeField] private GameObject bloodSplat;
    [SerializeField] private GameObject respawnYellow, respawnBlue, respawnRed, respawnGreen;
    [SerializeField] private SpriteRenderer mySprite, myInvinciblitySprite;
    [SerializeField] private AudioSource deathSplat;


    private List<PressurePlateTile> pressurePlateTiles = new List<PressurePlateTile>();

    public Bounds ReturnBounds()
    {
        return bounds;
    }

    public bool ReturnHasSword()
    {
        return hasSword;
    }

    public float ReturnCurrentSpeed()
    {
        return currentSpeed;
    }

    public bool ReturnIsInvincible()
    {
        return isInvincible;
    }

    public void SetHasSword(bool shouldHaveSword)
    {
        hasSword = shouldHaveSword;
    }

    public void SetSword(SwordTile swordToSet)
    {
        mySword = swordToSet;
    }

    public void SetColour(PlayerColour colourToSet)
    {
        playerColour = colourToSet;
    }

    public void PickupTreasure(int treasureToAdd)
    {
        treasureAmount += treasureToAdd;
        Mathf.RoundToInt(treasureAmount);
        treasureAmountDisplay = (int)treasureAmount;

        UIControllerParent.current.SetScore(playerColour, treasureAmountDisplay);
    }

    public void DeductTreasure()
    {
        treasureAmount -= (treasureAmount * 0.1f);
        Mathf.RoundToInt(treasureAmount);
        treasureAmountDisplay = (int)treasureAmount;

        UIControllerParent.current.SetScore(playerColour, treasureAmountDisplay);
    }

    //the rest of the movement is handled by PlayerInputScript
    public override void Move(Vector3 movePos)
    {
        GridManager.current.RemoveTileFromDictionary(tileLayer, bounds);

        transform.SetPositionAndRotation(movePos, Quaternion.identity);
        bounds = new Bounds(transform.position, bounds.size);
        GridManager.current.AddToTileDictionary(tileLayer, bounds, this);

        List<PressurePlateTile> tilesToRemove = new List<PressurePlateTile>();

        //depower any pressure plate tiles that aren't in tilesToTrigger anymore and remove them from that list
        foreach (PressurePlateTile pressurePlate in pressurePlateTiles)
        {

            if(!tilesToTrigger.Contains(pressurePlate)) 
            {
                Debug.Log("aaa");
                if (pressurePlate != null) { pressurePlate.DePower(); }
                tilesToRemove.Add(pressurePlate);
            }
        }

        foreach (PressurePlateTile pressurePlateTile in tilesToRemove)
        {
            pressurePlateTiles.Remove(pressurePlateTile);
        }
        tilesToRemove.Clear();

        //for some reason player trigggers tiles after it moves. Everything else in the game triggers tiles before it moves. idk I guess it makes sense. It hurts my head so I didn't change it.
        foreach (DelverzTile tileToTrigger in tilesToTrigger)
        {
            //other tiles will be more explicit about what methods they want tiles to call here (often DelverzTile.Die())
            //Player has lots of unique interactions with tiles that other tiles don't (ability pickups etc.). So it fetches what to do from other tiles instead via DelverzTile.Trigger()
            //the syntax of tiletotrigger is because the player was the first tile made that moves.
            tileToTrigger.Trigger(this);

            //add any pressureplate tiles that are in tilesToTrigger to pressurePlateTiles
            if(tileToTrigger is PressurePlateTile && !pressurePlateTiles.Contains(tileToTrigger as PressurePlateTile)) { pressurePlateTiles.Add(tileToTrigger as PressurePlateTile); }
        }
    }

    //This is for debugging. It shouldn't happen.
    public override void Trigger(PlayerTile incomingTile)
    {
        Debug.Log("player was triggered by" + incomingTile.name);
    }

    public override void Die()
    {
        if(!isDead && !isInvincible)
        {
            myPlayerInputScript.EnableSprite(false);
            Instantiate(bloodSplat, transform.position, Quaternion.identity);
            GridManager.current.RemoveTileFromDictionary(tileLayer, bounds);
            isDead = true;

            //Disable Input
            myPlayerInputScript.Die(true);
            //Deduct Treasure
            DeductTreasure();
            //Death Sound
            if (SoundManager.current.CanPlaySound(SoundToPlay.deathSplat)) { deathSplat.Play(); }

            StartCoroutine(RespawnTimer(5f));
        }
    }

    public void UseAbility(Vector3 AbilityDirection)
    {
        AbilityManager.current.UseAbility(currentAbility, AbilityDirection, this);
    }

    public void SetInnateAbility(Ability abilityToSet)
    {
        innateAbility = abilityToSet;
    }

    public bool canPickupAbility()
    {
        if (currentAbility == Ability.Null) { return true; }
        else return false;
    }
    public void PickupAbility(AbilityScriptableObject abilityToPickup)
    {
        currentAbility = abilityToPickup.ReturnAbility();
        UIControllerParent.current.PickupABility(playerColour, abilityToPickup.ReturnSprite());
        Debug.Log("you picked up " + currentAbility.ToString());
    }

    public void SpendAbility()
    {
        //if(currentAbility == pickupAbility) { currentAbility = Ability.Null; }
        //if the current ability is innateability start the timer
        UIControllerParent.current.PickupABility(playerColour, null);
        currentAbility = Ability.Null;
    }

    private IEnumerator RespawnTimer(float timeToWait)
    {
        mySprite.enabled = false;
        yield return new WaitForSeconds(5f);

        Vector3 positionToRespawn = RespawnManager.current.ReturnRespawnPos(this);

        if (positionToRespawn != new Vector3 (0,0, -1000)) 
        {
            isInvincible = true;
            Instantiate(ReturnRespawn(), positionToRespawn, Quaternion.identity);
            yield return new WaitForSeconds(0.66f);
            StartCoroutine(InvincibilityTimer(3f));

            mySprite.enabled = true;
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

    private GameObject ReturnRespawn()
    {
        if (playerColour == PlayerColour.yellow) { return respawnYellow; }
        else if (playerColour == PlayerColour.blue) { return respawnBlue; }
        else if (playerColour == PlayerColour.red) { return respawnRed; }
        else if (playerColour == PlayerColour.green) { return respawnGreen; }

        else return null;
    }

    private IEnumerator InvincibilityTimer( float timetoInvincibilise)
    {
        StopCoroutine(InvincibilityTimer(timetoInvincibilise));
        isInvincible = true;
        myInvinciblitySprite.enabled = true;
        yield return new WaitForSeconds(timetoInvincibilise);

        myInvinciblitySprite.enabled = false;
        isInvincible = false;

    }

    public IEnumerator InvisibilityPotion()
    {
        StopCoroutine(InvisibilityPotion());
        //go invisible
        mySprite.enabled = false;
        yield return new WaitForSeconds(10f);
        //stop going Invisible
        mySprite.enabled = true;
    }

    public IEnumerator SpeedPotion()
    {
        StopCoroutine(SpeedPotion());
        currentSpeed = 2f;

        yield return new WaitForSeconds(10f);
        currentSpeed = 1f;
    }

    public override void DestroySelf()
    {
        if (!isDead)
        {
            Die();
        }
    }
}
