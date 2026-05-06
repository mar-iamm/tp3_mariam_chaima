using UnityEngine;
using System.Collections.Generic;
public class Customer : MonoBehaviour
{
    [Header("Commande")]
    public List<ItemType> currentOrder = new List<ItemType>();
    public OrderGenerator orderGenerator;
    public OrderBubbleUI bubbleUI;
    [Header("Animations")]
    public Animator animator;
    public string enterTrigger = "Enter";
    public string leaveTrigger = "Leave";
    public bool orderCompleted = false;
    public void StartCustomer()
    {
        orderCompleted = false;
        Debug.Log("StartCustomer appelé sur : " + gameObject.name);
        if (orderGenerator != null)
        {
            currentOrder = orderGenerator.GenerateOrder();
            // 🔥 affichage propre enum
            Debug.Log("Commande générée : " + string.Join(", ", currentOrder));
        }
        else
        {
            Debug.LogError("OrderGenerator manquant sur " + gameObject.name);
        }
        if (bubbleUI != null)
        {
            Debug.Log("BubbleUI trouvée sur : " + bubbleUI.gameObject.name);
            bubbleUI.DisplayOrder(currentOrder);
        }
        else
        {
            Debug.LogError("BubbleUI manquante sur " + gameObject.name);
        }
        if (animator != null)
            animator.SetTrigger(enterTrigger);
    }
    // ✅ VERSION ENUM
    public bool CheckOrder(List<ItemType> trayItems)
    {
        if (trayItems.Count != currentOrder.Count)
            return false;
        List<ItemType> orderCopy = new List<ItemType>(currentOrder);
        foreach (ItemType item in trayItems)
        {
            if (orderCopy.Contains(item))
                orderCopy.Remove(item);
            else
                return false;
        }
        return orderCopy.Count == 0;
    }
    public void Leave()
    {
        orderCompleted = true;
        if (bubbleUI != null)
            bubbleUI.DisplayOrder(new List<ItemType>());
        if (animator != null)
            animator.SetTrigger(leaveTrigger);
        Invoke(nameof(HideCustomer), 2f);
    }
    void HideCustomer()
    {
        gameObject.SetActive(false);
    }
}