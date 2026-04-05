using TMPro;
using UnityEngine;

public class EndOfDayPanel : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text dayCustomerText;
    public TMP_Text totalRevenueText;
    public TMP_Text totalExpensesText;
    private GameTime gameTime;
    private CustomerManager customerManager;
    private EconomyManager economyManager;

    public void Initialize(GameTime gameTime, CustomerManager customerManager, EconomyManager economyManager)
    {
        this.gameTime = gameTime;
        this.customerManager = customerManager;
        this.economyManager = economyManager;
        this.gameTime.OnEndOfDay += ShowPanel;
        panel.SetActive(false);
    }

    private void ShowPanel()
    {
        dayCustomerText.text = $"Total customers today: {customerManager.DayCustomerCount}";
        totalRevenueText.text = $"Total revenue today: ${economyManager.TotalEarnedToday}";
        totalExpensesText.text = $"Total expenses today: ${economyManager.TotalSpentToday}";
        panel.SetActive(true);
    }

    public void StartNextDay()
    {
        gameTime.StartNextDay();
        panel.SetActive(false);
    }
}
