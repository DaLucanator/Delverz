using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTile : PoweredTile
{
    [SerializeField]private bool isOpen;
    [SerializeField] private GameObject myDoor;
    [SerializeField] private SpikeTile mySpikeTile;
    [SerializeField] private DelverzTile myWallTile;
    private bool addToDictionary;
    private BoxCollider2D doorCollider;
    [SerializeField] private Bounds doorBounds;
    bool shouldBruteForce;

    protected override void Start()
    {
        base.Start();
        if (!isNetworkedTile)
        {
            if (!isOpen) { TrapClock.current.onTick += PowerTile; }

            if (isOpen) { TrapClock.current.offTick += PowerTile; }
        }

        doorBounds.center = transform.position;
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
        //brute forcing because of a stupid bug
        if(shouldBruteForce)
        {
            GridManager.current.RemoveTileFromDictionary(1, new Bounds(transform.position, Vector3.zero));
            GridManager.current.RemoveTileFromDictionary(3, new Bounds(transform.position, Vector3.zero));
            shouldBruteForce = false;
        }


        if (!shouldOpen && isOpen)
        {
            isOpen = false;
            myDoor.SetActive(true);

            tilesToTrigger = null;
            Debug.Log(doorBounds);
            GridManager.current.AddToTileDictionary(1, doorBounds, mySpikeTile);

            TileIntersect intersectData = GridManager.current.ReturnIntersectTiles(doorBounds, mySpikeTile);
            tilesToTrigger = intersectData.tilesToTrigger;

            foreach (DelverzTile tile in intersectData.tilesToTrigger)
            {
                tile.Die();
            }

            GridManager.current.RemoveTileFromDictionary(1, doorBounds);
            GridManager.current.AddToTileDictionary(3, doorBounds, myWallTile);
        }

        //open the door
        else if (shouldOpen && !isOpen)
        {
            isOpen = true;
            GridManager.current.RemoveTileFromDictionary(1, doorBounds);
            GridManager.current.RemoveTileFromDictionary(3, doorBounds);
            myDoor.SetActive(false);

            addToDictionary = true;
        }
    }
}
