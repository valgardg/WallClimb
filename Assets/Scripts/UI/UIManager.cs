using UnityEngine;

public class UIManager : MonoBehaviour
{
    // uis 
    public TimePanel timePanel;

    public void Initialize(TimeSystem timeSystem, GameTime gameTime)
    {
        timePanel.Initialize(timeSystem, gameTime);
    }
}
