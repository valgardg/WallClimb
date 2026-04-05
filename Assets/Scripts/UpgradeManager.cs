using System;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private GymState gymState;
    private EconomyManager economyManager;
    [SerializeField] private UpgradeDefintion[] allUpgrades;

    [SerializeField] private UpgradeDefintion[] purchasedUpgrades;

    public Action<UpgradeDefintion> OnUpgradePurchased;

    public void Initialize(GymState gymState, EconomyManager economyManager)
    {
        this.gymState = gymState;
        this.economyManager = economyManager;
    }

    public bool CanApplyUpgrade(UpgradeDefintion upgrade)
    {
        // Check if already purchased
        if (System.Array.Exists(purchasedUpgrades, u => u == upgrade)) return false;
        // Check prerequisites
        foreach (var prereq in upgrade.prerequisites)
        {
            if (!Array.Exists(purchasedUpgrades, u => u == prereq)) return
                false;
        }
        return true;
    }

    public void PurchaseUpgrade(UpgradeDefintion upgrade)
    {
        if (!CanApplyUpgrade(upgrade)) return;
        
        // try to spend money
        if (!economyManager.TrySpendMoney(upgrade.baseCost)) return;
        // Announce purchase
        OnUpgradePurchased?.Invoke(upgrade);
        // Add to purchased upgrades
        purchasedUpgrades = purchasedUpgrades.Append(upgrade).ToArray();
    }
}