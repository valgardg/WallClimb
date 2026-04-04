using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    private GymState gymState;

    public void Initialize(GymState gymState)
    {
        this.gymState = gymState;
    }

    public void AddMoney(float amount)
    {
        gymState.cash += amount;
        Debug.Log($"Added ${amount}. New balance: ${gymState.cash}");
    }

    public bool TrySpendMoney(float amount)
    {
        if (gymState.cash < amount) return false;
        gymState.cash -= amount;
        Debug.Log($"Spent ${amount}. New balance: ${gymState.cash}");
        return true;
    }

    public void ProcessDailyExpenses()
    {
        float totalExpenses = 0f;
        gymState.cash -= totalExpenses;
        Debug.Log($"Processed daily expenses of ${totalExpenses}. New balance: ${gymState.cash}");
    }
}
