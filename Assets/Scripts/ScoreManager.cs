using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private int Score { get; set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    public void UpdateScore(int value, int multiplier)
    {
        Score += value * multiplier;
    }
}
