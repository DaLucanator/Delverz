using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CalibrationScreen : MonoBehaviour
{
    private PlayerInputManager inputManager;
    [SerializeField] private GameObject yellowUI, blueUI, redUI, greenUI, checkUI;

    private void Start()
    {
        inputManager = PlayerInputManager.instance;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        if (inputManager.playerCount == 1)
        {
            yellowUI.SetActive(false);
            blueUI.SetActive(true);
        }

        if (inputManager.playerCount == 2)
        {
            blueUI.SetActive(false);
            redUI.SetActive(true);
        }

        if (inputManager.playerCount == 3)
        {
            redUI.SetActive(false);
            greenUI.SetActive(true);
        }

        if (inputManager.playerCount == 4)
        {
            greenUI.SetActive(false);
            checkUI.SetActive(true);
        }


    }
}
