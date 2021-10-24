using System;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float duration;
    private float TimeRemaining { get; set; }
    public Action HandleCompletion;
    public TextMeshProUGUI timerText;
    public bool IsActive { get; set; }

    void Start()
    {
        TimeRemaining = duration;
    }

    void Update()
    {
        if (!IsActive) return;
        
        if (TimeRemaining > 0)
        {
            UpdateTimeDisplay();
            TimeRemaining -= Time.deltaTime;
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
        IsActive = false;
        TimeRemaining = 0;
        HandleCompletion();
    }
}
