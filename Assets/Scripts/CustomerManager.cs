using System.Collections.Generic;
using UnityEngine;

public enum CustomerState
{
    Arriving, // Customer has just entered the gym and is waiting to start climbing
    Climbing, // Customer is currently climbing the wall
    Leaving // Customer has finished climbing and is now leaving the gym
}

public class Customer {
    public CustomerType Type;
    public int TicksRemaining;
    public CustomerState State;

    public Customer(CustomerType customerType)
    {
        Type = customerType;
        TicksRemaining = customerType.climbDurationTicks;
    }
}

public class CustomerManager : MonoBehaviour
{
    private GymState gymState;
    [SerializeField] private CustomerType[] customerTypes;

    [Header("Spawning Tuning")]
    [SerializeField] private float baseSpawnChance = 0.3f;

    private List<Customer> customers = new();

    // UI readable values
    public IReadOnlyList<Customer> Customers => customers;
    public int ClimbingCount { get; private set; }
    public int DayCustomerCount { get; private set; }

    // Actions
    public event System.Action<float> OnCustomerPayment;

    public void Initialize(GymState gymState, GameTime gameTime, TickSystem tickSystem)
    {
        this.gymState = gymState;
        gameTime.OnStartOfDay += () => OnStartOfDay();
        tickSystem.OnTick += () => OnTick(gameTime.currentHour);
    }

    public void OnTick(int currentHour)
    {
        TickClimbingCustomers();
        TrySpawnCustomer(currentHour);
        UpdateCounts();
    }

    private void TrySpawnCustomer(int currentHour)
    {
        if (gymState.currentCustomers >= gymState.wallCapabity) return;
        float chance = CalculateSpawnChance(currentHour);

        if (Random.value > chance) return;

        var type = PickRandomEligibleCustomerType();
        if (type == null) return;

        var customer = new Customer(type);
        customers.Add(customer);
        gymState.currentCustomers++;
        DayCustomerCount++;

        OnCustomerPayment?.Invoke(gymState.entryFee);

        Debug.Log($"A new customer {type.name} has entered the gym. Total customers: {gymState.currentCustomers}");
    }

    private float CalculateSpawnChance(int currentHour)
    {
        // Reputation now scales [1.0 → 4.0] with steeper curve for high rep
        float repMultiplier = 1f + 3f * Mathf.Pow((gymState.reputation - 1f) / 9f, 0.7f);

        float timeMultiplier = Mathf.Clamp01(
            0.3f + 0.7f * Mathf.Sin(Mathf.PI * (currentHour - 6f) / 16f)
        );

        // Capacity now clamped to [1.0 → 2.0] — supportive, not dominant
        float normalizedCapacity = Mathf.Clamp01(gymState.wallCapabity / 10f);
        float capacityMultiplier = 1f + normalizedCapacity;

        return baseSpawnChance * repMultiplier * timeMultiplier * capacityMultiplier;
    }

    private CustomerType PickRandomEligibleCustomerType()
    {
        float totalWeight = 0f;
        foreach (var type in customerTypes)
        {
            if (type.minWallQuality <= gymState.wallQuality && type.minReputation <= gymState.reputation)
            {
                totalWeight += type.spawnWeight;
            }
        }

        if (totalWeight == 0f) return null;

        float randomValue = Random.value * totalWeight;
        float cumulative = 0f;

        foreach (var type in customerTypes)
        {
            if (type.minWallQuality > gymState.wallQuality || type.minReputation > gymState.reputation)
            {
                continue;
            }

            cumulative += type.spawnWeight;
            if (randomValue <= cumulative)
            {
                return type;
            }
        }
        return null;
    }

    private void TickClimbingCustomers()
    {
        for (int i = customers.Count - 1; i >= 0; i--)
        {
            Customer customer = customers[i];

            switch (customer.State)
            {
                case CustomerState.Arriving:
                    customer.State = CustomerState.Climbing;
                    break;
                case CustomerState.Climbing:
                    customer.TicksRemaining--;
                    if (customer.TicksRemaining <= 0)
                    {
                        customer.State = CustomerState.Leaving;
                        gymState.currentCustomers--;
                    }
                    break;
                case CustomerState.Leaving:
                    customers.RemoveAt(i);
                    Debug.Log($"A customer has left the gym. Total customers: {gymState.currentCustomers}");
                    break;
            }
        }
    }

    private void UpdateCounts()
    {
        ClimbingCount = 0;
        foreach (var customer in customers)
        {
            if (customer.State == CustomerState.Climbing)
            {
                ClimbingCount++;
            }
        }
    }

    private void OnStartOfDay()
    {
        DayCustomerCount = 0;   
    }
}
