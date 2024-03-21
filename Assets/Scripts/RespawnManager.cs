using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] private Transform respawnBoundary;
    private List<Bounds> respawnPoses = new List<Bounds>();
    private Bounds respawnBounds;

    public static RespawnManager current;

    private void Awake()
    {
        current = this;
    }
    public void AddRespawnPos(Bounds posToAdd)
    {
        respawnPoses.Add(posToAdd);
    }

    public void RemoveRespawnPos(Bounds posToRemove)
    {
        respawnPoses.Remove(posToRemove);
    }

    public Vector3 ReturnRespawnPos(PlayerTile playerToSpawn)
    {
        foreach (Bounds respawnPos in respawnPoses)
        {
            //if it's below this coordinate remove it from the list
            if (respawnPos.center.y < (transform.position.y + 0.5f)) { respawnPoses.Remove(respawnPos); }
        }

        List<Bounds> respawnPosesTemp = new List<Bounds>(); 

        foreach (Bounds respawnPos in respawnPoses)
        {
            //then if it's below this coordinate add it to this temporary list
            if (respawnPos.center.y < (respawnBoundary.position.y)) { respawnPosesTemp.Add(respawnPos); }
        }

        return ReturnSpawnPos2(respawnPosesTemp, playerToSpawn);
    }

    private Vector3 ReturnSpawnPos2(List<Bounds> respawnPosesTemp, PlayerTile playerToSpawn)
    {

        //then pick a random position from the list and check if it's populated
        Bounds randomBounds = respawnPosesTemp[Random.Range(0, respawnPosesTemp.Count - 1)];

        //if the position isn't populated return it as the respawn position
        TileIntersect newIntersect = GridManager.current.ReturnIntersectTiles(randomBounds, playerToSpawn);

        if (newIntersect.canTraverse = true) { }
    }

}
