using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GymState gymState;

    [SerializeField] private int minutesPerTick = 10;

    private GameTime gameTime;
    private TickSystem tickSystem;
    public UIManager uiManager;
    public CustomerManager customerManager;
    public UpgradeManager upgradeManager;

    void Start()
    {
        // Initialize game state and systems
        gymState.ResetToDefaults();
        gameTime = new GameTime();
        tickSystem = new TickSystem();
        tickSystem.Initialize(gameTime);
        uiManager.Initialize(gameTime, customerManager, upgradeManager);

        // Subscribe to tick events
        tickSystem.OnTick += () => gameTime.AdvanceTime(minutesPerTick);
        tickSystem.OnTick += () => customerManager.OnTick(gameTime.currentHour);
        gameTime.OnStartOfDay += () => customerManager.OnStartOfDay();

    }

    void Update()
    {
        tickSystem.Tick(Time.deltaTime);
    }
}
