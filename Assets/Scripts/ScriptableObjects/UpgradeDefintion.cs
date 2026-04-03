using UnityEngine;

public enum UpgradeEffectType
{
    WallQuality,
    WallCapabity,
    Reputation,
    EntryFeeMultiplier,
    PassiveIncome

}

[System.Serializable]
public struct UpgradeEffect
{
    public UpgradeEffectType type;
    public float value;
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
    public UpgradeEffect[] effects;

    [Header("Progression")]
    public UpgradeDefintion[] prerequisites;
}
