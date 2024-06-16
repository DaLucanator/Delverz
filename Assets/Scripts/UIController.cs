using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject CharacterSelectUI, MainSceneUI;
    [SerializeField] PlayerColour myPlayerColour;
    [SerializeField] private SpriteRenderer characterPortraitRendererMain;
    [SerializeField] private SpriteRenderer abilityRendererMain;
    [SerializeField] TextMeshProUGUI scoreText;

    public void EnableMainSceneUI(bool shouldEnable)
    {
        MainSceneUI.SetActive(shouldEnable);
    }

    public void SetRole(PlayerRole myPlayerRole)
    {
        characterPortraitRendererMain.sprite = myPlayerRole.ReturnPortraitSpriteSmall(myPlayerColour);
    }

    public void PickupAbility(Sprite abilitySprite)
    {
        abilityRendererMain.sprite = abilitySprite;
    }



    public void SetScore(int ScoreToset)
    {
        string addToScore = "";
        string scoreTemp = ScoreToset.ToString();

        if (scoreTemp.Length < 4) { addToScore += "0"; }
        if (scoreTemp.Length < 3) { addToScore += "0"; }
        if (scoreTemp.Length < 2) { addToScore += "0"; }
        if (scoreTemp.Length < 1) { addToScore += "0"; }

        scoreText.text = addToScore + scoreTemp;
    }
}
