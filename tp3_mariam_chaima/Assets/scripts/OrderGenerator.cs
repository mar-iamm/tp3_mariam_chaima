using System.Collections.Generic;
using UnityEngine;
public class OrderGenerator : MonoBehaviour
{
    public List<ItemType> possibleItems = new List<ItemType>()
   {
       ItemType.Coffee,
       ItemType.JuiceOrange,
       ItemType.JuiceLime,
       ItemType.DonutChocolate,
       ItemType.DonutStrawberry,
       ItemType.DonutGlazed,
       ItemType.CakeStrawberry,
       ItemType.CakeLime,
       ItemType.CakeBlueberry
   };
    public int minItems = 1;
    public int maxItems = 3;
    public List<ItemType> GenerateOrder()
    {
        List<ItemType> order = new List<ItemType>();
        int itemCount = Random.Range(minItems, maxItems + 1);
        for (int i = 0; i < itemCount; i++)
        {
            ItemType randomItem = possibleItems[Random.Range(0, possibleItems.Count)];
            order.Add(randomItem);
        }
        return order;
    }
}