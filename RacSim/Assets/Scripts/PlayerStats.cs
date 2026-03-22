using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHunger = 100f;
    public float maxEnergy = 100f;
    public float hunger;
    public float energy;

    //DRAIN RATES
    public float hungerDrainRate = 1f;
    public float energyDrainRate = 2f; // ALWAYS 2x hunger

    //GAME DURATION
    public float gameDuration = 300f; // 5 min

    private float survivalTimer = 0f;

    private ClimbControllerRB climbController;

    // NPC effect
    private float npcDrainMultiplier = 1f;

    void Start()
    {
        climbController = GetComponent<ClimbControllerRB>();
        ResetStats();
    }

    void Update()
    {
        survivalTimer += Time.deltaTime;

        float difficultyMultiplier = 1f + (survivalTimer / gameDuration);

        float climbingMultiplier = (climbController != null && climbController.IsClimbing) ? 2f : 1f;

        // APPLY NPC EFFECT
        float finalEnergyDrain =
            energyDrainRate *
            difficultyMultiplier *
            climbingMultiplier *
            npcDrainMultiplier;

        float finalHungerDrain =
            hungerDrainRate *
            difficultyMultiplier;

        hunger -= finalHungerDrain * Time.deltaTime;
        energy -= finalEnergyDrain * Time.deltaTime;

        hunger = Mathf.Clamp(hunger, 0f, maxHunger);
        energy = Mathf.Clamp(energy, 0f, maxEnergy);
    }

    public void ResetStats()
    {
        energy = maxEnergy;          // FULL
        hunger = maxHunger * 0.5f;   // HALF
        survivalTimer = 0f;
    }

    public void Eat(float hungerAmount, float energyAmount)
    {
        hunger = Mathf.Clamp(hunger + hungerAmount, 0f, maxHunger);
        energy = Mathf.Clamp(energy + energyAmount, 0f, maxEnergy);
    }

    public void SetNPCMultiplier(float multiplier)
    {
        npcDrainMultiplier = multiplier;
    }

    public bool IsDead()
    {
        return hunger <= 0f || energy <= 0f;
    }
}