using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OrderBubbleUI : MonoBehaviour
{
    public GameObject itemUIPrefab;
    public Transform container;

    public Sprite coffee;
    public Sprite juiceOrange;
    public Sprite juiceLime;
    public Sprite donutChocolate;
    public Sprite donutStrawberry;
    public Sprite donutGlazed;
    public Sprite cakeStrawberry;
    public Sprite cakeLime;
    public Sprite cakeBlueberry;

    private Dictionary<string, Sprite> spriteDict;

    void Awake()
    {
        spriteDict = new Dictionary<string, Sprite>()
        {
            { "Coffee", coffee },
            { "coffee", coffee },

            { "JuiceOrange", juiceOrange },
            { "Juice_Orange", juiceOrange },
            { "orangeDrink", juiceOrange },
            { "orangedrink", juiceOrange },

            { "JuiceLime", juiceLime },
            { "Juice_Lime", juiceLime },
            { "limeDrink", juiceLime },
            { "limedrink", juiceLime },

            { "DonutChocolate", donutChocolate },
            { "DonutChoco", donutChocolate },
            { "chocolateDonut", donutChocolate },
            { "chocolatedonut", donutChocolate },

            { "DonutStrawberry", donutStrawberry },
            { "DonutStraw", donutStrawberry },
            { "strawberryDonut", donutStrawberry },
            { "strawberrydonut", donutStrawberry },

            { "DonutGlazed", donutGlazed },
            { "glazedDonut", donutGlazed },
            { "glazeddonut", donutGlazed },

            { "CakeStrawberry", cakeStrawberry },
            { "CakeStrawb", cakeStrawberry },
            { "strawberryCake", cakeStrawberry },
            { "strawberry cake", cakeStrawberry },

            { "CakeLime", cakeLime },
            { "limeCake", cakeLime },
            { "limecake", cakeLime },

            { "CakeBlueberry", cakeBlueberry },
            { "CakeBlueber", cakeBlueberry },
            { "blueberryCake", cakeBlueberry },
            { "blueberrycake", cakeBlueberry }
        };
    }

    public void DisplayOrder(List<string> items)
    {
        foreach (Transform child in container)
            Destroy(child.gameObject);

        foreach (string item in items)
        {
            GameObject ui = Instantiate(itemUIPrefab, container);
            Image img = ui.GetComponent<Image>();

            if (img != null && spriteDict.ContainsKey(item))
            {
                img.sprite = spriteDict[item];
                img.color = Color.white;
                img.preserveAspect = true;
            }
            else
            {
                Debug.LogWarning("Sprite ou Image manquant pour : " + item);
            }
        }
    }
    
    void Update()
    {
        if (Camera.main != null)
        {
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0);
        }
    }
}