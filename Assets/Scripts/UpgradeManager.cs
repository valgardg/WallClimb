using System;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private GymState gymState;
    private EconomyManager economyManager;
    [SerializeField] private UpgradeDefintion[] availableUpgrades;

    [SerializeField] private UpgradeDefintion[] purchasedUpgrades;

    public Action<UpgradeDefintion> OnUpgradePurchased;

    public UpgradeDefintion[] AvailableUpgrades => availableUpgrades;

    public void Initialize(GymState gymState, EconomyManager economyManager)
    {
        this.gymState = gymState;
        this.economyManager = economyManager;
        LoadAvailableUpgrades();
    }

    public bool CanApplyUpgrade(UpgradeDefintion upgrade)
    {
        // Check if already purchased
        if (Array.Exists(purchasedUpgrades, u => u == upgrade)) return false;
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
        
        // Add to purchased upgrades
        purchasedUpgrades = purchasedUpgrades.Append(upgrade).ToArray();
        // Refresh available upgrades
        LoadAvailableUpgrades();
        // Announce purchase
        OnUpgradePurchased?.Invoke(upgrade);
    }

    public void LoadAvailableUpgrades()
    {
        UpgradeDefintion[] allUpgrades = Resources.LoadAll<UpgradeDefintion>("Upgrades");
        // check for each upgrade whether prerequisites are met and return list of unlocked upgrades
        availableUpgrades = allUpgrades.Where(CanApplyUpgrade).ToArray();

    }
}