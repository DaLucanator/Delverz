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

    public override void Trigger(PlayerTile incomingTile)
    {
        if (incomingTile.canPickupAbility())
        {
            incomingTile.PickupAbility(abilityToPickup);
            spriteRenderer.sprite = null;
            DestroySelf();
        }
    }


}
