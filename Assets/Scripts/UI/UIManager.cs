using UnityEngine;

public class UIManager : MonoBehaviour
{
    // uis 
    public TimePanel timePanel;
    public EndOfDayPanel endOfDayPanel;
    public CustomersPanel customersPanel;
    public UpgradePanel upgradePanel;

    public void Initialize(GameTime gameTime, CustomerManager customerManager, UpgradeManager upgradeManager)
    {
        timePanel.Initialize(gameTime);
        endOfDayPanel.Initialize(gameTime, customerManager);
        upgradePanel.Initialize(upgradeManager);
        customersPanel.Initialize(customerManager);
    }
}
