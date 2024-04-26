using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    [SerializeField] Scene calibrationScene, characterSelectScene, playScene;
    public static GameData current;
    void Awake()
    {
        current = this; 
    }

    public bool isCharacterSelect()
    {
        if (SceneManager.GetActiveScene() == characterSelectScene)
        {
            return true;
        }

        else return false;
    }

    public bool CanInputUI()
    {
        if (SceneManager.GetActiveScene() == characterSelectScene)
        {
            return true;
        }

        else return false;
    }

    public bool CanInputMove()
    {
        if (SceneManager.GetActiveScene() == calibrationScene || SceneManager.GetActiveScene() == playScene)
        {
            return true;
        }

        else return false;
    }


}
