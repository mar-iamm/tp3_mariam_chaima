using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public Transform counterPoint;
    public List<Transform> queuePoints = new List<Transform>();

    public List<Customer> customers = new List<Customer>();

    public void AddCustomer(Customer customer)
    {
        if (!customers.Contains(customer))
        {
            customers.Add(customer);
            UpdateQueue();
        }
    }

    public void RemoveCustomer(Customer customer)
    {
        if (customers.Contains(customer))
        {
            customers.Remove(customer);
            UpdateQueue();
        }
    }

    public void UpdateQueue()
    {
        for (int i = 0; i < customers.Count; i++)
        {
            Customer c = customers[i];

            if (c == null || c.movement == null) continue;

            Transform target;

            if (i == 0)
                target = counterPoint;
            else
                target = queuePoints[i - 1];

            c.movement.MoveTo(target);
        }
    }
}