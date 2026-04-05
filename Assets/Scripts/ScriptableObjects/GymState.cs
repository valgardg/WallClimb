using UnityEngine;

[CreateAssetMenu(fileName = "GymState", menuName = "Scriptable Objects/GymState")]
public class GymState : ScriptableObject
{
    [Header("Finances")]
    public float cash = 500f;
    public float todayEarnings = 0f;
    public float todayExpenses = 0f;
    public float rent = 100f;
    public float totalDailyUpkeep = 0f;

    [Header("Gym Stats")]
    public int wallQuality = 1;
    public int wallCapabity = 1;
    public float reputation = 1.0f;
    public float entryFee = 10f;
    public float passiveIncome = 0f;

    [Header("Customers")]
    public int currentCustomers = 0;

    public void ResetToDefaults()
    {
        // cash = 500f;
        todayEarnings = 0f;
        todayExpenses = 0f;
        rent = 100f;
        totalDailyUpkeep = 0f;
        wallQuality = 1;
        wallCapabity = 1;
        reputation = 1.0f;
        entryFee = 10f;
        passiveIncome = 0f;
        currentCustomers = 0;
    }
}