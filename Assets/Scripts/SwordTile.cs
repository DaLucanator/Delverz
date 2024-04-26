using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTile : DelverzTile
{
    [SerializeField] private SpriteRenderer spriteNorth, spriteEast, spriteSouth, spriteWest;
    [SerializeField] private GameObject north, east, south, west;
    private GameObject myDirection;
    private SpriteRenderer currentSprite;
    private bool isSlashing;

    public Bounds ReturnBounds()
    {
        return bounds;
    }

    public bool ReturnIsSlashing()
    {
        return isSlashing;
    }

    public GameObject ReturnDirection()
    {
        return myDirection;
    }

    public void ChangeDirection(Vector3 spawnDirection)
    {
        spriteNorth.enabled = false;
        spriteEast.enabled = false;
        spriteSouth.enabled = false;
        spriteWest.enabled = false;

        if (spawnDirection == new Vector3(0, 1, 0)) { currentSprite = spriteNorth; myDirection = north; }
        else if (spawnDirection == new Vector3(1, 0, 0)) { currentSprite = spriteEast; myDirection = east; }
        else if (spawnDirection == new Vector3(0, -1, 0)) { currentSprite = spriteSouth; myDirection = south; }
        else if (spawnDirection == new Vector3(-1, 0, 0)) { currentSprite = spriteWest; myDirection = west; }

        myCollider = myDirection.GetComponent<BoxCollider2D>();
        bounds = myCollider.bounds;
        myCollider.enabled = false;
        bounds.center = myDirection.transform.position;
        myPlayer = transform.parent.GetComponent<PlayerTile>();
        myPlayer.SetSword(this);
        myPlayer.SetHasSword(true);

        //Populate tile in GridManager
        if (CanMove(bounds) && !isSlashing)
        {
            isSlashing = true;
            currentSprite.enabled = true;

            foreach (DelverzTile tileToTrigger in tilesToTrigger)
            {
                if (tileToTrigger is ProjectileTile)
                {
                    ProjectileTile tileToReflect = tileToTrigger as ProjectileTile;
                    tileToReflect.Reflect();
                }

                else { tileToTrigger.Die(); }
            }

            if (tilesToTrigger.Count > 0)
            {
                myPlayer.SetHasSword(false);
                GridManager.current.RemoveTileFromDictionary(tileLayer, bounds);
                myPlayer.SpendAbility();
            }

            else
            {
                GridManager.current.AddToTileDictionary(tileLayer, bounds, this);
            }

            StartCoroutine(Slash());
        }
    }

    private IEnumerator Slash()
    {
        yield return new WaitForSeconds(0.25f);
        myPlayer.SetHasSword(false);
        currentSprite.enabled = false;
        isSlashing = false;
        GridManager.current.RemoveTileFromDictionary(tileLayer, bounds);
    }

    public override void Move(Vector3 movePos)
    {
        foreach (DelverzTile tileToTrigger in tilesToTrigger)
        {
            if (tileToTrigger is ProjectileTile)
            {
                ProjectileTile tileToReflect = tileToTrigger as ProjectileTile;
                tileToReflect.Reflect();
            }

            else { tileToTrigger.Die(); }
        }

        if (tilesToTrigger.Count > 0) { DestroySelf(); }

        else
        {
            GridManager.current.RemoveTileFromDictionary(tileLayer, bounds);
            bounds = new Bounds(movePos, bounds.size);
            GridManager.current.AddToTileDictionary(tileLayer, bounds, this);
        }
    }

    public override void Die()
    {
        Debug.Log("boop");
        myPlayer.SetHasSword(false);
        myPlayer.SpendAbility();
        GridManager.current.RemoveTileFromDictionary(tileLayer, bounds);
    }

    public override void Trigger(PlayerTile incomingTile)
    {

        incomingTile.Die();
    }

    public override void DestroySelf()
    {
        myPlayer.SetHasSword(false);
        base.DestroySelf();
    }

    protected override void Start()
    {

    }
}
