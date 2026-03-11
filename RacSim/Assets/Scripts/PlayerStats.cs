using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHunger = 100f;
    public float maxEnergy = 100f;
    public float hunger;
    public float energy;
    public float hungerDrainRate = 0.6f;
    public float energyDrainRate = 0.8f;
    //5-15 minute loop 
    public float gameDuration = 300f;
    private float survivalTimer = 0f;

    private ClimbControllerRB climbController;

    void Start()
    {
        climbController = GetComponent<ClimbControllerRB>();
        ResetStats(); 
    }

    void Update()
    {
        survivalTimer += Time.deltaTime;
        float difficultyMultiplier = 1f + (survivalTimer / gameDuration) * 2f;
        //energy drains faster when climbing
        float climbingMultiplier = (climbController != null && climbController.IsClimbing) ? 2.5f : 1f;
        hunger -= hungerDrainRate * difficultyMultiplier * Time.deltaTime;
        energy -= energyDrainRate * difficultyMultiplier * climbingMultiplier * Time.deltaTime;
        hunger = Mathf.Clamp(hunger, 0f, maxHunger);
        energy = Mathf.Clamp(energy, 0f, maxEnergy);
    }
    public void ResetStats()
    {
        hunger = maxHunger;
        energy = maxEnergy;
        survivalTimer = 0f; 
        Debug.Log("Stats Reset: Player is fresh for the new night.");

    }
    public void Eat(float hAmount, float eAmount)
    {
        hunger = Mathf.Clamp(hunger + hAmount, 0f, maxHunger);
        energy = Mathf.Clamp(energy + eAmount, 0f, maxEnergy);
    }
    public bool IsDead()
    {
        return hunger <= 0f || energy <= 0f;

    }

}