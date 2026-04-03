using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private GymState gymState;
    [SerializeField] private UpgradeDefintion[] allUpgrades;

    private System.Collections.Generic.Dictionary<UpgradeDefintion, int> purchasedLevels = new();

    public bool CanPurchaseUpgrade(UpgradeDefintion upgrade)
    {
        int currentLevel = GetUpgradeLevel(upgrade);
        if (currentLevel >= upgrade.maxLevel) return false;

        // Check prerequisites
        foreach (var prereq in upgrade.prerequisites)
        {
            if (GetUpgradeLevel(prereq) < prereq.maxLevel) return false;
        }

        return gymState.cash >= upgrade.GetCostForLevel(currentLevel);
    }

    public void PurchaseUpgrade(UpgradeDefintion upgrade)
    {
        if (!CanPurchaseUpgrade(upgrade)) return;

        int currentLevel = GetUpgradeLevel(upgrade);
        float cost = upgrade.GetCostForLevel(currentLevel);
        gymState.cash -= cost;

        // Apply upgrade effect
        ApplyUpgradeEffect(upgrade);

        // Increment level
        purchasedLevels[upgrade] = currentLevel + 1;
    }

    private void ApplyUpgradeEffect(UpgradeDefintion upgrade)
    {
        switch (upgrade.effectType)
        {
            case UpgradeEffect.WallQuality:
                gymState.wallQuality += (int)upgrade.effectValue;
                break;
            case UpgradeEffect.WallCapabity:
                gymState.wallCapabity += (int)upgrade.effectValue;
                break;
            case UpgradeEffect.Reputation:
                gymState.reputation += upgrade.effectValue;
                break;
            case UpgradeEffect.EntryFeeMultiplier:
                gymState.entryFee *= upgrade.effectValue;
                break;
            case UpgradeEffect.PassiveIncome:
                // Handle passive income logic (e.g., add to daily earnings)
                break;
        }
    }

    private int GetUpgradeLevel(UpgradeDefintion upgrade)
    {
        return purchasedLevels.TryGetValue(upgrade, out int level) ? level : 0;
    }
}