using UnityEngine;

public class UIManager : MonoBehaviour
{
    // uis 
    public TimePanel timePanel;
    public EndOfDayPanel endOfDayPanel;
    public CustomersPanel customersPanel;

    public void Initialize(GameTime gameTime, CustomerManager customerManager)
    {
        timePanel.Initialize(gameTime);
        endOfDayPanel.Initialize(gameTime, customerManager);
        customersPanel.Initialize(customerManager);
    }
}
