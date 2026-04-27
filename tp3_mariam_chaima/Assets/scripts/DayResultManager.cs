using UnityEngine;
using TMPro;

public class DayResultManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;

    void Start()
    {
        resultText.text =
            "Journée " + GameManager.Instance.currentDay + " terminée !\n" +
            "Score de la journée : " + GameManager.Instance.dayScore + "\n" +
            "Score total : " + GameManager.Instance.totalScore;
    }

    public void NextDay()
    {
        GameManager.Instance.GoNextDay();
    }
}