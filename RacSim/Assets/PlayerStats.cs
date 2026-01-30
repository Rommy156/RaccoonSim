using UnityEngine;
// This scripts for food system, rest system and day-night system
public class PlayerStats : MonoBehaviour
{
    [Header("Max Values")]
    public float maxHunger = 100f;
    public float maxEnergy = 100f;

    [Header("Current Values")]
    public float hunger;
    public float energy;

    [Header("Drain Per Second")]
    public float hungerDrainRate = 1f;
    public float energyDrainRate = 1.5f;
    [Header("Low Hunger Penalty")]
    public float lowHungerThreshold = 25f;
    public float extraEnergyDrain = 2f;

    void Start()
    {
        hunger = maxHunger;
        energy = maxEnergy;
    }

    void Update()
    {
        DrainStats();
        CheckDeath();
    }

    void DrainStats()
    {
        //hunger always drains
        hunger -= hungerDrainRate * Time.deltaTime;

        //energy drains faster when hungry
        if (hunger <= lowHungerThreshold)
        {
            energy -= (energyDrainRate + extraEnergyDrain) * Time.deltaTime;
        }

        else
        {
            energy -= energyDrainRate * Time.deltaTime;
        }

        hunger = Mathf.Clamp(hunger, 0f, maxHunger);
        energy = Mathf.Clamp(energy, 0f, maxEnergy);
    }

    void CheckDeath()
    {
        if (hunger <= 0f)
        {
            Debug.Log("Player died: Starved");
        }
        if (energy <= 0f)
        {
            Debug.Log("Player died:Exhausted");
        }
    }

    //called by food or garbage objects
    public void Eat(float hungerAmount, float energyAmount)
    {
        hunger += hungerAmount;
        energy += energyAmount;

        hunger = Mathf.Clamp(hunger, 0f, maxHunger);
        energy = Mathf.Clamp(energy, 0f, maxEnergy);
    }

    //used when resting
    public void RestoreEnergy(float amount)
    {
        energy += amount;
        energy = Mathf.Clamp(energy, 0f, maxEnergy);
    }
}