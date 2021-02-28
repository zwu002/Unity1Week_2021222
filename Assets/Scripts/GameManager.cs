using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public AudioSource music1;
    public AudioSource music2;
    public float bpm1;
    public float bpm2;

    public AudioSource levelMusic;

    public bool isMusicPlaying;

    public float currentBpm;
    public float timePerBeat;

    public uint miss;
    public uint hit;
    public uint perfectHit;
    uint ratingScore;

    public GameObject mainMenu;
    public GameObject mainHUD;
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
        isMusicPlaying = false;
        levelMusic.Pause();

        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        isPaused = false;
        isMusicPlaying = true;
        levelMusic.Play();

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
        levelMusic.Pause();

        Debug.Log("Game Over!");

        Time.timeScale = 0f;

        mainHUD.SetActive(false);
        gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        CleanScore();
        Restart();
    }

    public void Level1Begin()
    {
        mainHUD.SetActive(true);
        mainMenu.SetActive(false);
        isMusicPlaying = true;

        levelMusic = music1;
        currentBpm = bpm1;

        timePerBeat = 60f / currentBpm;

        CleanScore();
        levelMusic.Play();
    }

    public void Level2Begin()
    {
        mainHUD.SetActive(true);
        mainMenu.SetActive(false);
        isMusicPlaying = true;

        levelMusic = music2;
        currentBpm = bpm2;
        timePerBeat = 60f / currentBpm;

        CleanScore();
        levelMusic.Play();
    }


    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }

    void CleanScore()
    {
        miss = 0;
        hit = 0;
        perfectHit = 0;
    }

}
