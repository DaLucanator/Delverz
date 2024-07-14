using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PoweredTile : DelverzTile
{
    [SerializeField] protected bool isNetworkedTile;
    [SerializeField] private List<SpriteRenderer> myColors = new List<SpriteRenderer>();
    protected AudioSource mySound;

    protected override void Start()
    {
        base.Start();
        if (!isNetworkedTile) { TrapClock.current.tick += PowerTile; }
        mySound = gameObject.GetComponent<AudioSource>();
    }

    public virtual void PowerTile()
    {

    }

    public override void DestroySelf()
    {
        if (!isNetworkedTile) { TrapClock.current.tick -= PowerTile; }
        base.DestroySelf();
    }

    public virtual void PowerTile(bool shouldPower)
    {

    }

    public void SetColor(int colorToSet)
    {
        myColors[colorToSet].enabled = true;
    }
}
