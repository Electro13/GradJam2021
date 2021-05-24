using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public int skillPosition;
    public int skillNum;

    void Use (PlayerStats player)
    {
        player.usableSkills[skillPosition] = skillNum;
    }

}

