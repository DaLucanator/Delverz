using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Objects", menuName = "ScriptableObjects/Abilities")]
public class AbilityScriptableObject : ScriptableObject
{
    [SerializeField] private Ability ability;
    [SerializeField] private Sprite abilitySprite;

    public Ability ReturnAbility()
    {
        return ability;
    }

    public Sprite ReturnSprite()
    {
        return abilitySprite;
    }
}
