using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
<<<<<<< Updated upstream
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
=======
    public PlayerStats playerStats;
    //skill points
    public int skillPoints = 0;
    //skill levels
    public int hungerEfficiencyLevel = 0;
    public int energyEfficiencyLevel = 0;
    public int movementBoostLevel = 0;
    //skill settings
    public float hungerDrainReduction = 0.2f;
    public float energyDrainReduction = 0.2f;
    public float movementSpeedIncrease = 1f;

    public void AddSkillPoint(int amount)
    {
        skillPoints += amount;
       
    }
    public void UpgradeHungerEfficiency()
    {
        if (skillPoints <= 0) return;
        hungerEfficiencyLevel++;
        skillPoints--;
        playerStats.hungerDrainRate -= hungerDrainReduction;
        playerStats.hungerDrainRate = Mathf.Max(0.1f, playerStats.hungerDrainRate);
    }
    public void UpgradeEnergyEfficiency()
    {
        if (skillPoints <= 0) return;
        energyEfficiencyLevel++;
        skillPoints--;
        playerStats.energyDrainRate -= energyDrainReduction;
        playerStats.energyDrainRate=Mathf.Max(0.1f,playerStats.energyDrainRate);
    }
    public void UpgradeMovement(PlayerMovement movement)
    {
        if (skillPoints <= 0) return;
        movementBoostLevel++;
>>>>>>> Stashed changes
    }
}
