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

    private bool addToDictionary;

    protected override void Start()
    {
        base.Start();
        if(!isNetworkedTile)
        {
            if (isPowered) { TrapClock.current.onTick += PowerTile; }

            if (!isPowered) { TrapClock.current.offTick += PowerTile; }
        }
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
            if (addToDictionary) { GridManager.current.AddToTileDictionary(1, bounds, mySpikeTile); }
            TileIntersect intersectData = GridManager.current.ReturnIntersectTiles(bounds, mySpikeTile);
            tilesToTrigger = intersectData.tilesToTrigger;

            foreach (DelverzTile tile in intersectData.tilesToTrigger)
            {
                tile.Die();
            }
        }

        else if (!shouldPower && isPowered)
        {
            isPowered = false;
            GridManager.current.RemoveTileFromDictionary(1, bounds);
            mySpikes.SetActive(false);
            addToDictionary = true;
        }
    }
}
