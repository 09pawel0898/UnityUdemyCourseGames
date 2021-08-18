﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    [SerializeField] private GameObject startGameButton;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private Button restartGameButton;

    [SerializeField] 
    private Text scoreText, endScore, bestScore, gameOverText;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject[] birds;
    [SerializeField] private Sprite[] medals;
    [SerializeField] private Image medalImage;

    private void Awake()
    {
        MakeInstance();
        Time.timeScale = 0;
    }
    
    private void Start()
    {
        birds[GameController.instance.SelectedBird].SetActive(true);
    }

    private void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }

    public void StartTheGame()
    {
        scoreText.gameObject.SetActive(true);
        startGameButton.gameObject.SetActive(false);

        BirdScript.instance.gameStarted = true;
        pauseButton.gameObject.SetActive(true);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
        //startGameButton.gameObject.SetActive(true);
    }

    public void PauseGame()
    {
        if(BirdScript.instance.isAlive)
        {
            pauseButton.gameObject.SetActive(false);
            pausePanel.SetActive(true);
            gameOverText.gameObject.SetActive(false);
            endScore.text = "" + BirdScript.instance.score;
            bestScore.text = "" + GameController.instance.HighScore;
            Time.timeScale = 0;
            restartGameButton.onClick.RemoveAllListeners();
            restartGameButton.onClick.AddListener(() => ResumeGame());
        }
    }

    public void GoToMenuButton()
    {
        Time.timeScale = 1f;
        SceneFader.instance.FadeInAndOut("MainMenu");
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneFader.instance.FadeInAndOut(Application.loadedLevelName);
    }

    public void SetScore(int score)
    {
        scoreText.text = "" + score;
    }

    public void PlayerDiedShowScore(int score)
    {
        pausePanel.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        endScore.text = "" + score;

        if (score > GameController.instance.HighScore)
            GameController.instance.HighScore = score;

        bestScore.text = "" + GameController.instance.HighScore;

        if (score <= 20)
            medalImage.sprite = medals[1];
        else if (score > 20 && score < 40)
        {
            medalImage.sprite = medals[0];
            if (GameController.instance.IsGreenBirdUnlocked() == 0)
                GameController.instance.UnlockGreenBird();
        }
        else
        {
            medalImage.sprite = medals[2];
            if (GameController.instance.IsRedBirdUnlocked() == 0)
                GameController.instance.UnlockRedBird();
        }

        restartGameButton.onClick.RemoveAllListeners();
        restartGameButton.onClick.AddListener(() => RestartGame());
    }
}