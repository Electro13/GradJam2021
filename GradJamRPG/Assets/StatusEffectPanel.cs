using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectPanel : MonoBehaviour
{
    public TextMeshProUGUI amountText;
    public Image image;
    
    //public animator;

    public void Set(int amount)
    {
        if (amount == 0)
        {
            amountText.SetText("");
        }
        else
        {
            amountText.SetText(amount.ToString());
        }

        //Play change animation
    }

    public void SetImage(Image image_)
    {
        image.sprite = image_.sprite;
        image.color = image_.color;
    }
}
