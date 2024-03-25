using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController current;
    private Camera mainCamera;

    private float spawnOffset = 15f;

    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private float delayTime;
    [SerializeField] private RoomManager roomManager;
    [SerializeField] private Transform boundaryY1, boundaryY2, boundaryX1, boundaryX2;
    bool canMove = true;

    //move the camera
    //if the camera has progressed this much spawn another room


    private void Awake()
    {
        current = this;
    }

    public bool ReturnIsOffScreen(Vector3 vectorToCheck)
    {
        if(vectorToCheck.x < boundaryX1.position.x) { return true; }
        if (vectorToCheck.x > boundaryX2.position.x) { return true; }
        if (vectorToCheck.y < boundaryY2.position.y) { return true; }

        else return false;
    } 

    private void FixedUpdate()
    {
        if (canMove)
        {
            transform.SetPositionAndRotation(transform.position + moveDirection, Quaternion.identity);
            StartCoroutine(MoveDelay());
            canMove = false;

            if(transform.position.y >= spawnOffset)
            {
                roomManager.SpawnRoom();
                spawnOffset += 15f;
            }
        }
    }

    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(delayTime);
        canMove = true;
    }


}
