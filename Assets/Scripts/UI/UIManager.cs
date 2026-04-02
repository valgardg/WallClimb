using UnityEngine;

public class UIManager : MonoBehaviour
{
    // uis 
    public TimePanel timePanel;
    public EndOfDayPanel endOfDayPanel;

    public void Initialize(GameTime gameTime)
    {
        timePanel.Initialize(gameTime);
        endOfDayPanel.Initialize(gameTime);
    }
}
