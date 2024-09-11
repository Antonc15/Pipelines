using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    [SerializeField] private string mainMenuScene;

    private void Update()
    {
        MainMenu();
    }

    private void MainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(mainMenuScene, LoadSceneMode.Single);
        }
    }
}
