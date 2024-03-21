using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInputScript : MonoBehaviour
{
    PlayerInput input;
    private InputAction move, fire;
    private Vector2Int moveDir;
    private Vector2 fireDir;
    private bool canMove = true, canFire = true, isDead;
    private float delayTime = 0.03125f;
    private PlayerColour playerColour;
    private Joystick myJoystick;


    PlayerInputManager inputManager;

    [SerializeField] private PlayerTile myPlayerTile;
    [SerializeField] private GameObject yellowSprite, blueSprite, redSprite, greenSprite;
    [SerializeField] private TextMeshProUGUI joystickName;

    private enum PlayerColour
    {
        yellow,
        blue,
        red,
        green
    }

    private void Awake()
    {
        inputManager = PlayerInputManager.instance;

        if (inputManager.playerCount == 1)
        {
            playerColour = PlayerColour.yellow;
            yellowSprite.SetActive(true);
        }
        if (inputManager.playerCount == 2)
        {
            playerColour = PlayerColour.blue;
            blueSprite.SetActive(true);
        }
        if (inputManager.playerCount == 3)
        {
            playerColour = PlayerColour.red;
            redSprite.SetActive(true);
        }
        if (inputManager.playerCount == 4)
        {
            playerColour = PlayerColour.green;
            greenSprite.SetActive(true);
        }
        input = GetComponent<PlayerInput>();

        move = input.actions["Move"];
        fire = input.actions["Fire"];

        move.performed += MoveInput;
        move.canceled += MoveInput;

        fire.performed += FireInput;
        fire.canceled += FireCancel;
    }

    void MoveInput(InputAction.CallbackContext context)
    {
        Vector2 moveDirTemp = context.ReadValue<Vector2>();

        moveDir.x = Mathf.RoundToInt(moveDirTemp.x);
        moveDir.y = Mathf.RoundToInt(moveDirTemp.y);
    }

    void FireInput(InputAction.CallbackContext context)
    {
        fireDir = context.ReadValue<Vector2>();

        Fire();
    }

    void FireCancel(InputAction.CallbackContext context)
    {
        canFire = true;
    }

    private void FixedUpdate()
    {
        Move();
    }

    //Movement
    private void Move()
    {
        if (canMove && moveDir != Vector2.zero && !isDead)
        {

            Vector2 moveDirFloat = moveDir;
            moveDirFloat *= 0.125f;
            Vector3 movePos = new Vector3(transform.position.x + moveDirFloat.x, transform.position.y + moveDirFloat.y, 0f);

            Bounds moveBounds = new Bounds(movePos, Vector3.one * 0.96875f);

            if (myPlayerTile.CanMove(moveBounds))
            {
                myPlayerTile.Move(movePos);

                canMove = false;
                StartCoroutine(MoveDelay());
            }
        }
    }

    //Abilities
    private void Fire()
    {
        if (!isDead && canFire && !myPlayerTile.canPickupAbility())
        {
            canFire = false;
            Vector3 abilityDirection = new Vector3(fireDir.x, fireDir.y, 0f);
            myPlayerTile.UseAbility(abilityDirection);

            fireDir = Vector2.zero;
        }
    }

    private void ChangeAnimationDirection()
    {
        //if statement for each direction
        //change animation
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
