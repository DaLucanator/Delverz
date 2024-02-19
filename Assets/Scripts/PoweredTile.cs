using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredTile : DelverzTile
{
    private protected bool isPowered;

    protected override void Start()
    {
        base.Start();
        TrapClock.current.tick += PowerTile;
        TrapClock.current.onOfftick += PowerTile;
    }

    public virtual void PowerTile()
    {

    }

    public virtual void PowerTile(bool shouldPower)
    {

    }
}
