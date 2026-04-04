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
    public EconomyManager economyManager;

    void Start()
    {
        // Initialize core systems
        gymState.ResetToDefaults();
        gameTime = new GameTime();
        tickSystem = new TickSystem();
        tickSystem.Initialize(gameTime);

        // Initialize managers with dependencies
        customerManager.Initialize(gymState, gameTime, tickSystem);
        economyManager.Initialize(gymState);
        upgradeManager.Initialize(gymState, economyManager);

        // Subscribe to tick events
        tickSystem.OnTick += () => gameTime.AdvanceTime(minutesPerTick);

        // Finally initialize UI
        uiManager.Initialize(gameTime, customerManager, upgradeManager, gymState);
    }

    void Update()
    {
        tickSystem.Tick(Time.deltaTime);
    }
}
