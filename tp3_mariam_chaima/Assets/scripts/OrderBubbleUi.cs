using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class OrderBubbleUI : MonoBehaviour
{
    public GameObject itemUIPrefab;
    public Transform container;
    [Header("Sprites")]
    public Sprite coffee;
    public Sprite juiceOrange;
    public Sprite juiceLime;
    public Sprite donutChocolate;
    public Sprite donutStrawberry;
    public Sprite donutGlazed;
    public Sprite cakeStrawberry;
    public Sprite cakeLime;
    public Sprite cakeBlueberry;
    private Dictionary<ItemType, Sprite> spriteDict;
    void Awake()
    {
        spriteDict = new Dictionary<ItemType, Sprite>()
       {
           { ItemType.Coffee, coffee },
           { ItemType.JuiceOrange, juiceOrange },
           { ItemType.JuiceLime, juiceLime },
           { ItemType.DonutChocolate, donutChocolate },
           { ItemType.DonutStrawberry, donutStrawberry },
           { ItemType.DonutGlazed, donutGlazed },
           { ItemType.CakeStrawberry, cakeStrawberry },
           { ItemType.CakeLime, cakeLime },
           { ItemType.CakeBlueberry, cakeBlueberry }
       };
    }
    public void DisplayOrder(List<ItemType> items)
    {
        foreach (Transform child in container)
            Destroy(child.gameObject);
        foreach (ItemType item in items)
        {
            GameObject ui = Instantiate(itemUIPrefab, container);
            Image img = ui.GetComponent<Image>();
            if (img == null)
            {
                Debug.LogError("Image manquante sur prefab UI");
                continue;
            }
            if (!spriteDict.ContainsKey(item))
            {
                Debug.LogError("Item non trouvé dans dictionnaire : " + item);
                continue;
            }
            Sprite sprite = spriteDict[item];
            if (sprite == null)
            {
                Debug.LogError("Sprite NULL pour : " + item);
                continue;
            }
            img.sprite = sprite;
            img.color = Color.white;
            img.preserveAspect = true;
            Debug.Log("Affiché : " + item);
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