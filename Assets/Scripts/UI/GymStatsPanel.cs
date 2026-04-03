using TMPro;
using UnityEngine;

public class GymStatsPanel : MonoBehaviour
{
    private GymState gymState;

    public TMP_Text gymCashText;

    [Header("Gym Stats UI Elements")]
    public TMP_Text wallQualityText;
    public TMP_Text wallCapacityText;
    public TMP_Text reputationText;
    public TMP_Text entryFeeText;

    public void Initialize(GymState gymState)
    {
        this.gymState = gymState;
    }

    public void UpdateStats()
    {
        if (gymState != null)
        {
            gymCashText.text = $"Cash: ${gymState.cash:F2}";
            wallQualityText.text = $"WQ: {gymState.wallQuality}";
            wallCapacityText.text = $"WC: {gymState.wallCapabity}";
            reputationText.text = $"RP: {gymState.reputation}";
            entryFeeText.text = $"EF: ${gymState.entryFee:F2}";
        }
    }
}
