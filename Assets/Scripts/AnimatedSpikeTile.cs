using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class AnimatedSpikeTile : PoweredTile
{
    [SerializeField] private bool isPowered;
    [SerializeField] private SpawnableSpikeTile mySpikeTile;
    [SerializeField] private SpriteRenderer mySpriteRenderer;

    protected override void Start()
    {
        base.Start();
        if(!isNetworkedTile)
        {
            if (!isPowered) { TrapClock.current.onTick += PowerTile; }

            if (isPowered) { TrapClock.current.offTick += PowerTile; }
        }

        if (!isPowered)
        {
            if (mySpriteRenderer != null) { mySpriteRenderer.enabled = false; }

            //I should tell the tiles to depopulate here but I don't want it to happen before their start functions so I'll ignore it and just not put any AnimatedSpikes in the first room
        }
    }

    public override void DestroySelf()
    {
        if (!isNetworkedTile)
        {
            if (isPowered) { TrapClock.current.onTick += PowerTile; }

            if (!isPowered) { TrapClock.current.offTick += PowerTile; }
        }
        base.DestroySelf();
    }

    public bool ReturnIsPowered()
    {
        return isPowered;
    }

    public override void PowerTile(bool shouldPower)
    {
        //power the spikes
        if (shouldPower && !isPowered)
        {
            isPowered = true;
            if (mySpriteRenderer != null) { mySpriteRenderer.enabled = true; } 

            mySpikeTile.PopulateTile();
        }

        //Depower the spikes
        else if (!shouldPower && isPowered)
        {
            mySpikeTile.DePopulateTile();

            if (mySpriteRenderer != null) { mySpriteRenderer.enabled = false; }
            isPowered = false;
        }
    }
}
