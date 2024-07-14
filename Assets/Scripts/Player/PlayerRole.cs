using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "ScriptableObjects/PlayerRoles")]
public class PlayerRole : ScriptableObject
{
    [SerializeField] private Sprite yellow, red, blue, green, yellowSmall, redSmall, blueSmall, greenSmall;
    [SerializeField] private AnimatorOverrideController yellowAnim, redAnim, blueAnim, greenAnim;
    [SerializeField] private AnimatorOverrideController invincibilityAnim;
    [SerializeField] private string roleName, abilityDescription;
    [SerializeField] private AbilityScriptableObject innateAbility;

    public string ReturnRoleString()
    {
        return roleName;
    }

    public string ReturnAbilityString()
    {
        return abilityDescription;
    }
    
    public Sprite ReturnPortraitSpriteLarge(PlayerColour myColour)
    {
        if (myColour == PlayerColour.yellow) { return yellow; }
        else if (myColour == PlayerColour.blue) { return blue; }
        else if (myColour == PlayerColour.red) { return red; }
        else if (myColour == PlayerColour.green) { return green; }

        else return null;
    }

    public Sprite ReturnPortraitSpriteSmall(PlayerColour myColour)
    {
        if (myColour == PlayerColour.yellow) { return yellowSmall; }
        else if (myColour == PlayerColour.blue) { return blueSmall; }
        else if (myColour == PlayerColour.red) { return redSmall; }
        else if (myColour == PlayerColour.green) { return greenSmall; }

        else return null;
    }

    public Sprite ReturnAbilitySpriteSmall()
    {
        return innateAbility.ReturnSprite();
    }

    public AnimatorOverrideController ReturnAnimatorController(PlayerColour myColour)
    {
        if (myColour == PlayerColour.yellow) { return yellowAnim; }
        else if (myColour == PlayerColour.blue) { return blueAnim; }
        else if (myColour == PlayerColour.red) { return redAnim; }
        else if (myColour == PlayerColour.green) { return greenAnim; }

        else return null;
    }

    public AnimatorOverrideController ReturnInvincibilityAnim()
    {
        return invincibilityAnim;
    }

}
