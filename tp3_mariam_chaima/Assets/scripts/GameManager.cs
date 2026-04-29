using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Clients")]
    public Customer[] customers;
    private int currentCustomerIndex = -1;
    private Customer currentCustomer;

    [Header("Commande")]
    public TrayOrderChecker trayChecker;

    [Header("Score")]
    public int totalScore = 0;
    public int scorePerOrder = 100;
    public int winScore = 300;
    public event Action<int> OnScoreChanged;

    [Header("UI")]
    public TextMeshProUGUI resultText;
    public GameObject resultPanel;

    private bool customerActive = false;
    private bool orderReady = false;
    private int customersCompleted = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        resultPanel.SetActive(false);

        foreach (Customer c in customers)
        {
            c.gameObject.SetActive(false);
        }

        OnScoreChanged?.Invoke(totalScore);
    }

    public void CallNextCustomer()
    {
        if (customerActive)
        {
            Debug.Log("Un client est déjà actif.");
            return;
        }

        if (currentCustomerIndex >= customers.Length - 1)
        {
            Debug.Log("Tous les clients sont déjà passés.");
            return;
        }

        currentCustomerIndex++;
        currentCustomer = customers[currentCustomerIndex];

        currentCustomer.gameObject.SetActive(true);
        currentCustomer.StartCustomer();

        trayChecker.SetCurrentCustomer(currentCustomer);

        customerActive = true;
        orderReady = false;
    }

    public void SetOrderReady(bool ready)
    {
        orderReady = ready;
    }

    public void GiveOrder()
    {
        if (!customerActive || currentCustomer == null)
        {
            Debug.Log("Aucun client actif.");
            return;
        }

        if (!orderReady)
        {
            Debug.Log("Commande incorrecte ou incomplète.");
            return;
        }

        AddScore(scorePerOrder);

        currentCustomer.Leave();

        customerActive = false;
        orderReady = false;
        customersCompleted++;

        trayChecker.ClearTray();

        if (customersCompleted >= customers.Length)
        {
            EndGame();
        }
    }

    public void AddScore(int amount)
    {
        totalScore += amount;
        OnScoreChanged?.Invoke(totalScore);
    }

    void EndGame()
    {
        resultPanel.SetActive(true);

        if (totalScore >= winScore)
        {
            resultText.text = 
                "Fin des 4 clients\n" +
                "Score final : " + totalScore + "\n" +
                "Félicitations, tu es embauché !";
        }
        else
        {
            resultText.text = 
                "Fin des 4 clients\n" +
                "Score final : " + totalScore + "\n" +
                "Tu n'as pas encore prouvé que tu peux être embauché.";
        }
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Day1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}