using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasurePickup : DelverzTile
{
    [SerializeField] private int treasureAmount;

    public override void Trigger(DelverzTile incomingTile)
    {
        if(incomingTile is PlayerTile)
        {
            PlayerTile tileToTrigger = incomingTile as PlayerTile;
            tileToTrigger.PickupTreasure(treasureAmount);
        }
    }
}
