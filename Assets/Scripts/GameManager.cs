using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TimeManager _gameTimer;
    public ScoreManager gameScore;

    private void Awake()
    {
        gameScore = GetComponent<ScoreManager>();
        
        _gameTimer = GetComponent<TimeManager>();
        _gameTimer.duration = 60.0f;
        _gameTimer.HandleCompletion = GameOver;
    }

    private void GameOver()
    {
        Time.timeScale = 0;
    }
}
