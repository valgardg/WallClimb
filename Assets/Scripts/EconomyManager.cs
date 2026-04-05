using System;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    private GymState gymState;
    private float totalEarnedToday = 0f;
    private float totalSpentToday = 0f;

    [Header("Bankcruptcy Stats")]
    [SerializeField] private bool wasNegativeAtEndOfDay = false;
    public event Action OnBankruptcy;

    public float TotalEarnedToday => totalEarnedToday;
    public float TotalSpentToday => totalSpentToday;

    public void Initialize(GymState gymState, GameTime gameTime)
    {
        this.gymState = gymState;
        gameTime.OnStartOfDay += StartOfDayReset;
        gameTime.OnEndOfDay += EndOfDayCheck;
    }

    public void AddMoney(float amount)
    {
        gymState.cash += amount;
        totalEarnedToday += amount;
        Debug.Log($"Added ${amount}. New balance: ${gymState.cash}");
    }

    public bool TrySpendMoney(float amount)
    {
        if (gymState.cash < amount) return false;
        gymState.cash -= amount;
        totalSpentToday += amount;
        Debug.Log($"Spent ${amount}. New balance: ${gymState.cash}");
        return true;
    }

    public bool ForceSpendMoney(float amount)
    {
        gymState.cash -= amount;
        totalSpentToday += amount;
        Debug.Log($"Force spent ${amount}. New balance: ${gymState.cash}");
        return true;
    }

    private void EndOfDayCheck()
    {
        if (gymState.cash > 0) return;
        if (wasNegativeAtEndOfDay)
        {
            Debug.Log("Bankruptcy confirmed. Game Over.");
            OnBankruptcy?.Invoke();
        }
        else
        {
            Debug.LogWarning("Cash is negative at end of day. If this happens again, the game will end.");
            wasNegativeAtEndOfDay = true;
        }
    }

    private void StartOfDayReset()
    {
        totalEarnedToday = 0f;
        totalSpentToday = 0f;
    }
}
