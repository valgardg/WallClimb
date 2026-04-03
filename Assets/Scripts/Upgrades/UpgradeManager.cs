using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private GymState gymState;
    [SerializeField] private UpgradeDefintion[] allUpgrades;

    [SerializeField] private UpgradeDefintion[] purchasedUpgrades;

    public bool CanPurchaseUpgrade(UpgradeDefintion upgrade)
    {
        // Check if already purchased
        if (System.Array.Exists(purchasedUpgrades, u => u == upgrade)) return false;
        // Check prerequisites
        foreach (var prereq in upgrade.prerequisites)
        {
            if (!System.Array.Exists(purchasedUpgrades, u => u == prereq)) return
                false;
        }
        // Check cost
        return gymState.cash >= upgrade.baseCost;
    }

    public void PurchaseUpgrade(UpgradeDefintion upgrade)
    {
        if (!CanPurchaseUpgrade(upgrade)) return;
        gymState.cash -= upgrade.baseCost;

        // Apply upgrade effect
        ApplyUpgradeEffect(upgrade);
        // Add to purchased upgrades
        purchasedUpgrades = purchasedUpgrades.Append(upgrade).ToArray();
    }

    private void ApplyUpgradeEffect(UpgradeDefintion upgrade)
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
                        // Handle passive income logic (e.g., add to daily earnings)
                        break;
                }
        }
    }
}