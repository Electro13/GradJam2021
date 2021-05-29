using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : Pickup
{
    public int skillNum;

    public override void Use (PlayerStats player)
    {
        //If a skill is not assigned in that slot it becomes this skill
        for(int i = 0; i < player.usableSkills.Length; i++)
        {
            if(player.usableSkills[i] <= 0)
            {
                player.usableSkills[i] = skillNum;
                return;
            }
        }

        //Else the player's skill are full and we ask to swap
        FindObjectOfType<OverWorldGameManager>().SwapSkills(this);
    }

}

