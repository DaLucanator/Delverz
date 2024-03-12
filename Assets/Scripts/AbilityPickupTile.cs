using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickupTile : DelverzTile
{
    public override void Trigger(DelverzTile incomingTile)
    {
       if(incomingTile is PlayerTile)
        {
            PlayerTile currentPlayerTile = incomingTile as PlayerTile;
            currentPlayerTile.
        }
    }
}
