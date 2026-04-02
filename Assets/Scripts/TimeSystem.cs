using System;

public class TimeSystem
{
    public float GameSpeed { get; private set; } = 1f;
    public bool IsPaused { get; private set; } = false;

    public Action OnTimeSettingsChanged;

    public void SetSpeed(float speed)
    {
        GameSpeed = speed;
        OnTimeSettingsChanged?.Invoke();
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        OnTimeSettingsChanged?.Invoke();
    }
}
