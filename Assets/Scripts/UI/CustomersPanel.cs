using TMPro;
using UnityEngine;

public class CustomersPanel : MonoBehaviour
{
    public TMP_Text climbingCountText;

    private CustomerManager customerManager;

    public void Initialize(CustomerManager customerManager)
    {
        this.customerManager = customerManager;
    }

    private void Update()
    {
        if (customerManager != null)
        {
            climbingCountText.text = $"Climbing customers: {customerManager.ClimbingCount}";
        }
    }
}
