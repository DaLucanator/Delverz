using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private float delayTime;
    bool canMove = true;

    private void FixedUpdate()
    {
        if(canMove) 
        {
            transform.SetPositionAndRotation(transform.position + moveDirection, Quaternion.identity);
            StartCoroutine(MoveDelay());
            canMove = false;
        }

        IEnumerator MoveDelay()
        {
            yield return new WaitForSeconds(delayTime);
            canMove = true;
        }
    }
}
