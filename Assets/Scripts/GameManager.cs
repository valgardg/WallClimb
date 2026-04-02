using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GymState gymState;

    [SerializeField] private int minutesPerTick = 10;

    private GameTime gameTime;
    private TickSystem tickSystem;
    public UIManager uiManager;
    public CustomerManager customerManager;

    void Start()
    {
        gymState.ResetToDefaults();
        gameTime = new GameTime();
        tickSystem = new TickSystem();
        tickSystem.Initialize(gameTime);
        tickSystem.OnTick += () => gameTime.AdvanceTime(minutesPerTick);
        tickSystem.OnTick += customerManager.TrySpawnCustomer;

        uiManager.Initialize(gameTime);
    }

    void Update()
    {
        tickSystem.Tick(Time.deltaTime);
    }
}
