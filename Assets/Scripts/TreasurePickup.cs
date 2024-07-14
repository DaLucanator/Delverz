using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasurePickup : DelverzTile
{
    [SerializeField] private int treasureAmount;
    [SerializeField] SpriteRenderer mySprite;
    bool canPickup = true;


    public override void Trigger(PlayerTile incomingTile)
    {
        if (!canPickup) return;

        else
        {
            canPickup = false;
            incomingTile.PickupTreasure(treasureAmount);
            mySprite.sprite = null;
            //DestroySelf();
        }
    }
}
