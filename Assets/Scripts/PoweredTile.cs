using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredTile : DelverzTile
{
    [SerializeField] protected bool isNetworkedTile;

    public override void Start()
    {
        base.Start();
        Debug.Log("Base netowrk tile");
        if (!isNetworkedTile) { TrapClock.current.tick += PowerTile; }

    }

    public virtual void PowerTile()
    {

    }

    public virtual void PowerTile(bool shouldPower)
    {

    }
}
