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
    private bool canMove = true, canFire = true, isDead, joined, ready;
    private float delayTime = 0.03125f, rotOffset;
    private SwordTile mySwordTile;

    PlayerInputManager inputManager;

    [SerializeField] private PlayerTile myPlayerTile;
    [SerializeField] private PlayerVisualHandler myPlayerVisualHandler;
    [SerializeField] private float rotOffset1, rotOffset2;

    private void Awake()
    {
        inputManager = PlayerInputManager.instance;

        if (inputManager.playerCount == 1)
        {
            myPlayerVisualHandler.SetColour(PlayerColour.yellow);
            myPlayerTile.SetColour(PlayerColour.yellow);
            myPlayerVisualHandler.SetRoleExplicit(0);
            rotOffset = rotOffset1;
        }
        if (inputManager.playerCount == 2)
        {
            myPlayerVisualHandler.SetColour(PlayerColour.blue);
            myPlayerVisualHandler.SetRoleExplicit(1);
        }
        if (inputManager.playerCount == 3)
        {
            myPlayerVisualHandler.SetColour(PlayerColour.red);
            myPlayerVisualHandler.SetRoleExplicit(2);
        }
        if (inputManager.playerCount == 4)
        {
            myPlayerVisualHandler.SetColour(PlayerColour.green);
            myPlayerVisualHandler.SetRoleExplicit(3);
            rotOffset = rotOffset2;
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
        Vector3 moveDirTemp = context.ReadValue<Vector2>();

        moveDirTemp = Quaternion.AngleAxis(rotOffset, Vector3.forward) * moveDirTemp;

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
        myPlayerVisualHandler.CheckAnimState(moveDir);
    }


    //Movement
    private void Move()
    {
        //Character Select

        //If I'm in the specific part of the character select where you change character (I'm not being asked to join and I haven't readied up)
        if(canMove && moveDir != Vector2.zero && SceneController.current.IsCharacterSelectScene() && joined && !ready)
        {
            //If I press Right
            if (moveDir.x > 0) 
            {
                //iterate to next portrait and ability text
                myPlayerVisualHandler.SetRole(true);

                canMove = false;
                StartCoroutine(MoveDelay());
            }

            //If I press left
            if (moveDir.x > 0)
            {
                //iterate to previous portrait and ability text
                myPlayerVisualHandler.SetRole(false);

                canMove = false;
                StartCoroutine(MoveDelay());
            }
        }

        //Movement
        if (canMove && moveDir != Vector2.zero && !isDead && SceneController.current.IsMainScene())
        {
            Vector2 moveDirFloat = moveDir;
            moveDirFloat *= 0.125f * myPlayerTile.ReturnCurrentSpeed();
            Vector3 movePos = new Vector3(transform.position.x + moveDirFloat.x, transform.position.y + moveDirFloat.y, 0f);

            Bounds moveBounds = new Bounds(movePos, myPlayerTile.ReturnBounds().size);

            //Sword Stuff
            if (myPlayerTile.ReturnHasSword())
            {
                if (mySwordTile == null) { mySwordTile = myPlayerTile.ReturnSword(); }
                if (mySwordTile.ReturnIsSlashing())
                {
                    Vector3 swordPos = mySwordTile.ReturnDirection().transform.position;
                    Vector3 swordMovePos = new Vector3(swordPos.x + moveDirFloat.x, swordPos.y + moveDirFloat.y, 0f);
                    Bounds swordMoveBounds = new Bounds(swordMovePos, mySwordTile.ReturnBounds().size);

                    if (mySwordTile.CanMove(swordMoveBounds) && myPlayerTile.CanMove(moveBounds))
                    {
                        mySwordTile.Move(swordMovePos);
                        myPlayerTile.Move(movePos);

                        canMove = false;
                        StartCoroutine(MoveDelay());
                    }
                }

                else if (myPlayerTile.CanMove(moveBounds))
                {
                    myPlayerTile.Move(movePos);

                    canMove = false;
                    StartCoroutine(MoveDelay());
                }
            }

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

    private void Select()
    {
        if(SceneController.current.IsCharacterSelectScene())
        {
            if (joined == true) { ready = true; }
            else if (joined == false) { joined = true; }
        }
    }

    private void Back()
    {
        if (SceneController.current.IsCharacterSelectScene())
        {
            if (ready == true && joined == true) { ready = false; }
            else if (joined == true) { joined = false; }
        }

    }

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

    public void EnableSprite (bool shouldEnable)
    {
        //mySprite.SetActive(shouldEnable);
    }
}
