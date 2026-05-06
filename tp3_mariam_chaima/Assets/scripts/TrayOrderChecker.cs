using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
public class TrayOrderChecker : MonoBehaviour
{
    private Customer currentCustomer;
    private List<GameObject> objectsOnTray = new List<GameObject>();
    public void SetCurrentCustomer(Customer customer)
    {
        currentCustomer = customer;
        CheckTray();
    }
    // ✅ AJOUT D'OBJET
    public void OnItemPlaced(SelectEnterEventArgs args)
    {
        GameObject placedObject = args.interactableObject.transform.gameObject;
        FoodItem food = placedObject.GetComponent<FoodItem>();
        if (food == null)
            food = placedObject.GetComponentInParent<FoodItem>();
        // 🔴 Ignore tout ce qui n’est pas un vrai item
        if (food == null)
        {
            Debug.Log("Objet ignoré (pas un FoodItem)");
            return;
        }
        GameObject foodObject = food.gameObject;
        if (!objectsOnTray.Contains(foodObject))
        {
            objectsOnTray.Add(foodObject);
            Debug.Log("Item ajouté : " + food.itemType);
        }
        CheckTray();
    }
    // ✅ RETRAIT D'OBJET
    public void OnItemRemoved(SelectExitEventArgs args)
    {
        GameObject removedObject = args.interactableObject.transform.gameObject;
        FoodItem food = removedObject.GetComponent<FoodItem>();
        if (food == null)
            food = removedObject.GetComponentInParent<FoodItem>();
        if (food == null)
            return;
        GameObject foodObject = food.gameObject;
        if (objectsOnTray.Contains(foodObject))
        {
            objectsOnTray.Remove(foodObject);
            Debug.Log("Item retiré : " + food.itemType);
        }
        CheckTray();
    }
    // ✅ CHECK COMMANDE (ENUM)
    void CheckTray()
    {
        if (currentCustomer == null)
        {
            GameManager.Instance.SetOrderReady(false);
            return;
        }
        List<ItemType> trayItems = new List<ItemType>();
        foreach (GameObject obj in objectsOnTray)
        {
            if (obj == null) continue;
            FoodItem food = obj.GetComponent<FoodItem>();
            if (food == null)
                food = obj.GetComponentInParent<FoodItem>();
            if (food != null)
                trayItems.Add(food.itemType);
        }
        bool correct = currentCustomer.CheckOrder(trayItems);
        Debug.Log("Commande correcte ? " + correct);
        GameManager.Instance.SetOrderReady(correct);
    }
    // ✅ CLEAR TRAY
    public void ClearTray()
    {
        List<GameObject> copy = new List<GameObject>(objectsOnTray);
        objectsOnTray.Clear();
        foreach (GameObject obj in copy)
        {
            if (obj != null)
                Destroy(obj);
        }
        GameManager.Instance.SetOrderReady(false);
    }
}