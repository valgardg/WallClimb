using UnityEngine;

public class UIManager : MonoBehaviour
{
    // uis 
    public TimePanel timePanel;
    public EndOfDayPanel endOfDayPanel;
    public CustomersPanel customersPanel;
    public UpgradePanel upgradePanel;
    public GymStatsPanel gymStatsPanel;

    public void Initialize(GameTime gameTime, CustomerManager customerManager, UpgradeManager upgradeManager, GymState gymState)
    {
        timePanel.Initialize(gameTime);
        endOfDayPanel.Initialize(gameTime, customerManager);
        upgradePanel.Initialize(upgradeManager);
        customersPanel.Initialize(customerManager);
        gymStatsPanel.Initialize(gymState);
    }

    private void Update()
    {
        gymStatsPanel.UpdateStats();
    }
}