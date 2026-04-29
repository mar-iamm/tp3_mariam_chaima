using UnityEngine;
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

    private void OnTriggerEnter(Collider other)
    {
        FoodItem food = other.GetComponent<FoodItem>();

        if (food == null)
            food = other.GetComponentInParent<FoodItem>();

        if (food == null)
            return;

        GameObject foodObject = food.gameObject;

        if (!objectsOnTray.Contains(foodObject))
            objectsOnTray.Add(foodObject);

        CheckTray();
    }

    private void OnTriggerExit(Collider other)
    {
        FoodItem food = other.GetComponent<FoodItem>();

        if (food == null)
            food = other.GetComponentInParent<FoodItem>();

        if (food == null)
            return;

        GameObject foodObject = food.gameObject;

        if (objectsOnTray.Contains(foodObject))
            objectsOnTray.Remove(foodObject);

        CheckTray();
    }

    void CheckTray()
    {
        if (currentCustomer == null)
        {
            GameManager.Instance.SetOrderReady(false);
            return;
        }

        List<string> trayItems = new List<string>();

        foreach (GameObject obj in objectsOnTray)
        {
            if (obj == null) continue;

            FoodItem food = obj.GetComponent<FoodItem>();

            if (food != null)
                trayItems.Add(food.itemName);
        }

        bool correct = currentCustomer.CheckOrder(trayItems);
        GameManager.Instance.SetOrderReady(correct);
    }

    public void ClearTray()
    {
        foreach (GameObject obj in objectsOnTray)
        {
            if (obj != null)
                Destroy(obj);
        }

        objectsOnTray.Clear();
        GameManager.Instance.SetOrderReady(false);
    }
}