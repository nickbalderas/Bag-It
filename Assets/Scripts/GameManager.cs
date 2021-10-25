using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private ItemSpawner _itemSpawner;
    private TimeManager _countdownTimer;
    public TimeManager GameTimer { get; private set; }
    public ScoreManager GameScore { get; private set; }
    private GameUI _gameUI;
    private bool _hasGameStarted;

    private void Awake()
    {
        GameScore = GetComponent<ScoreManager>();

        _itemSpawner = GameObject.Find("Item Spawner").GetComponent<ItemSpawner>();
        
        _gameUI = GetComponent<GameUI>();

        _countdownTimer = GameObject.Find("Countdown").GetComponent<TimeManager>();
        _countdownTimer.duration = 4;
        _countdownTimer.HandleCompletion = StartGame;
        _countdownTimer.IsActive = false;

        GameTimer = GetComponent<TimeManager>();
        GameTimer.duration = 60.0f;
        GameTimer.HandleCompletion = GameOver;
        GameTimer.IsActive = false;
    }

    private void Start()
    {
        InitiateCountdown();
    }

    private void Update()
    {
        if (!_hasGameStarted) return;
        HandleGamePaused();
    }

    private void InitiateCountdown()
    {
        _gameUI.countdown.SetActive(true);
        _countdownTimer.IsActive = true;
    }

    private void StartGame()
    {
        _gameUI.gameOverlay.SetActive(true);
        _gameUI.countdown.SetActive(false);
        GameTimer.IsActive = true;
        _hasGameStarted = true;
        StartCoroutine("HandleItemSpawn");
    }
    
    private void HandleGamePaused()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (GameTimer.IsActive)
        {
            GameTimer.IsActive = false;
            _gameUI.gamePauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            GameTimer.IsActive = true;
            _gameUI.gamePauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    private void GameOver()
    {
        GameTimer.IsActive = false;
        _hasGameStarted = false;
        _gameUI.UpdateFinalScore(GameScore.Score);
        _gameUI.gameOver.SetActive(true);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void ShowHowToPlay()
    {
        _gameUI.howToPlay.SetActive(true);
    }

    public void HideHowToPlay()
    {
        _gameUI.howToPlay.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    private IEnumerator HandleItemSpawn()
    {
        while (GameTimer.IsActive)
        {
            _itemSpawner.SpawnItem();
            yield return new WaitForSeconds(1);
        }
    }
}
