using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public UpgradePrefab upgradePrefab;
    public Transform upgradeListContainer;
    private UpgradeManager upgradeManager;

    public void Initialize(UpgradeManager upgradeManager)
    {
        this.upgradeManager = upgradeManager;
        upgradeManager.OnUpgradePurchased += upgrade => RefreshUpgradeList();
        LoadUpgrades();
    }

    private void LoadUpgrades()
    {
        UpgradeDefintion[] availableUpgrades = upgradeManager.AvailableUpgrades;
        Debug.Log($"Loaded {availableUpgrades.Length} upgrades from Resources/Upgrades");
        foreach (var upgrade in availableUpgrades)
        {
            UpgradePrefab prefabInstance = Instantiate(upgradePrefab, upgradeListContainer);
            prefabInstance.Initialize(upgrade);
            prefabInstance.OnUpgradeClicked += upgradeManager.PurchaseUpgrade;
        }
        // Resolve all layouts after population is complete
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(upgradeListContainer.GetComponent<RectTransform>());
    }

    private void RemoveUpgradePrefab(UpgradeDefintion purchasedUpgrade)
    {
        foreach (Transform child in upgradeListContainer)
        {
            UpgradePrefab prefab = child.GetComponent<UpgradePrefab>();
            if (prefab != null && prefab.UpgradeData == purchasedUpgrade)
            {
                Destroy(prefab.gameObject);
                break;
            }
        }
        // Resolve all layouts after removal
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(upgradeListContainer.GetComponent<RectTransform>());
    }

    private void RefreshUpgradeList()
    {
        // Clear existing prefabs
        foreach (Transform child in upgradeListContainer)
        {
            Destroy(child.gameObject);
        }
        // Reload upgrades
        LoadUpgrades();
    }
}
