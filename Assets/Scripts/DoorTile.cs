using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTile : PoweredTile
{
    [SerializeField]private bool isOpen;
    [SerializeField] private GameObject myWall, mySpikes;
    [SerializeField] private SpikeTile mySpikeTile;
    [SerializeField] private DelverzTile myWallTile;
    bool shouldRemove = true;

    protected override void Start()
    {
        base.Start();
        if (!isNetworkedTile)
        {
            if (!isOpen) { TrapClock.current.onTick += PowerTile; }

            if (isOpen) { TrapClock.current.offTick += PowerTile; }
        }
    }

    public override void DestroySelf()
    {
        if (!isNetworkedTile)
        {
            if (isOpen) { TrapClock.current.onTick += PowerTile; }

            if (!isOpen) { TrapClock.current.offTick += PowerTile; }
        }
        base.DestroySelf();
    }

    public bool ReturnIsPowered()
    {
        return isOpen;
    }

    public override void PowerTile(bool shouldOpen)
    {
        if (!shouldOpen && isOpen)
        {
            isOpen = false;
            myWall.SetActive(true);
            GridManager.current.RemoveTileFromDictionary(4, new Bounds(transform.position, Vector3.zero));
            mySpikes.SetActive(true);
            GridManager.current.RemoveTileFromDictionary(1, new Bounds(transform.position, Vector3.zero));

            tilesToTrigger = null;
            Debug.Log(bounds);
            GridManager.current.AddToTileDictionary(1, bounds, mySpikeTile);

            TileIntersect intersectData = GridManager.current.ReturnIntersectTiles(bounds, mySpikeTile);
            tilesToTrigger = intersectData.tilesToTrigger;

            foreach (DelverzTile tile in intersectData.tilesToTrigger)
            {
                tile.Die();
            }

            GridManager.current.RemoveTileFromDictionary(1, bounds);
            GridManager.current.AddToTileDictionary(4, bounds, myWallTile);
        }

        //open the door
        else if (shouldOpen && !isOpen)
        {
            GridManager.current.RemoveTileFromDictionary(1, bounds);
            GridManager.current.RemoveTileFromDictionary(4, bounds);
            mySpikes.SetActive(false);
            myWall.SetActive(false);
            isOpen = true;
        }
    }
}
