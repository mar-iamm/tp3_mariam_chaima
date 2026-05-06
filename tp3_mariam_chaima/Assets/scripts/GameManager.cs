using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int savedCurrentDay = 1;
    public static int savedTotalScore = 0;
    [Header("Clients")]
    public Customer[] customers;
    private int currentCustomerIndex = -1;
    private Customer currentCustomer;
    [Header("Commande")]
    public TrayOrderChecker trayChecker;
    [Header("Score")]
    public int currentDay = 1;
    public int dayScore = 0;
    public int totalScore = 0;
    public int scorePerOrder = 100;
    public int winScore = 900;
    public event Action<int> OnScoreChanged;
    private bool customerActive = false;
    private bool orderReady = false;
    private int customersCompleted = 0;
    void Awake()
    {
        Instance = this;
        // 🔥 FIX PRINCIPAL : synchronisation immédiate du score
        totalScore = savedTotalScore;
        currentDay = savedCurrentDay;
    }
    void Start()
    {
        // 🔥 sécurité (évite toute désynchronisation)
        totalScore = savedTotalScore;
        currentDay = savedCurrentDay;
        Debug.Log("Start GameManager → totalScore = " + totalScore);
        Debug.Log("Start GameManager → savedTotalScore = " + savedTotalScore);
        if (SceneManager.GetActiveScene().name == "Day1")
            SetupDayScene();
    }
    void SetupDayScene()
    {
        customers = FindObjectsOfType<Customer>(true);
        trayChecker = FindObjectOfType<TrayOrderChecker>(true);
        currentCustomerIndex = -1;
        currentCustomer = null;
        customerActive = false;
        orderReady = false;
        customersCompleted = 0;
        dayScore = 0;
        foreach (Customer c in customers)
        {
            if (c != null)
                c.gameObject.SetActive(false);
        }
        Debug.Log("Journée " + currentDay + " initialisée. Clients trouvés : " + customers.Length);
        OnScoreChanged?.Invoke(dayScore);
    }
    public void CallNextCustomer()
    {
        if (customerActive)
        {
            Debug.Log("Un client est déjà actif.");
            return;
        }
        if (customers == null || customers.Length == 0)
        {
            Debug.LogError("Aucun client trouvé dans Day1.");
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
        if (trayChecker != null)
            trayChecker.SetCurrentCustomer(currentCustomer);
        customerActive = true;
        orderReady = false;
    }
    public void SetOrderReady(bool ready)
    {
        orderReady = ready;
        Debug.Log("Order ready = " + orderReady);
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
        if (trayChecker != null)
            trayChecker.ClearTray();
        if (customersCompleted >= 4)
            SceneManager.LoadScene("DayResult");
    }
    public void AddScore(int amount)
    {
        dayScore += amount;
        totalScore += amount;
        // 🔥 FIX IMPORTANT : toujours synchroniser
        savedTotalScore = totalScore;
        Debug.Log("Score journée : " + dayScore);
        Debug.Log("Score total : " + totalScore);
        Debug.Log("Score sauvegardé : " + savedTotalScore);
        OnScoreChanged?.Invoke(dayScore);
    }
    public void GoNextDay()
    {
        savedCurrentDay++;
        Debug.Log("Changement de jour → savedTotalScore = " + savedTotalScore);
        if (savedCurrentDay <= 3)
            SceneManager.LoadScene("Day1");
        else
            SceneManager.LoadScene("FinalResult");
    }
    public void RestartGame()
    {
        savedCurrentDay = 1;
        savedTotalScore = 0;
        currentDay = 1;
        dayScore = 0;
        totalScore = 0;
        SceneManager.LoadScene("Intro");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}