using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public AudioSource music;
    public bool isMusicPlaying;

    public float bpm;
    public float timePerBeat;

    public uint score;

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

        timePerBeat = 60f / bpm;
    }

    void Update()
    {
        //temporary start method
        if (!isMusicPlaying)
        {
            if (Input.anyKeyDown)
            {
                isMusicPlaying = true;
                music.Play();
            }
        }

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
        isMusicPlaying = false;
        music.Pause();

        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        isPaused = false;
        isMusicPlaying = true;
        music.Play();

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
        isMusicPlaying = false;
        music.Pause();

        Debug.Log("Game Over!");

        Time.timeScale = 0f;

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
