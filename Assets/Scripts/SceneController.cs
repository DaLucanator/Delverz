using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController current;

    public void Awake()
    {
        current = this;
    }

    public bool IsCharacterSelectScene()
    {
        if (SceneManager.GetActiveScene().name == "CharacterSelect")
        {
            return true;
        }

        else return false;
    }

    public bool IsScoreScene()
    {
        if (SceneManager.GetActiveScene().name == "ScoreScene")
        {
            return true;
        }

        else return false;
    }

    public bool IsMainScene()
    {
        if (SceneManager.GetActiveScene().name == "Luc's Scene")
        {
            return true;
        }

        else return false;
    }
}
