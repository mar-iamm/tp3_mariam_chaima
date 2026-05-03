using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetupDayScene();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Day1")
        {
            SetupDayScene();
        }
    }

    void SetupDayScene()
    {
        customers = FindObjectsOfType<Customer>(true);
        trayChecker = FindObjectOfType<TrayOrderChecker>();

        currentCustomerIndex = -1;
        currentCustomer = null;
        customerActive = false;
        orderReady = false;
        customersCompleted = 0;
        dayScore = 0;

        foreach (Customer c in customers)
        {
            c.gameObject.SetActive(false);
        }

        OnScoreChanged?.Invoke(dayScore);
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

        if (trayChecker != null)
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

        if (trayChecker != null)
            trayChecker.ClearTray();

        if (customersCompleted >= 4)
        {
            SceneManager.LoadScene("DayResult");
        }
    }

    public void AddScore(int amount)
    {
        dayScore += amount;
        totalScore += amount;

        Debug.Log("Score journée : " + dayScore);
        Debug.Log("Score total : " + totalScore);

        OnScoreChanged?.Invoke(dayScore);
    }

    public void GoNextDay()
    {
        currentDay++;

        if (currentDay <= 3)
        {
            SceneManager.LoadScene("Day1");
        }
        else
        {
            SceneManager.LoadScene("FinalResult");
        }
    }

    public void RestartGame()
    {
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