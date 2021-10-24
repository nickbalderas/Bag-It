using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public GameObject gameOverlay;
    public GameObject gamePauseMenu;
    public GameObject gameOver;
    public GameObject countdown;

    public TextMeshProUGUI finalScore;

    public void UpdateFinalScore(int score)
    {
        finalScore.text = "Final Score: " + $"{score}";
    }
}
