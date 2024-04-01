using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTile : DelverzTile
{
    [SerializeField] private SpriteRenderer spriteNorth, spriteEast, spriteSouth, spriteWest;
    [SerializeField] private GameObject north, east, south, west;
    private GameObject myDirection;
    private bool canMove;


    public void ChangeDirection(Vector3 spawnDirection)
    {
        spriteNorth.enabled = false;
        spriteEast.enabled = false;
        spriteSouth.enabled = false;
        spriteWest.enabled = false;

        if (spawnDirection == new Vector3(0, 1, 0)) { spriteNorth.enabled = true; myDirection = north; }
        else if (spawnDirection == new Vector3(1, 0, 0)) { spriteEast.enabled = true; myDirection = east; }
        else if (spawnDirection == new Vector3(0, -1, 0)) { spriteSouth.enabled = true; myDirection = south; }
        else if (spawnDirection == new Vector3(-1, 0, 0)) { spriteWest.enabled = true; myDirection = west; }

        myCollider = myDirection.GetComponent<BoxCollider2D>();
        bounds = myCollider.bounds;
        myCollider.enabled = false;
        bounds.center = myDirection.transform.position;

        //Populate tile in GridManager
        if (CanMove(bounds))
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
                GridManager.current.AddToTileDictionary(tileLayer, bounds, this);
                canMove = true;
            }
        }
    }

    public override void Trigger(PlayerTile incomingTile)
    {
        incomingTile.Die();
    }

    public Bounds ReturnBounds()
    {
        return bounds;
    }


    protected override void Start()
    {

    }
}
