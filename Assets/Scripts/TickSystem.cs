using UnityEngine;
using System;

public class TickSystem
{
    [Header("Tick Settings")]
    private float tickInterval = 1.0f;
    private float tickTimer = 1.0f;
    private GameTime gameTime;

    public Action OnTick;

    public void Initialize(GameTime gameTime)
    {
        this.gameTime = gameTime;
    }

    public void Tick(float deltaTime)
    {
        if (gameTime.IsPaused) return;
        tickTimer -= deltaTime;
        if (tickTimer <= 0f)
        {
            OnTick?.Invoke();
            tickTimer = tickInterval / gameTime.GameSpeed;
        }
    }
}
