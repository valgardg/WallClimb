using UnityEngine;
using System;

public class GameTime
{
    public int currentDay = 1;
    public int currentHour = 0;
    public int currentMinute = 0;
    public Action OnTimeChanged;
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
            currentHour = 8;
            currentDay++;
        }

        Debug.Log($"Day {currentDay}, {currentHour:00}:{currentMinute:00}");
        OnTimeChanged?.Invoke();
    }
}
