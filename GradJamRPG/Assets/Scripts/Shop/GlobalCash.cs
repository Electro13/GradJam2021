using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalCash : MonoBehaviour
{
    public static int CurrentCoins = 100;
    public int LocalCoins;
    public GameObject InvDisplay;

    // Update is called once per frame
    void Update()
    {
        LocalCoins = CurrentCoins;
        InvDisplay.GetComponent<Text>().text = "Coins: " + LocalCoins;
    }
}
