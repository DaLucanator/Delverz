using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerParent : MonoBehaviour
{
    [SerializeField] private UIController yellowUI, blueUI, redUI, greenUI;

    public static UIControllerParent current;
    private bool yellowInMain = true, blueInMain = true, redInMain = true, greenInMain = true;

    private void Awake()
    {
        current = this;
    }

    private void Update()
    {
        if(SceneController.current.IsMainScene())
        {
            yellowUI.EnableMainSceneUI(yellowInMain);
            blueUI.EnableMainSceneUI(blueInMain);
            redUI.EnableMainSceneUI(redInMain);
            greenUI.EnableMainSceneUI(greenInMain);
        }
    }

    public void SetRole(PlayerColour colourToSet, PlayerRole roleToSet)
    {
        if (colourToSet == PlayerColour.yellow) { yellowUI.SetRole(roleToSet); }
        else if (colourToSet == PlayerColour.blue) { blueUI.SetRole(roleToSet); }
        else if (colourToSet == PlayerColour.red) { redUI.SetRole(roleToSet); }
        else if (colourToSet == PlayerColour.green) { greenUI.SetRole(roleToSet); }
    }

    public void SetScore(PlayerColour colourToSet, int scoreToSet)
    {
        if (colourToSet == PlayerColour.yellow) { yellowUI.SetScore(scoreToSet); }
        else if (colourToSet == PlayerColour.blue) { blueUI.SetScore(scoreToSet); }
        else if (colourToSet == PlayerColour.red) { redUI.SetScore(scoreToSet); }
        else if (colourToSet == PlayerColour.green) { greenUI.SetScore(scoreToSet); }
    }

    public void PickupABility(PlayerColour colourToSet, Sprite abilityToPickup)
    {
        if (colourToSet == PlayerColour.yellow) { yellowUI.PickupAbility(abilityToPickup); }
        else if (colourToSet == PlayerColour.blue) { blueUI.PickupAbility(abilityToPickup); }
        else if (colourToSet == PlayerColour.red) { redUI.PickupAbility(abilityToPickup); }
        else if (colourToSet == PlayerColour.green) { greenUI.PickupAbility(abilityToPickup); }
    }

}
