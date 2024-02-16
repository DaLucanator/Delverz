using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTile : DelverzTile
{
    [SerializeField] PlayerInputScript myPlayerInputScript;
    private float treasureAmount;
    private int TreasureAmountDisplay;

    public override void Move(Vector3 movePos)
    {
        GridManager.current.RemoveTileFromDictionary(tileLayer, bounds);

        transform.SetPositionAndRotation(movePos, Quaternion.identity);
        bounds = new Bounds(transform.position, Vector3.one * 0.96875f);
        GridManager.current.AddToTileDictionary(tileLayer, bounds, this);

        foreach (DelverzTile tileIAmTraversingTo in tilesToTraverseTo)
        {
            Debug.Log(tileIAmTraversingTo);
            tileIAmTraversingTo.Trigger(this);
        }
    }

    public override void Trigger(DelverzTile incomingTile)
    {
        Die();
    }
    public override void Die()
    {
        //Disable Input
        myPlayerInputScript.Die(true);

        //Deduct Treasure
        treasureAmount -= treasureAmount * 0.1f;
        Mathf.RoundToInt(treasureAmount);
        TreasureAmountDisplay = (int)treasureAmount;

        StartCoroutine(RespawnTimer());
    }

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(5f);

        myPlayerInputScript.Die(false);
    }

    public override void DestroySelf()
    {
        base.DestroySelf();
        Die();
    }
}
