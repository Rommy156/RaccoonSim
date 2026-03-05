using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerController playerController;
    //skill points
    public int skillPoints = 0;
    //skill levels
    public int hungerEfficiencyLevel = 0;
    public int energyEfficiencyLevel = 0;
    public int movementBoostLevel = 0;
    //skill settings
    public float hungerDrainReductionPercent = 0.1f;
    public float energyDrainReductionPercent = 0.1f;
    public float movementSpeedIncrease = 1f;

    public void AddSkillPoint(int amount)
    {
        skillPoints += amount;
        Debug.Log("Skill Points:"+ skillPoints);
       
    }
    public void UpgradeHungerEfficiency()
    {
        if (skillPoints <= 0) return;
        hungerEfficiencyLevel++;
        skillPoints--;
        playerStats.hungerDrainRate *= (1f-hungerDrainReductionPercent);
        
    }
    public void UpgradeEnergyEfficiency()
    {
        if (skillPoints <= 0) return;
        energyEfficiencyLevel++;
        skillPoints--;
        playerStats.energyDrainRate *= (1f - energyDrainReductionPercent);
        
    }
    public void UpgradeMovement()
    {
        if (skillPoints <= 0) return;
        movementBoostLevel++;
        skillPoints--;

        playerController.moveSpeed += movementSpeedIncrease;
    

    }
}
