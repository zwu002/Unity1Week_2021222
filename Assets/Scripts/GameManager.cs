using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject mainHUD;
    public GameObject nextLevelUI;
    public GameObject levelLoadingUI;
    public GameObject gameOverUI;
    public GameObject gameWinUI;
    public GameObject pauseUI;

    public bool isGameOver;
    public bool isPaused;

    static GameManager instance = null;

    void Awake()
    {
        Time.timeScale = 1;

        Debug.Log("Game Manager Awake");
        if (null == instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.LogWarning("Destroy duplicate gm");
            Destroy(gameObject);
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

    public void GameWin()
    {
        Debug.Log("You Won!");

        gameWinUI.SetActive(true);
    }

    public void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over!");

        mainHUD.SetActive(false);
        gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        Restart();
    }

    public void LoadNextLevel()
    {

    }

    public void LevelBegin()
    {
        mainHUD.SetActive(true);
    }


    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }
}
