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
    bool canMove = true;

    //move the camera
    //if the camera has progressed this much spawn another room


    private void Awake()
    {
        current = this;
    }

    public Camera ReturnMainCamera()
    {
        return mainCamera;
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
                spawnOffset += spawnOffset;
            }
        }
    }

    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(delayTime);
        canMove = true;
    }


}
