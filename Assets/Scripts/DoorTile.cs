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


    public override void PowerTile()
    {
        isOpen = !isOpen;

        //close the door
        if (isOpen)
        {
            myDoor.SetActive(true);
            tilesToTrigger = null;
            if (addToDictionary) 
            { 
                GridManager.current.AddToTileDictionary(1, bounds, mySpikeTile);
                GridManager.current.AddToTileDictionary(3, bounds, myWallTile);
            }
            TileIntersect intersectData = GridManager.current.ReturnIntersectTiles(bounds, mySpikeTile);
            tilesToTrigger = intersectData.tilesToTrigger;

            foreach (DelverzTile tile in intersectData.tilesToTrigger)
            {
                tile.Die();
            }
        }

        //open the door
        else if (!isOpen)
        {
            GridManager.current.RemoveTileFromDictionary(1, bounds);
            GridManager.current.RemoveTileFromDictionary(3, bounds);
            myDoor.SetActive(false);
            addToDictionary = true;
        }
    }
}
