using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Customer : MonoBehaviour
{
    public float baseWaitTime = 30f;
    private float timer;

    public bool orderCompleted = false;

    public List<string> currentOrder = new List<string>();

    public OrderBubbleUI bubbleUI;
    public OrderGenerator orderGenerator;
    public CustomerMovement movement;
    public Transform counterTarget;

    public TrayOrderChecker trayChecker;

    void Start()
    {
        timer = baseWaitTime - ((GameManager.Instance.currentDay - 1) * 10f);

        if (timer < 10f)
            timer = 10f;

        StartCoroutine(CustomerFlow());
    }

    IEnumerator CustomerFlow()
    {
        if (movement != null && counterTarget != null)
            movement.MoveTo(counterTarget);

        yield return new WaitUntil(() => movement.HasReached());

        ShowOrder();

        if (trayChecker != null)
            trayChecker.SetCurrentCustomer(this);

        while (timer > 0 && !orderCompleted)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        if (!orderCompleted)
        {
            Debug.Log("Temps écoulé.");
            Leave();
        }
    }

    void ShowOrder()
    {
        if (orderGenerator != null)
            currentOrder = orderGenerator.GenerateOrder();

        if (bubbleUI != null)
            bubbleUI.DisplayOrder(currentOrder);
    }

    public void TryGiveItem(string itemName)
    {
        if (orderCompleted) return;

        if (currentOrder.Contains(itemName))
        {
            currentOrder.Remove(itemName);

            if (bubbleUI != null)
                bubbleUI.DisplayOrder(currentOrder);

            if (currentOrder.Count == 0)
            {
                CompleteOrder();
            }
        }
    }

    public void CompleteOrder()
    {
        orderCompleted = true;
        GameManager.Instance.AddScore(100);
        Leave();
    }

    void Leave()
    {
        if (movement != null)
            movement.StopMoving();

        Destroy(gameObject, 2f);

        DayManager dayManager = FindObjectOfType<DayManager>();
        if (dayManager != null)
            dayManager.CustomerFinished();
    }

    public float GetTimeRemaining()
    {
        return timer;
    }
}