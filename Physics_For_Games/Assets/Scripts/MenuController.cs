using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GotoExit();
        }
    }

    public void OnStartUp()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
    }

    public void GotoMainMenu()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
    }

    public void GotoMainGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }

    public void GotoExit()
    {
        Application.Quit();
    }
}
