using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "ScriptableObjects/PlayerRoles")]
public class PlayerRole : ScriptableObject
{
    [SerializeField] private Sprite yellow, red, blue, green;
    [TextArea]
    [SerializeField] private string roleName, abilityDescription;
    [SerializeField] private Ability innateAbility;

    public string ReturnRoleName()
    {
        return roleName;
    }

    public string ReturnabilityDescription()
    {
        return abilityDescription;
    }
    
    public Sprite ReturnPortrait(PlayerColour myColour)
    {
        if (myColour == PlayerColour.yellow) { return yellow; }
        else if (myColour == PlayerColour.blue) { return blue; }
        else if (myColour == PlayerColour.red) { return red; }
        else if (myColour == PlayerColour.green) { return green; }

        else return null;
    }
}
