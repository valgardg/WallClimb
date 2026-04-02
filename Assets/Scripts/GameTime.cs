using UnityEngine;
using System;

public class GameTime
{
    public int currentDay = 1;
    public int currentHour = 10;
    public int currentMinute = 0;
    public Action OnTimeChanged;
    public Action OnEndOfDay;

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
        if (currentHour >= 22)
        {
            TogglePause();
            OnEndOfDay?.Invoke();
        }

        Debug.Log($"Day {currentDay}, {currentHour:00}:{currentMinute:00}");
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
    }
}
