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
    public Sprite cakeChocolate;
    public Sprite cakeLime;
    public Sprite cakeBlueberry;

    private Dictionary<string, Sprite> spriteDict;

    void Awake()
    {
        spriteDict = new Dictionary<string, Sprite>()
        {
            { "Coffee", coffee },
            { "JuiceOrange", juiceOrange },
            { "JuiceLime", juiceLime },
            { "DonutChocolate", donutChocolate },
            { "DonutStrawberry", donutStrawberry },
            { "DonutGlazed", donutGlazed },
            { "CakeChocolate", cakeChocolate },
            { "CakeLime", cakeLime },
            { "CakeBlueberry", cakeBlueberry }
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

            if (spriteDict.ContainsKey(item))
                img.sprite = spriteDict[item];
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