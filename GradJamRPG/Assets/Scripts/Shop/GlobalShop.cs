using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalShop : MonoBehaviour
{
    public static string Item01;
    public static string Item02;
    public static string Item03;
    public static string Item04;
    public static int shopNumber;
    // Update is called once per frame
    void Update()
    {
        if (shopNumber == 1)
        {
            Item01 = "Sword";
            Item02 = "Armor";
            Item03 = "Health";
            Item04 = "Energy";
        }

        if (shopNumber == 2)
        {
            Item01 = "New Skill";
            Item02 = "Health";
            Item03 = "Sword";
            Item04 = "";
        }

    }
}
