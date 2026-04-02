using UnityEngine;

[CreateAssetMenu(fileName = "CustomerType", menuName = "Scriptable Objects/CustomerType")]
public class CustomerType : ScriptableObject
{
    public string typeName;
    
    [Header("Behavior")]
    public int climbDurationTicks = 6;
    public float payMultiplier = 1f;
    public float spawnWeight = 1f;

    [Header("Requirements")]
    public int minWallQuality = 0;
    public int minReputation = 0;
}
