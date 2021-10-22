using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float duration;
    public float TimeRemaining { get; private set; }
    public Action HandleCompletion;
    
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
        }
        else TimeExpired();
    }

    private void TimeExpired()
    {
        TimeRemaining = 0;
        HandleCompletion();
    }
}
