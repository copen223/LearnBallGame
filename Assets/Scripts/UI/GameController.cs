using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

   
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }
    public GameObject pause;

    public void StartScene()
    {
        SceneManager.LoadScene(0);
    }
    public void GameOverScene()
    {
        SceneManager.LoadScene(2);
    }
    public void GamePause()
    {
        pause.SetActive(false);
        Time.timeScale = 0;
    }
    public void GameContinue()
    {
        pause.SetActive(true);
        Time.timeScale = 1;
    }
}
