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
        }

        return null;
    }
}
