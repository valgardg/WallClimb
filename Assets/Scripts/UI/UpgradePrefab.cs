using TMPro;
using UnityEngine;
using System;

public class UpgradePrefab : MonoBehaviour
{
    [SerializeField] private TMP_Text upgradeNameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text effectText;
    [SerializeField] private TMP_Text effectValueText;
    [SerializeField] private TMP_Text costText;

    private UpgradeDefintion upgrade;
    public Action<UpgradeDefintion> OnUpgradeClicked;

    public void Initialize(UpgradeDefintion upgrade)
    {
        this.upgrade = upgrade;
        upgradeNameText.text = upgrade.upgradeName;
        descriptionText.text = upgrade.description;
        effectText.text = upgrade.effectType.ToString();
        effectValueText.text = $"+{upgrade.effectValue}";
        costText.text = $"${upgrade.baseCost}";
    }

    public void BuyUpgrade()
    {
        OnUpgradeClicked?.Invoke(this.upgrade);
    }
}
