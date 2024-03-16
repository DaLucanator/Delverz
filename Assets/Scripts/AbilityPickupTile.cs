using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickupTile : DelverzTile
{
    AbilityScriptableObject abilityToPickup;
    SpriteRenderer spriteRenderer;

    public override void Trigger(DelverzTile incomingTile)
    {
       if(incomingTile is PlayerTile)
        {
            PlayerTile currentPlayerTile = incomingTile as PlayerTile;

            if(currentPlayerTile.canPickupAbility())
            {
                currentPlayerTile.PickupAbility(abilityToPickup.ReturnAbility());
                DestroySelf();
            }
        }
    }


}
