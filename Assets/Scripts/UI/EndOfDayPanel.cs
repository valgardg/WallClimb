using TMPro;
using UnityEngine;

public class EndOfDayPanel : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text dayCustomerText;
    private GameTime gameTime;
    private CustomerManager customerManager;

    public void Initialize(GameTime gameTime, CustomerManager customerManager)
    {
        this.gameTime = gameTime;
        this.customerManager = customerManager;
        this.gameTime.OnEndOfDay += ShowPanel;
        panel.SetActive(false);
    }

    private void ShowPanel()
    {
        dayCustomerText.text = $"Total customers today: {customerManager.DayCustomerCount}";
        panel.SetActive(true);
    }

    public void StartNextDay()
    {
        gameTime.StartNextDay();
        panel.SetActive(false);
    }
}
