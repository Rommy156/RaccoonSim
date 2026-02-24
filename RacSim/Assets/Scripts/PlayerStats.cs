using UnityEngine;
using UnityEngine.UI; // Make sure to import UnityEngine.UI for UI components

// This script handles food system, rest system, and day-night system
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

    [Header("UI Elements")]
    public Slider hungerSlider; // Reference to the Hunger Slider UI
    public Slider energySlider; // Reference to the Energy Slider UI

    [Header("Slider Colors")]
    public Color hungerColor = Color.red; // Set hunger color to red
    public Color energyColor = Color.yellow; // Set energy color to yellow

    void Start()
    {
        hunger = maxHunger;
        energy = maxEnergy;

        // Set the initial color of sliders
        SetSliderColor();
    }

    void Update()
    {
        DrainStats();
        CheckDeath();
        UpdateUI();
    }

    // Drain stats over time
    void DrainStats()
    {
        // Hunger always drains
        hunger -= hungerDrainRate * Time.deltaTime;

        // Energy drains faster when hungry
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

    // Check if the player died (starved or exhausted)
    void CheckDeath()
    {
        if (hunger <= 0f)
        {
            Debug.Log("Player died: Starved");
        }
        if (energy <= 0f)
        {
            Debug.Log("Player died: Exhausted");
        }
    }

    // Called by food or garbage objects
    public void Eat(float hungerAmount, float energyAmount)
    {
        hunger += hungerAmount;
        energy += energyAmount;

        hunger = Mathf.Clamp(hunger, 0f, maxHunger);
        energy = Mathf.Clamp(energy, 0f, maxEnergy);
    }

    // Used when resting
    public void RestoreEnergy(float amount)
    {
        energy += amount;
        energy = Mathf.Clamp(energy, 0f, maxEnergy);
    }

    // Update UI Sliders
    void UpdateUI()
    {
        if (hungerSlider != null)
        {
            hungerSlider.value = hunger / maxHunger; // Normalize hunger to 0-1 scale
        }

        if (energySlider != null)
        {
            energySlider.value = energy / maxEnergy; // Normalize energy to 0-1 scale
        }
    }

    // Set the colors of the sliders based on preset colors
    void SetSliderColor()
    {
        if (hungerSlider != null)
        {
            hungerSlider.fillRect.GetComponent<Image>().color = hungerColor; // Set hunger slider color
        }

        if (energySlider != null)
        {
            energySlider.fillRect.GetComponent<Image>().color = energyColor; // Set energy slider color
        }
    }
}