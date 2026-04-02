using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Tick Settings")]
    private float tickInterval = 1.0f;
    private float tickTimer = 0f;

    [SerializeField] private int minutesPerTick = 10;

    private GameTime gameTime;
    public UIManager uiManager;

    void Start()
    {
        gameTime = new GameTime();
        tickTimer = tickInterval / gameTime.GameSpeed;

        uiManager.Initialize(gameTime);
    }

    void Update()
    {
        if (gameTime.IsPaused) return;
        tickTimer -= Time.deltaTime;
        if (tickTimer <= 0f)
        {
            GameTick();
            tickTimer = tickInterval / gameTime.GameSpeed;
        }
    }

    void GameTick()
    {
        gameTime.AdvanceTime(minutes: minutesPerTick); 
    }
}
