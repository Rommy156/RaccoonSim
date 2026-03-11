//Allen Adepoju
//000948096
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HungerSystem : MonoBehaviour
{
    public Slider hungerSlider;
    public float maxHunger = 100f;
    public float currentHunger;
    public float hungerDrainRate = 5f; // per second
    public GameObject starvedText;

    void Start()
    {
        starvedText.SetActive(false);

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
            starvedText.SetActive(true);
            Invoke("LoadGameOver", 2f);
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
    void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}