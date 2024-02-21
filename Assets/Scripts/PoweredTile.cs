using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredTile : DelverzTile
{
    [SerializeField] protected bool isNetworkedTile;

    protected override void Start()
    {
        base.Start();
        if (!isNetworkedTile) { TrapClock.current.tick += PowerTile; }

    }

    public virtual void PowerTile()
    {

    }

    public virtual void PowerTile(bool shouldPower)
    {

    }
}
