using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapSkillManager : MonoBehaviour
{
    public GameObject skillMenu;
    public ButtonSkillInfo[] skills;
    public ButtonSkillInfo newSkillButtonInfo;
    public PlayerStats player;

    public Scroll newSkill;

    public void GetNewSkill(Scroll newSkill_)
    {
        newSkill = newSkill_;

        newSkillButtonInfo.SetType((PlayerStats.SKILLS)newSkill.skillNum);

        for (int i = 0; i < skills.Length; i++)
        {
            skills[i].SetType((PlayerStats.SKILLS)player.usableSkills[i]);
        }

        skillMenu.SetActive(true);
    }

    public void ChangeSkill(int skillPosition)
    {
        player.usableSkills[skillPosition] = newSkill.skillNum;

        Destroy(newSkill.itemObject);

        Tooltip.HideTooltip_Static();

        skillMenu.SetActive(false);
    }

    public void Close()
    {
        Tooltip.HideTooltip_Static();
        skillMenu.SetActive(false);
    }


}
