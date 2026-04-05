using UnityEngine;

public class UIManager : MonoBehaviour
{
    // uis 
    public TimePanel timePanel;
    public EndOfDayPanel endOfDayPanel;
    public CustomersPanel customersPanel;
    public UpgradePanel upgradePanel;
    public GymStatsPanel gymStatsPanel;
    public GameOverPanel gameOverPanel;

    public void Initialize(
            GameTime gameTime, 
            CustomerManager customerManager, 
            UpgradeManager upgradeManager, 
            GymState gymState, 
            EconomyManager economyManager
        )
    {
        timePanel.Initialize(gameTime);
        endOfDayPanel.Initialize(gameTime, customerManager, economyManager);
        upgradePanel.Initialize(upgradeManager);
        customersPanel.Initialize(customerManager);
        gymStatsPanel.Initialize(gymState);
        gameOverPanel.Initialize(economyManager);
    }

    private void Update()
    {
        gymStatsPanel.UpdateStats();
    }
}