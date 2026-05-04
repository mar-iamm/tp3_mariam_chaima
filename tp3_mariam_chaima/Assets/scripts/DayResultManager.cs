using UnityEngine;
using TMPro;

public class DayResultManager : MonoBehaviour
{
    public TextMeshPro resultText;

    void Start()
    {
        resultText.text =
            "Journée " + GameManager.Instance.currentDay + " terminée\n\n" +
            "Score de la journée : " + GameManager.Instance.dayScore + "\n" +
            "Score total : " + GameManager.Instance.totalScore;
    }

    public void Continue()
    {
        GameManager.Instance.GoNextDay();
    }
}