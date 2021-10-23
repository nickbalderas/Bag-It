using System;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float duration;
    public float TimeRemaining { get; private set; }
    public Action HandleCompletion;
    public TextMeshProUGUI timerText;
    
    // Start is called before the first frame update
    void Start()
    {
        TimeRemaining = duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeRemaining > 0)
        {
            TimeRemaining -= Time.deltaTime;
            UpdateTimeDisplay();
        }
        else TimeExpired();
    }

    private void UpdateTimeDisplay()
    {
        if (!timerText) return;
        
        float minutes = Mathf.FloorToInt(TimeRemaining / 60); 
        float seconds = Mathf.FloorToInt(TimeRemaining % 60);

        timerText.text = $"{minutes:0}:{seconds:00}";
    }

    private void TimeExpired()
    {
        TimeRemaining = 0;
        HandleCompletion();
    }
}
