using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapSkillManager : MonoBehaviour
{
    public GameObject skillMenu;
    public ButtonSkillInfo[] skills;
    public ButtonSkillInfo newSkillButtonInfo;
    PlayerStats player;

    public Scroll newSkill;

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        GetNewSkill(null);
    }

    public void GetNewSkill(Scroll newSkill_)
    {
        newSkill = newSkill_;

        newSkillButtonInfo.SetType((PlayerStats.SKILLS)5);

        for (int i = 0; i < skills.Length; i++)
        {
            skills[i].SetType((PlayerStats.SKILLS)player.usableSkills[i]);
        }
    }

    public void ChangeSkill(int skillPosition)
    {
        player.usableSkills[skillPosition] = newSkill.skillNum;
        skillMenu.SetActive(false);
    }

    public void Close()
    {
        skillMenu.SetActive(false);
    }


}
