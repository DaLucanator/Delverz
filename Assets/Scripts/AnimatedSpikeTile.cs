using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AnimatedSpikeTile : PoweredTile
{
    [SerializeField]private GameObject mySpikes;
    [SerializeField] private SpikeTile mySpikeTile;
    [SerializeField] private bool isPowered;
    private BoxCollider2D spikeCollider;
    private Bounds spikeBounds;
    private bool addToDictionary;

    protected override void Start()
    {
        base.Start();
        if(!isNetworkedTile)
        {
            if (isPowered) { TrapClock.current.onTick += PowerTile; }

            if (!isPowered) { TrapClock.current.offTick += PowerTile; }
        }

        spikeCollider = mySpikes.GetComponent<BoxCollider2D>();
        spikeBounds = spikeCollider.bounds;
        spikeBounds.center = transform.position;
        spikeCollider.enabled = false;
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
        if (shouldPower && !isPowered)
        {
            isPowered = true;
            mySpikes.SetActive(true);   
            tilesToTrigger = null;
            if (addToDictionary) { GridManager.current.AddToTileDictionary(1, spikeBounds, mySpikeTile); }
            TileIntersect intersectData = GridManager.current.ReturnIntersectTiles(spikeBounds, mySpikeTile);
            tilesToTrigger = intersectData.tilesToTrigger;

            foreach (DelverzTile tile in intersectData.tilesToTrigger)
            {
                tile.Die();
            }
        }

        else if (!shouldPower && isPowered)
        {
            isPowered = false;
            GridManager.current.RemoveTileFromDictionary(1, spikeBounds);
            mySpikes.SetActive(false);
            addToDictionary = true;
        }
    }
}
