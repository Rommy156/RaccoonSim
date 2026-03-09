//Energy system UI
using UnityEngine;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour
{
    public PlayerStats playerStats;
    public Slider energySlider;

    private void Update()
    {
        energySlider.value = playerStats.energy;
    }
}
