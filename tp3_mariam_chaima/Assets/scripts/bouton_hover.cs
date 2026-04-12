using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverScale : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f);
    public float speed = 10f;

    [Header("Color Settings")]
    public float darkenAmount = 0.8f; // 1 = normal, <1 = darker
    public float colorSpeed = 10f;

    private Vector3 normalScale;
    private Vector3 targetScale;

    private Image image;
    private Color normalColor;
    private Color targetColor;

    void Start()
    {
        normalScale = transform.localScale;
        targetScale = normalScale;

        image = GetComponent<Image>();
        if (image != null)
        {
            normalColor = image.color;
            targetColor = normalColor;
        }
    }

    void Update()
    {
        // Scale animation
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * speed
        );

        // Color animation
        if (image != null)
        {
            image.color = Color.Lerp(
                image.color,
                targetColor,
                Time.deltaTime * colorSpeed
            );
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = hoverScale;

        if (image != null)
        {
            targetColor = normalColor * darkenAmount;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = normalScale;

        if (image != null)
        {
            targetColor = normalColor;
        }
    }
}