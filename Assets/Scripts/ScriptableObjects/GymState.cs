using UnityEngine;

[CreateAssetMenu(fileName = "GymState", menuName = "Scriptable Objects/GymState")]
public class GymState : ScriptableObject
{
    [Header("Gym Stats")]
    [Range(0, 10)]
    public int wallQuality = 1;

    public int wallCapacity = 2;

    [Header("Business")]
    public float entryFee = 5.0f;

    [Range(0f, 100f)]
    public float reputation = 10f;

    public int maxCustomersPerHour = 5;
}