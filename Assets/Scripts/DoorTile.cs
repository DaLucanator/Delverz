using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTile : PoweredTile
{
    [SerializeField]private bool isOpen;
    [SerializeField] private SpawnableSpikeTile mySpikeTile;
    [SerializeField] private SpawnableWallTile myWallTile;
    [SerializeField] private SpriteRenderer mySpriteRenderer;

    protected override void Start()
    {
        base.Start();
        if (!isNetworkedTile)
        {
            if (!isOpen) { TrapClock.current.onTick += PowerTile; }

            if (isOpen) { TrapClock.current.offTick += PowerTile; }
        }

        if (isOpen)
        {
            mySpriteRenderer.enabled = false;

            //I should tell the tiles to depopulate here but I don't want it to happen before their start functions so I'll ignore it and just not put any doors in the first room
        }
    }

    public override void DestroySelf()
    {
        if (!isNetworkedTile)
        {
            if (isOpen) { TrapClock.current.onTick += PowerTile; }

            if (!isOpen) { TrapClock.current.offTick += PowerTile; }
        }
    }

    public bool ReturnIsPowered()
    {
        return isOpen;
    }

    public override void PowerTile(bool shouldOpen)
    {
        //close the door
        if (!shouldOpen && isOpen)
        {
            isOpen = false;
            mySpriteRenderer.enabled = true;

            mySpikeTile.PopulateTile();
            myWallTile.PopulateTile();
        }

        //open the door
        else if (shouldOpen && !isOpen)
        {
            mySpikeTile.DePopulateTile();
            myWallTile.DePopulateTile();

            mySpriteRenderer.enabled = false;
            isOpen = true;
        }
    }
}
