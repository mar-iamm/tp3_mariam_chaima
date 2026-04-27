using UnityEngine;
using System.Collections;

public class DayManager : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform spawnPoint;
    public Transform counterTarget;

    public OrderGenerator orderGenerator;
    public TrayOrderChecker trayChecker;

    public int customersPerDay = 3;
    private int customersFinished = 0;
    private bool spawning = false;

    void Start()
    {
        StartCoroutine(SpawnCustomer());
    }

    IEnumerator SpawnCustomer()
    {
        if (spawning) yield break;

        spawning = true;

        GameObject customerObj = Instantiate(customerPrefab, spawnPoint.position, spawnPoint.rotation);

        Customer customer = customerObj.GetComponent<Customer>();
        CustomerMovement movement = customerObj.GetComponent<CustomerMovement>();

        customer.orderGenerator = orderGenerator;
        customer.movement = movement;
        customer.counterTarget = counterTarget;
        customer.trayChecker = trayChecker;

        spawning = false;

        yield return null;
    }

    public void CustomerFinished()
    {
        customersFinished++;

        if (customersFinished >= customersPerDay)
        {
            GameManager.Instance.EndDay();
        }
        else
        {
            StartCoroutine(SpawnCustomer());
        }
    }
}