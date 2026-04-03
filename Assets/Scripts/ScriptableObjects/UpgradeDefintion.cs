using UnityEngine;

public enum UpgradeEffect
{
    WallQuality,
    WallCapabity,
    Reputation,
    EntryFeeMultiplier,
    PassiveIncome

}

[CreateAssetMenu(fileName = "UpgradeDefintion", menuName = "Scriptable Objects/UpgradeDefintion")]
public class UpgradeDefintion : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;

    [Header("Cost")]
    public float baseCost = 100f;
    public float dailyUpkeep = 0f;

    [Header("Effect")]
    public UpgradeEffect effectType;
    public float effectValue;

    [Header("Progression")]
    public int maxLevel = 1;
    public float costMultiplierPerLevel = 1.5f;
    public UpgradeDefintion[] prerequisites;

    public float GetCostForLevel(int level)
    {
        return baseCost * Mathf.Pow(costMultiplierPerLevel, level);
    }
}
