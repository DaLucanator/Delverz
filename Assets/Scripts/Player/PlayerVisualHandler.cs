using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerColour
{
    yellow,
    blue,
    red,
    green
}

public class PlayerVisualHandler : MonoBehaviour
{
    private PlayerColour myPlayerColour = PlayerColour.yellow;
    private PlayerRole myPlayerRole;
    private int myRoleInt = 0;
    private AnimationState currentState;
    private Dictionary<AnimationState, AnimationState> IdleAnimationStates = new Dictionary<AnimationState, AnimationState>
    {
        {AnimationState.north, AnimationState.northIdle },
        {AnimationState.east, AnimationState.eastIdle },
        {AnimationState.west, AnimationState.westIdle },
        {AnimationState.south, AnimationState.southIdle },
        {AnimationState.northIdle, AnimationState.northIdle },
        {AnimationState.eastIdle, AnimationState.eastIdle },
        {AnimationState.westIdle, AnimationState.westIdle },
        {AnimationState.southIdle, AnimationState.southIdle }

    };

    [SerializeField] private PlayerRole[] playerRoles = new PlayerRole[4];
    [SerializeField] private Animator myAnimator;
    [SerializeField] private Animator invincibleAnimator;


    public void SetColour(PlayerColour colourToSet)
    {
        myPlayerColour = colourToSet;
    }

    public void SetRoleExplicit(int roleNum)
    {
        myRoleInt = roleNum;
        myPlayerRole = playerRoles[myRoleInt];
        myAnimator.runtimeAnimatorController = myPlayerRole.ReturnAnimatorController(myPlayerColour);
        invincibleAnimator.runtimeAnimatorController = myPlayerRole.ReturnInvincibilityAnim();

        UIControllerParent.current.SetRole(myPlayerColour, myPlayerRole);

    }

    public void SetRole(bool shouldIncrease)
    {
        if (shouldIncrease) { myRoleInt++; }
        else { myRoleInt--; }

        if (myRoleInt < 0) { myRoleInt = 3; }
        if (myRoleInt > 3) { myRoleInt = 0; }

        myPlayerRole = playerRoles[myRoleInt];
        myAnimator.runtimeAnimatorController = myPlayerRole.ReturnAnimatorController(myPlayerColour);
        invincibleAnimator.runtimeAnimatorController = myPlayerRole.ReturnInvincibilityAnim();

        UIControllerParent.current.SetRole(myPlayerColour, myPlayerRole);
    }

    public void CheckAnimState(Vector2 moveDir)
    {
        if(moveDir == Vector2.zero) 
        {
            ChangeAnimState(IdleAnimationStates[currentState]);
        }
        else if (moveDir.x > 0) { ChangeAnimState(AnimationState.east); }
        else if (moveDir.x < 0) { ChangeAnimState(AnimationState.west); }
        else if (moveDir.y > 0) { ChangeAnimState(AnimationState.north); }
        else if (moveDir.y < 0) { ChangeAnimState(AnimationState.south); }
    }

    private void ChangeAnimState(AnimationState stateToChangeTo)
    {
        if (currentState == stateToChangeTo) { return; }

        else
        {
            currentState = stateToChangeTo;
            myAnimator.Play(stateToChangeTo.ToString());
            invincibleAnimator.Play(stateToChangeTo.ToString());
        }
    }

    private enum AnimationState
    { 
        north,
        east,
        south,
        west,
        northIdle,
        eastIdle,
        southIdle,
        westIdle
    }

}
