using UnityEngine;
public class GameManager : MonoBehaviour
{
    [Header("Game State")]
    public bool isGameActive = true;
    [Header("Day System")]
    public int currentDay = 1;
    public int maxDays = 5;
    [Header("References")]
    public PlayerStats playerStats;

    void Update()
    {
        if (!isGameActive)
            return;


        CheckGameOver();
        CheckWinCondition();

    }

    void CheckGameOver()
    {
        if (playerStats == null)
            return;
        if (playerStats.IsDead())
        {
            isGameActive = false;
            Debug.Log("Game Over");

        }

    }

    void CheckWinCondition()
    {
        if (currentDay > maxDays)
        {
            isGameActive = false;
            Debug.Log("You Survived!");
        }
    }

    public void AdvanceDay()
    {
        if (!isGameActive)
            return;
        currentDay++;
        Debug.Log("Day: " + currentDay);

    }

}