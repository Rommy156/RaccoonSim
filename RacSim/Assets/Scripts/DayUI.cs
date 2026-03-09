using TMPro;
using UnityEngine;

public class DayUI : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI dayText;
    void Update()
    {
        dayText.text = "Day:" + gameManager.currentDay;   
    }
} 