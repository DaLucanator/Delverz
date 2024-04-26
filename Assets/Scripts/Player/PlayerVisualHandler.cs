using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private PlayerColour myPlayerColour;
    private PlayerRole myPlayerRole;
    private int myRoleInt = 0;

    [SerializeField] private PlayerRole[] playerRoles = new PlayerRole[4];
    [SerializeField] private GameObject yellowCharacterSelect, blueCharacterSelect, redCharacterSelect, greenCharacterSelect;
    [SerializeField] private SpriteRenderer yellowCharacterSprite, blueCharacterSprite, redCharacterSprite, greenCharacterSprite;
    [SerializeField] private SpriteRenderer yellowAbilitySprite, blueAbilitySprite, redAbilitySprite, greenAbilitySprite;
    [SerializeField] private TextMeshProUGUI yellowRoleText, blueRoleText, redRoleText, greenRoleText;
    [SerializeField] private TextMeshProUGUI yellowAbilityText, blueAbilityText, redAbilityText, greenAbilityText;

    public void SetColour(PlayerColour colourToSet)
    {
        myPlayerColour = colourToSet;
    }

    public void SetRoleExplicit(int roleNum)
    {
        myRoleInt = roleNum;
        myPlayerRole = playerRoles[myRoleInt];

        if (myPlayerColour == PlayerColour.yellow) { yellowRoleText.text = myPlayerRole.ReturnRoleString(); yellowAbilityText.text = myPlayerRole.ReturnAbilityString(); yellowCharacterSprite.sprite = myPlayerRole.ReturnPortraitSprite(PlayerColour.yellow); yellowAbilitySprite.sprite = myPlayerRole.ReturnAbilitySprite(); }
        else if (myPlayerColour == PlayerColour.blue) { blueRoleText.text = myPlayerRole.ReturnRoleString(); blueAbilityText.text = myPlayerRole.ReturnAbilityString(); blueCharacterSprite.sprite = myPlayerRole.ReturnPortraitSprite(PlayerColour.blue); blueAbilitySprite.sprite = myPlayerRole.ReturnAbilitySprite(); }
        else if (myPlayerColour == PlayerColour.red) { redRoleText.text = myPlayerRole.ReturnRoleString(); redAbilityText.text = myPlayerRole.ReturnAbilityString(); redCharacterSprite.sprite = myPlayerRole.ReturnPortraitSprite(PlayerColour.red); redAbilitySprite.sprite = myPlayerRole.ReturnAbilitySprite(); }
        else if (myPlayerColour == PlayerColour.green) { greenRoleText.text = myPlayerRole.ReturnRoleString(); greenAbilityText.text = myPlayerRole.ReturnAbilityString(); greenCharacterSprite.sprite = myPlayerRole.ReturnPortraitSprite(PlayerColour.green); greenAbilitySprite.sprite = myPlayerRole.ReturnAbilitySprite(); }
    }

    public void SetRole(bool shouldIncrease)
    {
        if(shouldIncrease) { myRoleInt++; }
        else { myRoleInt--; }

        if (myRoleInt < 0) { myRoleInt = 3; }
        if (myRoleInt > 3) { myRoleInt = 0; }

        myPlayerRole = playerRoles[myRoleInt];

        if (myPlayerColour == PlayerColour.yellow) { yellowRoleText.text = myPlayerRole.ReturnRoleString(); yellowAbilityText.text = myPlayerRole.ReturnAbilityString(); yellowCharacterSprite.sprite = myPlayerRole.ReturnPortraitSprite(PlayerColour.yellow); yellowAbilitySprite.sprite = myPlayerRole.ReturnAbilitySprite(); }
        else if (myPlayerColour == PlayerColour.blue) { blueRoleText.text = myPlayerRole.ReturnRoleString(); blueAbilityText.text = myPlayerRole.ReturnAbilityString(); blueCharacterSprite.sprite = myPlayerRole.ReturnPortraitSprite(PlayerColour.blue); blueAbilitySprite.sprite = myPlayerRole.ReturnAbilitySprite(); }
        else if (myPlayerColour == PlayerColour.red) { redRoleText.text = myPlayerRole.ReturnRoleString(); redAbilityText.text = myPlayerRole.ReturnAbilityString(); redCharacterSprite.sprite = myPlayerRole.ReturnPortraitSprite(PlayerColour.red); redAbilitySprite.sprite = myPlayerRole.ReturnAbilitySprite(); }
        else if (myPlayerColour == PlayerColour.green) { greenRoleText.text = myPlayerRole.ReturnRoleString(); greenAbilityText.text = myPlayerRole.ReturnAbilityString(); greenCharacterSprite.sprite = myPlayerRole.ReturnPortraitSprite(PlayerColour.green); greenAbilitySprite.sprite = myPlayerRole.ReturnAbilitySprite(); }
    }

    public void Update()
    {
        if(SceneController.current.IsCharacterSelectScene())
        {
            if(myPlayerColour == PlayerColour.yellow)
            {
                yellowCharacterSelect.SetActive(true);
            }
            else if (myPlayerColour == PlayerColour.blue)
            {
                blueCharacterSelect.SetActive(true);
            }
            else if (myPlayerColour == PlayerColour.red)
            {
                redCharacterSelect.SetActive(true);
            }
            else if (myPlayerColour == PlayerColour.green)
            {
                greenCharacterSelect.SetActive(true);
            }
        }
    }

}
