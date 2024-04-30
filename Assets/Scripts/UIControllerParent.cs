using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerParent : MonoBehaviour
{
    [SerializeField] private UIController yellowUI, blueUI, redUI, greenUI;

    public static UIControllerParent current;
    private bool yellowInMain = true, blueInMain, redInMain, greenInMain;

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

}
