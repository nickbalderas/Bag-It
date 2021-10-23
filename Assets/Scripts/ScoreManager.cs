using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int Score { get; private set; }
    private int ScoreMultiplier { get; set; }
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI scoreMultiplierDisplay;

    private void Start()
    {
        Score = 0;
        ScoreMultiplier = 1;
        UpdateScoreDisplay();
    }

    public void UpdateScoreMultiplier(int newMultiplier)
    {
        ScoreMultiplier = newMultiplier;
        UpdateScoreMultiplierDisplay();
    }
    
    public void UpdateScore(int value)
    {
        Score += value * ScoreMultiplier;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (!scoreDisplay) return;
        scoreDisplay.text = $"{Score}";
    }

    private void UpdateScoreMultiplierDisplay()
    {
        if (!scoreMultiplierDisplay) return;
        scoreMultiplierDisplay.text = $"{"x" + ScoreMultiplier}";
    }

    public void Reset()
    {
        Score = 0;
        ScoreMultiplier = 0;
        scoreDisplay.text = "0";
        scoreMultiplierDisplay.text = "x1";
    }
}
