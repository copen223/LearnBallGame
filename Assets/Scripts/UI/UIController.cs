using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public GameObject pause;
    public GameObject gamePause;

    public void StartScene()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void GameOverScene()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }
    public void GamePause()
    {
        pause.SetActive(false);
        gamePause.SetActive(true);
        Time.timeScale = 0;
    }
    public void GameContinue()
    {
        pause.SetActive(true);
        gamePause.SetActive(false);
        Time.timeScale = 1;
    }
}
