using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {
    
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;

    public static GameManager Instance;

    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countDownPage;
    public Text scoreText;

    enum PageState {
        None,
        Start,
        GameOver,
        Countdown
    }

    int score = 0;
    int gameOverCount = 0;
    bool gameOver = true;

    public int GameOverCount { get { return gameOverCount; } }
    public bool GameOver { get { return gameOver; } }
    public int Score { get { return score; } }

    void Awake() {
        Instance = this;
    }

    void OnEnable() {
        CountDownText.OnCountdownFinished += OnCountdownFinished;
        TapController.OnPlayerDied += OnPlayerDied;
        TapController.OnPlayerScored += OnPlayerScored;
    }

    void OnDisable() {
        CountDownText.OnCountdownFinished -= OnCountdownFinished;
        TapController.OnPlayerDied -= OnPlayerDied;
        TapController.OnPlayerScored -= OnPlayerScored;
    }

    void OnCountdownFinished()
    {
        SetPageState(PageState.None);
        OnGameStarted();
        score = 0;
        gameOver = false;
    }

    void OnPlayerDied() {
        gameOver = true;
        gameOverCount++;
        int savedScore = PlayerPrefs.GetInt("HighScore");
        if (score > savedScore) {
            PlayerPrefs.SetInt("HighScore", score);
        }
        SetPageState(PageState.GameOver);
    }

    void OnPlayerScored() {
        score++;
        scoreText.text = score.ToString();
    }

    void SetPageState(PageState state) {
        switch (state) {
            case PageState.None:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(false);
                break;
            case PageState.Start:
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(false);
                break;
            case PageState.Countdown:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(true);
                break;
            case PageState.GameOver:
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                countDownPage.SetActive(false);
                break;
        }
    }

    public void ConfirmedGameOver() {
        OnGameOverConfirmed();
        scoreText.text = "0";
        SetPageState(PageState.Start);
    }

    public void StartGame() {
        SetPageState(PageState.Countdown);
    }

    
}
