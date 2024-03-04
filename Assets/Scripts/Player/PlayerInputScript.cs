using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInputScript : MonoBehaviour
{
    PlayerInput input;
    private InputAction move, fire;
    private Vector2Int moveDir, fireDir;
    private Vector2 moveDirFloat;
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

    private void MapJoystickControls()
    {
        if (Joystick.current != null) 
        {
            myJoystick = Joystick.current;
            joystickName.text = myJoystick.name; 
        }
    }

    private void Awake()
    {
        inputManager = PlayerInputManager.instance;
        MapJoystickControls();
        //if(Joystick.current != null) { joystickName.text = "Joystick is connected!"; }



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
