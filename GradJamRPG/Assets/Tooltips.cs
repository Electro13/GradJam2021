using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltips : MonoBehaviour
{
    public static string[] GetStatusTooltip(Entity.STATUSEFFECTS stausEffect, int amount) {

        string[] tip = new string[2];

        switch (stausEffect) {
            case Entity.STATUSEFFECTS.Paralyzed:
                tip[0] = "Paralyzed";
                tip[1] = "You cannot do anything for " + "<color=#F00>" + amount + "</color>" + " turns";
                return tip;
            case Entity.STATUSEFFECTS.Fear:
                tip[0] = "Fear";
                tip[1] = "You take 20% more damage from attacks";
                return tip;
            case Entity.STATUSEFFECTS.RestlessLeg:
                tip[0] = "Restless Leg";
                tip[1] = "At the start of your turn take " + "<color=#FFFFFF>" + amount + "</color>" + " damage";
                return tip;
            case Entity.STATUSEFFECTS.Adrenaline:
                tip[0] = "Adrenaline";
                tip[1] = "Deal " + amount + " extra damage with your attacks";
                return tip;
        }

        return tip;
    }

    public static string[] GetSkillTooltip(PlayerStats.SKILLS skill, int strength)
    {
        string[] tip = new string[2];

        switch(skill)
        {
            case PlayerStats.SKILLS.DoubleStrike:
                tip[0] = "Double Strike";
                tip[1] = "[Attack] Deal " + Mathf.RoundToInt(strength * 1.5f) + " to target";
                return tip;
            case PlayerStats.SKILLS.FireBall:
                tip[0] = "Fireball";
                tip[1] = "[Skill] Deal 6 damage to target";
                return tip;
            case PlayerStats.SKILLS.ShoulderBash:
                tip[0] = "Shoulder Bash";
                tip[1] = "[Skill] Give 2 <color=#F9E816>Paralyzed</color> to target";
                return tip;
            case PlayerStats.SKILLS.Pressure:
                tip[0] = "Pressure";
                tip[1] = "[Skill] Give 2 <color=#F9E816>Restless Leg</color> to target";
                return tip;

        }

        return tip;
    }

    public static string[] GetSkillInfoTooltip(PlayerStats.SKILLS skill)
    {
        string[] tip = new string[2];

        switch (skill)
        {
            case PlayerStats.SKILLS.DoubleStrike:
                tip[0] = "Double Strike";
                tip[1] = "[Attack] Deal 1.5x attack damage to target";
                return tip;
            case PlayerStats.SKILLS.FireBall:
                tip[0] = "Fireball";
                tip[1] = "[Skill] Deal 6 magic damage to target";
                return tip;
            case PlayerStats.SKILLS.ShoulderBash:
                tip[0] = "Shoulder Bash";
                tip[1] = "[Skill] Give 2 <color=#F9E816>Paralyzed</color> to target";
                return tip;
            case PlayerStats.SKILLS.Pressure:
                tip[0] = "Pressure";
                tip[1] = "[Skill] Give 2 <color=#F9E816>Restless Leg</color> to target";
                return tip;

        }

        return tip;
    }
}
