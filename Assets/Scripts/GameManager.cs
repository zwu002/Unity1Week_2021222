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

    float timer;
    public float onBeatThreshold = 0.1f;
    public int onBeatMultiplier = 4;
    public bool onBeat = false;

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

                timer = Time.time;
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

    void FixedUpdate()
    {
        if (isMusicPlaying)
        {
            if (Time.time - timer + onBeatThreshold >= timePerBeat * onBeatMultiplier)
            {
                timer = Time.time;
                StartCoroutine(TriggerOnBeat());
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

    IEnumerator TriggerOnBeat()
    {
        onBeat = true;
        Debug.Log("OnBeat!");

        yield return new WaitForSeconds(onBeatThreshold * 2);

        onBeat = false;
    }
}
