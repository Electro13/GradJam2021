using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSkillInfo : MonoBehaviour
{
    public PlayerStats.SKILLS skillType;
    PlayerStats player;

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>();
    }

    public void SetType(PlayerStats.SKILLS type)
    {
        skillType = type;
    }

    public void ShowTooltip()
    {
        string[] tip = Tooltips.GetSkillTooltip(skillType, player.potentialStrength);
        Tooltip.ShowTooltip_Static(tip[0], tip[1]);
    }

    public void HideTooltip()
    {
        Tooltip.HideTooltip_Static();
    }
}
