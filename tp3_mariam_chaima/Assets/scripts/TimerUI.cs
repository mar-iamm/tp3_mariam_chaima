using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Customer customer;

    void Update()
    {
        if (customer == null)
        {
            timerText.text = "";
            return;
        }

        timerText.text = Mathf.Ceil(customer.GetTimeRemaining()).ToString();
    }

    public void SetCustomer(Customer newCustomer)
    {
        customer = newCustomer;
    }
}