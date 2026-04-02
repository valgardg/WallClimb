using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Tick Settings")]
    private float tickInterval = 1.0f;
    private float tickTimer = 0f;

    [SerializeField] private int minutesPerTick = 10;

    private GameTime gameTime;
    private TimeSystem timeSystem;
    public UIManager uiManager;

    void Start()
    {
        gameTime = new GameTime();
        timeSystem = new TimeSystem();
        tickTimer = tickInterval / timeSystem.GameSpeed;

        uiManager.Initialize(timeSystem, gameTime);
    }

    void Update()
    {
        if (timeSystem.IsPaused) return;
        tickTimer -= Time.deltaTime;
        if (tickTimer <= 0f)
        {
            GameTick();
            tickTimer = tickInterval / timeSystem.GameSpeed;
        }
    }

    void GameTick()
    {
        gameTime.AdvanceTime(minutes: minutesPerTick); 
    }
}
