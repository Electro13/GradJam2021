using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;

    private TextMeshProUGUI tooltipText;
    private TextMeshProUGUI titleText;
    private RectTransform backgroundRectTransform;

    void Awake()
    {
        instance = this;
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        tooltipText = transform.Find("text").GetComponent<TextMeshProUGUI>();
        titleText = transform.Find("title").GetComponent<TextMeshProUGUI>();

        HideTooltip();
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out localPoint);
        transform.position = Input.mousePosition;
    }

    private void ShowTooltip(string titleString, string tooltipString)
    {
        gameObject.SetActive(true);

        tooltipText.text = tooltipString;
        titleText.text = titleString;

        float textPaddingSize = 6f;
        Vector2 backgroundSize = new Vector2(tooltipText.renderedWidth + textPaddingSize * 2, tooltipText.renderedHeight + titleText.renderedHeight + textPaddingSize * 2.5f);
        backgroundRectTransform.sizeDelta = backgroundSize;
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string titleString, string tooltipString)
    {
        instance.ShowTooltip(titleString, tooltipString);

        print("showing tooltip");
    }

    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }
}
