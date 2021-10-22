using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TimeManager _gameTimer;

    private void Awake()
    {
        _gameTimer = GetComponent<TimeManager>();
        _gameTimer.duration = 60.0f;
        _gameTimer.HandleCompletion = GameOver;
    }

    private void GameOver()
    {
        Time.timeScale = 0;
    }
}
