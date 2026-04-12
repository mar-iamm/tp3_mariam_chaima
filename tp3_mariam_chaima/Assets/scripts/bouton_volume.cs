using UnityEngine;
using UnityEngine.UI;

public class ToggleImageButton : MonoBehaviour
{
    public Image targetImage;     // The Image component to change
    public Sprite onSprite;
    public Sprite offSprite;

    public bool isOn = true;

    void Start()
    {
        UpdateVisual();
    }

    public void Toggle()
    {
        isOn = !isOn;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (isOn)
            targetImage.sprite = onSprite;
        else
            targetImage.sprite = offSprite;
    }
}