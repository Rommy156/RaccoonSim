using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerSystem : MonoBehaviour
{
    public Slider hungerSlider;
    public float maxHunger = 100f;
    public float currentHunger;
    public float hungerDrainRate = 5f; // per second

    void Start()
    {
        currentHunger = maxHunger;
        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = currentHunger;
    }

    void Update()
    {
        DrainHunger();
        hungerSlider.value = currentHunger;

        if (currentHunger <= 0)
        {
            Debug.Log("Player Starved!");
        }
    }

    void DrainHunger()
    {
        currentHunger -= hungerDrainRate * Time.deltaTime;
        currentHunger = Mathf.Clamp(currentHunger, 0f, maxHunger);

        if (currentHunger < 30)
        {
            hungerSlider.fillRect.GetComponent<Image>().color = Color.red;
        }
    }

    public void AddHunger(float amount)
    {
        currentHunger += amount;
        currentHunger = Mathf.Clamp(currentHunger, 0f, maxHunger);
    }
}