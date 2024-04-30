using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class UIController : MonoBehaviour
{
    private string score;
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
        abilityRendererMain.sprite = myPlayerRole.ReturnAbilitySpriteSmall();
    }

    public void SetScore(int ScoreToset)
    {
        string addToScore = "";
        string scoreTemp = ScoreToset.ToString();

        if (score.Length > 4) { addToScore += "0"; }
        if (score.Length > 3) { addToScore += "0"; }
        if (score.Length > 2) { addToScore += "0"; }

        scoreText.text = addToScore + scoreTemp;
    }
}
