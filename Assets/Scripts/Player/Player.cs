using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerInputScript myPlayerInputScript;
    private float treasureAmount;
    private int TreasureAmountDisplay;

    public void Die()
    {
        //Disable Input
        myPlayerInputScript.Die(true);

        //Deduct Treasure
        treasureAmount -= treasureAmount * 0.1f;
        Mathf.RoundToInt(treasureAmount);
        TreasureAmountDisplay = (int) treasureAmount;

        StartCoroutine(RespawnTimer());
    }

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(5f);

        myPlayerInputScript.Die(false);
    }
}
