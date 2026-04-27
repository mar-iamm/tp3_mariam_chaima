using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentDay = 1;
    public int totalScore = 0;
    public int dayScore = 0;

    public int winScore = 500;

    public event Action<int> OnScoreChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetGame()
    {
        currentDay = 1;
        totalScore = 0;
        dayScore = 0;
        SceneManager.LoadScene("Intro");
    }

    public void AddScore(int amount)
    {
        dayScore += amount;
        totalScore += amount;
        OnScoreChanged?.Invoke(totalScore);
    }

    public void StartDay(int day)
    {
        currentDay = day;
        dayScore = 0;
        SceneManager.LoadScene("Day" + day);
    }

    public void EndDay()
    {
        if (currentDay == 1)
            SceneManager.LoadScene("DayResult");
        else if (currentDay == 2)
            SceneManager.LoadScene("DayResult2");
        else if (currentDay == 3)
            SceneManager.LoadScene("FinalResult");
    }

    public void GoNextDay()
    {
        currentDay++;

        if (currentDay <= 3)
            StartDay(currentDay);
        else
            SceneManager.LoadScene("FinalResult");
    }
}