using UnityEngine;
using TMPro;

public class FinalManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;

    void Start()
    {
        int score = GameManager.Instance.totalScore;
        int winScore = GameManager.Instance.winScore;

        if (score >= winScore)
        {
            resultText.text =
                "Félicitations !\n" +
                "Score final : " + score + "\n" +
                "Tu es embauché !";
        }
        else
        {
            resultText.text =
                "Dommage...\n" +
                "Score final : " + score + "\n" +
                "Tu n'as pas été embauché.";
        }
    }

    public void Restart()
    {
        GameManager.Instance.ResetGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}