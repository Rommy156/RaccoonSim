using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    public PlayerStats playerStats;
    public Slider energySlider;

    void Start()
    {
        energySlider.value = playerStats.energy;   
    }
}
