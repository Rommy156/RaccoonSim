using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameManager gameManager;
    //stats bars
    public Slider energySlider;
    public Slider hungerSlider;
    //text elements
    public TextMeshProUGUI dayText;

    void Update()
    {
        if (gameManager != null && dayText != null)
        {
            dayText.text = "NIGHT: " + gameManager.currentDay;
        }

        if (playerStats != null)
        {
            if (energySlider != null)
                energySlider.value = playerStats.energy / playerStats.maxEnergy;
            if (hungerSlider != null)
                hungerSlider.value = playerStats.hunger / playerStats.maxHunger;
        }
    }
}