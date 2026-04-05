using TMPro;
using UnityEngine;
using System;

public class UpgradePrefab : MonoBehaviour
{
    [SerializeField] private TMP_Text upgradeNameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Transform effectList;
    [SerializeField] private TMP_Text effectTextPrefab;
    [SerializeField] private TMP_Text dailyCostText;
    [SerializeField] private TMP_Text costText;

    private UpgradeDefintion upgrade;
    public UpgradeDefintion UpgradeData => upgrade;
    public Action<UpgradeDefintion> OnUpgradeClicked;

    public void Initialize(UpgradeDefintion upgrade)
    {
        this.upgrade = upgrade;
        upgradeNameText.text = upgrade.upgradeName;
        descriptionText.text = upgrade.description;
        // display each upgrade effect with type and value
        for (int i = 0; i < upgrade.effects.Length; i++)
        {
            var effect = upgrade.effects[i];
            var effectText = Instantiate(effectTextPrefab, effectList);
            effectText.text = $"{effect.type}: +{effect.value}";
        }
        dailyCostText.text = $"Daily Cost: ${upgrade.dailyUpkeep}";
        costText.text = $"${upgrade.baseCost}";
    }

    public void BuyUpgrade()
    {
        OnUpgradeClicked?.Invoke(this.upgrade);
    }
}
