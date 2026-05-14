using UnityEngine;
using TMPro;

public class DayResultManager : MonoBehaviour
{
    public TextMeshPro resultText;

    void Start()
    {
        resultText.text =
            "Journee " + GameManager.savedCurrentDay + " terminee\n\n" +
            "Score de la journée : " + GameManager.savedDayScore + "\n" +
            "Score total : " + GameManager.savedTotalScore;
    }

    public void Continue()
    {
        GameManager.Instance.GoNextDay();
    }
}