using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void Initialize(EconomyManager economyManager)
    {
        economyManager.OnBankruptcy += ShowPanel;
        panel.SetActive(false);
    }
    private void ShowPanel()
    {
        panel.SetActive(true);
    }
}
