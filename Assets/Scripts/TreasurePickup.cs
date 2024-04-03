using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasurePickup : DelverzTile
{
    [SerializeField] private int treasureAmount;

    public override void Trigger(PlayerTile incomingTile)
    {
        incomingTile.PickupTreasure(treasureAmount);
    }
}
