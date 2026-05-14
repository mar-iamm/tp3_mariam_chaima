using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalResultManager : MonoBehaviour
{
    public TextMeshPro finalText;

    void Start()
    {
        int score = GameManager.savedTotalScore;
        int winScore = 800;

        if (finalText == null)
        {
            Debug.LogError("Final Text n'est pas assigné dans FinalResultManager.");
            return;
        }

        if (score >= winScore)
        {
            finalText.text =
                "FIN DES 3 JOURNÉES\n\n" +
                "Score final : " + score + "\n\n" +
                "Félicitations, tu es embauché !";
        }
        else
        {
            finalText.text =
                "FIN DES 3 JOURNÉES\n\n" +
                "Score final : " + score + "\n\n" +
                "Tu n'as pas encore réussi à te faire embaucher.";
        }
    }

    public void Restart()
    {
        GameManager.savedCurrentDay = 1;
        GameManager.savedDayScore = 0;
        GameManager.savedTotalScore = 0;

        SceneManager.LoadScene("Intro");
    }

    public void Quit()
    {
        Application.Quit();
    }
}