using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int Score { get; private set; }
    public int ScoreMultiplier { get; private set; }
    public int Penalty { get; private set; }
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
        if (scoreDisplay) scoreDisplay.text = "0";
        if (scoreMultiplierDisplay) scoreMultiplierDisplay.text = "x1";
    }

    public void ApplyPenalty(int penalty)
    {
        Penalty = penalty;
        if (Score + penalty < 0)
        {
            Score = 0;
            return;
        }
        Score += penalty;
    }
}
