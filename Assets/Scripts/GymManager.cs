using UnityEngine;

public class GymManager : MonoBehaviour
{
    GymState gymState;
    private EconomyManager economyManager;
    public void Initialize(GymState gymState, GameTime gameTime, EconomyManager economyManager, UpgradeManager upgradeManager)
    {
        this.gymState = gymState;
        this.economyManager = economyManager;
        gameTime.OnEndOfDay += HandleDayEnd;
        gameTime.OnEndOfWeek += HandleWeekEnd;
        upgradeManager.OnUpgradePurchased += (upgrade) => ApplyGymUpgrade(upgrade);
    }

    private void HandleDayEnd()
    {
        float totalDayExpenses = gymState.totalDailyUpkeep;
        economyManager.ForceSpendMoney(totalDayExpenses);
        float passiveIncome = gymState.passiveIncome;
        if (passiveIncome > 0)
        {
            economyManager.AddMoney(passiveIncome);
        }
    }

    private void HandleWeekEnd()
    {
        float totalDayExpenses = gymState.totalDailyUpkeep + gymState.rent;
        economyManager.ForceSpendMoney(totalDayExpenses);
    }

     private void ApplyGymUpgrade(UpgradeDefintion upgrade)
    {
        for (int i = 0; i < upgrade.effects.Length; i++)
        {
                var effect = upgrade.effects[i];
                switch (effect.type)
                {
                    case UpgradeEffectType.WallQuality:
                        gymState.wallQuality += (int)effect.value;
                        break;
                    case UpgradeEffectType.WallCapabity:
                        gymState.wallCapabity += (int)effect.value;
                        break;
                    case UpgradeEffectType.Reputation:
                        gymState.reputation += effect.value;
                        break;
                    case UpgradeEffectType.EntryFeeMultiplier:
                        gymState.entryFee *= effect.value;
                        break;
                    case UpgradeEffectType.PassiveIncome:
                        gymState.passiveIncome += effect.value;
                        break;
                }
        }

        gymState.totalDailyUpkeep += upgrade.dailyUpkeep;
    }
}
