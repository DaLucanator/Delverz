using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController current;
    [SerializeField] private Scene characterSelectScene, mainScene;

    public void Awake()
    {

    }

    public bool IsCharacterSelectScene()
    {
        if (SceneManager.GetActiveScene() == characterSelectScene)
        {
            return true;
        }

        else return false;
    }

    public bool IsMainScene()
    {
        if (SceneManager.GetActiveScene() == mainScene)
        {
            return true;
        }

        else return false;
    }
}
