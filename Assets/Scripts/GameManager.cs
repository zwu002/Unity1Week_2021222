using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AudioSource music1;
    public AudioSource music2;
    public float bpm1;
    public float bpm2;

    [SerializeField] float music1Length;
    [SerializeField] float music2Length;
    public float extraLevelLength = 5f;

    public GameObject level1Spawners;
    public GameObject level2Spawners;

    public float level1ScoreMultiplier;
    public float level2ScoreMultiplier;
    float scoreMultiplier;

    public AudioSource levelMusic;
    [SerializeField] float timer;
    [SerializeField] float currentMusicLength;

    public bool isInLevel;
    public bool isMusicPlaying;

    public float currentBpm;
    public float timePerBeat;

    public int miss;
    public int hit;
    public int perfectHit;
    public float ratingScore;

    public GameObject mainMenu;
    public GameObject mainHUD;
    public GameObject gameOverUI;
    public GameObject gameWinUI;
    public GameObject pauseUI;

    public bool isGameOver;
    public bool isPaused;

    public float cameraShakeMagnitude;
    public float cameraShakeMaxMagnitude = 5f;

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

        isInLevel = false;

        music1Length = music1.clip.length;
        music2Length = music2.clip.length;
    }

    void Update()
    {
        if (isInLevel)
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

            if (Time.time - timer - extraLevelLength >= currentMusicLength - 6f)
            {
                if (cameraShakeMagnitude > 0)
                {
                    cameraShakeMaxMagnitude -= 0.02f;
                }
                else
                {
                    cameraShakeMagnitude = 0f;
                }
            }

            if (Time.time - timer - extraLevelLength >= currentMusicLength)
            {
                GameWin();
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
        isInLevel = false;
        isMusicPlaying = false;

        levelMusic.Pause();

        Debug.Log("You Won!");

        Time.timeScale = 0f;

        ratingScore = (hit * 1 + perfectHit * 2 - miss * 5) * scoreMultiplier;

        mainHUD.SetActive(false);
        gameWinUI.SetActive(true);
    }

    public void GameOver()
    {
        isGameOver = true;
        isMusicPlaying = false;
        isInLevel = false;

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
        level1Spawners.SetActive(true);
        level2Spawners.SetActive(false);
        isMusicPlaying = true;
        isInLevel = true;

        levelMusic = music1;
        currentBpm = bpm1;

        timePerBeat = 60f / currentBpm;

        CleanScore();
        levelMusic.Play();

        currentMusicLength = music1Length;
        timer = Time.time;

        scoreMultiplier = level1ScoreMultiplier;
    }

    public void Level2Begin()
    {
        mainHUD.SetActive(true);
        mainMenu.SetActive(false);
        level1Spawners.SetActive(false);
        level2Spawners.SetActive(true);
        isMusicPlaying = true;
        isInLevel = true;

        levelMusic = music2;
        currentBpm = bpm2;
        timePerBeat = 60f / currentBpm;

        CleanScore();
        levelMusic.Play();

        currentMusicLength = music2Length + 3f;
        timer = Time.time;

        scoreMultiplier = level2ScoreMultiplier;
    }


    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }

    void CleanScore()
    {
        miss = 0;
        hit = 0;
        perfectHit = 0;
    }

}
