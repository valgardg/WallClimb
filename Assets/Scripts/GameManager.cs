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
    public GymManager GymManager;

    void Start()
    {
        // Initialize core systems
        gymState.ResetToDefaults();
        gameTime = new GameTime();
        tickSystem = new TickSystem();
        tickSystem.Initialize(gameTime);

        // Initialize managers with dependencies
        economyManager.Initialize(gymState, gameTime);
        customerManager.Initialize(gymState, gameTime, tickSystem);
        upgradeManager.Initialize(gymState, economyManager);
        GymManager.Initialize(gymState, gameTime, economyManager, upgradeManager);

        // Subscribe to events
        tickSystem.OnTick += () => gameTime.AdvanceTime(minutesPerTick);
        customerManager.OnCustomerPayment += (amount) => economyManager.AddMoney(amount);
        economyManager.OnBankruptcy += () => GameOver();

        // Finally initialize UI
        uiManager.Initialize(gameTime, customerManager, upgradeManager, gymState, economyManager);
    }

    void Update()
    {
        tickSystem.Tick(Time.deltaTime);
    }

    void GameOver()
    {
        gameTime.TogglePause();
    }
}
