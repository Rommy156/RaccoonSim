using UnityEngine;
public class PlayerStats : MonoBehaviour
{
    //max values
    public float maxHunger = 100f;
    public float maxEnergy = 100f;

    //current values
    public float hunger;
    public float energy;

    //drain per second
    public float hungerDrainRate = 1f;
    public float energyDrainRate = 1.5f;

    //low hunger penalty
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
    }

    void DrainStats()
    {
        hunger -= hungerDrainRate * Time.deltaTime;
        if (hunger <= lowHungerThreshold)
            energy -= (energyDrainRate + extraEnergyDrain) * Time.deltaTime;
        else
            energy -= energyDrainRate * Time.deltaTime;
        hunger = Mathf.Clamp(hunger, 0f, maxHunger);
        energy = Mathf.Clamp(energy, 0f, maxEnergy);


    }

    public void Eat(float hungerAmount, float energyAmount)
    {
        hunger += hungerAmount;
        energy += energyAmount;
        hunger = Mathf.Clamp(hunger, 0f, maxHunger);
        energy = Mathf.Clamp(energy, 0f, maxEnergy);

    }

    public void RestoreEnergy(float amount)
    {
        energy += amount;
        energy = Mathf.Clamp(energy, 0f, maxEnergy);
    }

    public bool IsDead()
    {
        return hunger <= 0f || energy <= 0f;


    }
}