using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickupTile : DelverzTile
{
    [SerializeField] AbilityScriptableObject abilityToPickup;
    [SerializeField] SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        spriteRenderer.sprite = abilityToPickup.ReturnSprite();
        base.Start();
    }

    public override void Trigger(DelverzTile incomingTile)
    {
       if(incomingTile is PlayerTile)
        {
            PlayerTile currentPlayerTile = incomingTile as PlayerTile;

            if(currentPlayerTile.canPickupAbility())
            {
                currentPlayerTile.PickupAbility(abilityToPickup.ReturnAbility());
                spriteRenderer.sprite = null;
                DestroySelf();
            }
        }
    }


}
