using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TrayOrderChecker : MonoBehaviour
{
    private Customer currentCustomer;

    public void SetCurrentCustomer(Customer customer)
    {
        currentCustomer = customer;
    }

    public void OnItemPlaced(SelectEnterEventArgs args)
    {
        GameObject placedObject = args.interactableObject.transform.gameObject;

        FoodItem food = placedObject.GetComponent<FoodItem>();

        if (food == null)
        {
            Debug.Log("Objet sans FoodItem.");
            return;
        }

        if (currentCustomer != null)
        {
            currentCustomer.TryGiveItem(food.itemName);
            Destroy(placedObject);
        }
    }
}