using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "ScriptableObjects/PlayerRoles")]
public class PlayerRole : ScriptableObject
{
    [SerializeField] private Sprite yellow, red, blue, green;
    [SerializeField] private AnimatorOverrideController yellowAnim, redAnim, blueAnim, greenAnim;
    [TextArea]
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
    
    public Sprite ReturnPortraitSprite(PlayerColour myColour)
    {
        if (myColour == PlayerColour.yellow) { return yellow; }
        else if (myColour == PlayerColour.blue) { return blue; }
        else if (myColour == PlayerColour.red) { return red; }
        else if (myColour == PlayerColour.green) { return green; }

        else return null;
    }

    public Sprite ReturnAbilitySprite()
    {
        return innateAbility.ReturnSprite();
    }
}
