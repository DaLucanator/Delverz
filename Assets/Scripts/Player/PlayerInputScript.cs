using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    PlayerInput input;
    private InputAction move, fire;
    private Vector2Int moveDir, fireDir;
    private Vector2 moveDirFloat;
    private bool canMove = true, canFire = true, isDead;
    private float delayTime = 0.03125f;

    [SerializeField] private DelverzTile myPlayerTile;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();

        move = input.actions["Move"];
        fire = input.actions["Fire"];

        move.performed += MoveInput;
        move.canceled += MoveInput;

        fire.performed += FireInput;
        fire.canceled += FireInput;
    }

    void MoveInput(InputAction.CallbackContext context)
    {

        Vector2 moveDirTemp = context.ReadValue<Vector2>();

        moveDir.x = Mathf.RoundToInt(moveDirTemp.x);
        moveDir.y = Mathf.RoundToInt(moveDirTemp.y);
    }

    void FireInput(InputAction.CallbackContext context)
    {
        //fireDir = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
        // Fire();
    }

    private void Move()
    {
        if (canMove && moveDir != Vector2.zero && !isDead)
        {
            moveDirFloat = moveDir;
            moveDirFloat *= 0.125f;
            Vector3 movePos = new Vector3(transform.position.x + moveDirFloat.x, transform.position.y + moveDirFloat.y, -0.5f);

            Bounds moveBounds = new Bounds(movePos, Vector3.one);

            if(myPlayerTile.CanMove(moveBounds)) 
            {
                myPlayerTile.Move(movePos);

                canMove = false;
                StartCoroutine(MoveDelay());
            }
        }
    }

    /*private void Fire()
     {
         if (canFire && fireDir != Vector2.zero && currentItem != null)
         {
             canFire = false;

             currentItem.UseItem(fireDir);

             StartCoroutine(FireDelay());
         }
     }

     */


    private IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(delayTime);
        canMove = true;
    }

    private IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(delayTime);
        canFire = true;
    }

    public void Die( bool shouldKill)
    {
        isDead = shouldKill;
    }
}
