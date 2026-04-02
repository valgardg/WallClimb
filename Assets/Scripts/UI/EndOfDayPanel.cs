using UnityEngine;

public class EndOfDayPanel : MonoBehaviour
{
    public GameObject panel;
    private GameTime gameTime;

    public void Initialize(GameTime gameTime)
    {
        this.gameTime = gameTime;
        this.gameTime.OnEndOfDay += ShowPanel;
        panel.SetActive(false);
    }

    private void ShowPanel()
    {
        panel.SetActive(true);
    }

    public void StartNextDay()
    {
        gameTime.TogglePause();
        panel.SetActive(false);
    }
}
