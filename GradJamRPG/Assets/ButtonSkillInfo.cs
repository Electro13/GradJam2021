using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSkillInfo : MonoBehaviour
{
    //Position of this skill in the players skill inventory
    public int skillPosition;
    public PlayerStats.SKILLS skillType;
    public Text text;
    PlayerStats player;

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>();
    }

    public void SetType(PlayerStats.SKILLS type)
    {
        skillType = type;
        text.text = type.ToString();
    }

    public void ShowTooltip()
    {
        string[] tip = Tooltips.GetSkillInfoTooltip((PlayerStats.SKILLS)player.usableSkills[skillPosition]);
        Tooltip.ShowTooltip_Static(tip[0], tip[1]);
    }

    public void HideTooltip()
    {
        Tooltip.HideTooltip_Static();
    }
}
