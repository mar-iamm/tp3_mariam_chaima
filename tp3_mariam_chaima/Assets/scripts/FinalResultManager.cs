using UnityEngine;
using TMPro;

public class FinalResultManager : MonoBehaviour
{
    public TextMeshPro finalText;

    void Start()
    {
        int score = GameManager.Instance.totalScore;   
        int winScore = GameManager.Instance.winScore;

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
        GameManager.Instance.RestartGame();
    }

    public void Quit()
    {
        GameManager.Instance.QuitGame();
    }
}