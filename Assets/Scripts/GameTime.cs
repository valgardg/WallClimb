using UnityEngine;
using System;

public class GameTime
{
    [Header("Current Time")]
    public int currentDay = 1;
    public int currentHour = 10;
    public int currentMinute = 0;

    public Action OnTimeChanged;
    public Action OnEndOfDay;
    public Action OnStartOfDay;
    public Action OnEndOfWeek;

    public float GameSpeed { get; private set; } = 1f;
    public bool IsPaused { get; private set; } = false;

    public void AdvanceTime(int minutes)
    {
        currentMinute += minutes;
        if (currentMinute >= 60)
        {
            currentMinute = 0;
            currentHour++;
        }
         // invoke end of week
        if (currentDay % 7 == 0 && currentHour >= 22)
        {
            // Handle end of week logic here (e.g., reset weekly stats, trigger events)
            TogglePause();
            OnEndOfWeek?.Invoke();
            OnEndOfDay?.Invoke();
        }
        else if (currentHour >= 22)
        {
            TogglePause();
            OnEndOfDay?.Invoke();
        }
       

        OnTimeChanged?.Invoke();
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
    }

    public void SetSpeed(float speed)
    {
        GameSpeed = speed;
    }

    public void StartNextDay()
    {
        currentDay++;
        currentHour = 10;
        currentMinute = 0;
        TogglePause();
        OnTimeChanged?.Invoke();
        OnStartOfDay?.Invoke();
    }
}
