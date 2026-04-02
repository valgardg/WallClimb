using UnityEngine;
using TMPro;

public class TimePanel : MonoBehaviour
{
    public TMP_Text dayText;
    public TMP_Text hourText;
    public TMP_Text minuteText;

    private GameTime gameTime;
    private TimeSystem timeSystem;

    public void Initialize(TimeSystem timeSystem, GameTime gameTime)
    {
        this.timeSystem = timeSystem;
        this.gameTime = gameTime;
        this.gameTime.OnTimeChanged += UpdateTimeDisplay;
        UpdateTimeDisplay();
    }

    public void UpdateTimeDisplay()
    {
        if (gameTime != null)
        {
            dayText.text = $"Day: {gameTime.currentDay}";
            hourText.text = $"Hour: {gameTime.currentHour:00}";
            minuteText.text = $"Minute: {gameTime.currentMinute:00}";
        }
    }

    public void TogglePause()
    {
        timeSystem.TogglePause();
    }

    // slider for game speed would call this method
    public void SetGameSpeed(float speed)
    {
        timeSystem.SetSpeed(speed);
    }
}
