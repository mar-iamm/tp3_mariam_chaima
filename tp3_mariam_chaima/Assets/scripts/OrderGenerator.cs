using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    public List<string> possibleItems = new List<string>()
    {
        "Coffee",
        "JuiceOrange",
        "JuiceLime",
        "DonutChocolate",
        "DonutStrawberry",
        "DonutGlazed",
        "CakeStrawberry",
        "CakeLime",
        "CakeBlueberry"
    };

    public int minItems = 1;
    public int maxItems = 3;

    public List<string> GenerateOrder()
    {
        List<string> order = new List<string>();

        int itemCount = Random.Range(minItems, maxItems + 1);

        for (int i = 0; i < itemCount; i++)
        {
            string randomItem = possibleItems[Random.Range(0, possibleItems.Count)];
            order.Add(randomItem);
        }

        return order;
    }
}