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

    public void OnItemPlaced(SelectEnterEventArgs args)
    {
        GameObject placedObject = args.interactableObject.transform.gameObject;

        FoodItem food = placedObject.GetComponent<FoodItem>();

        if (food == null)
            food = placedObject.GetComponentInParent<FoodItem>();

        if (food == null)
        {
            Debug.LogWarning("Objet placé sans FoodItem : " + placedObject.name);
            return;
        }

        GameObject foodObject = food.gameObject;

        if (!objectsOnTray.Contains(foodObject))
            objectsOnTray.Add(foodObject);

        Debug.Log("Item ajouté au cabaret : " + food.itemName);

        CheckTray();
    }

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
            objectsOnTray.Remove(foodObject);

        Debug.Log("Item retiré du cabaret : " + food.itemName);

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

            if (food == null)
                food = obj.GetComponentInParent<FoodItem>();

            if (food != null)
                trayItems.Add(food.itemName);
        }

        bool correct = currentCustomer.CheckOrder(trayItems);

        Debug.Log("Commande correcte ? " + correct);

        GameManager.Instance.SetOrderReady(correct);
    }

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