using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    public void SetCustomer(Customer newCustomer)
    {
        // Timer désactivé pour la nouvelle version du jeu
        if (timerText != null)
            timerText.text = "";
    }
}