using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectPanel : MonoBehaviour
{
    public TextMeshProUGUI amountText;
    public Image image;

    public Entity.Effect effect;
    
    //public animator;

    public void Set(int amount)
    {
        if (amount == 0)
        {
            amountText.SetText("");
            image.color = new Color(0,0,0,0);
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

    private void OnMouseEnter()
    {
        if (effect.status != Entity.STATUSEFFECTS.None)
        {
            string[] toolTipData = Tooltips.GetStatusTooltip(effect.status, effect.amount);
            Tooltip.ShowTooltip_Static(toolTipData[0], toolTipData[1]);
        }
    }

    private void OnMouseExit()
    {
        Tooltip.HideTooltip_Static();
    }
}
