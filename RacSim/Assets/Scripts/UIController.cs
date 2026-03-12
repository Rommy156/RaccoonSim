using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameManager gameManager;

    public Slider energySlider;
    public Slider hungerSlider;

    public TextMeshProUGUI dayText;

    void Update()
    {
        if (gameManager != null)
            dayText.text = "Night: " + gameManager.currentDay;

        if (playerStats != null)
        {
            energySlider.value = playerStats.energy / playerStats.maxEnergy;
            hungerSlider.value = playerStats.hunger / playerStats.maxHunger;
        }
    }
}