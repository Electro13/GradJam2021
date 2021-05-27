using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
using UnityEngine.UI;

public class GlobalCash : MonoBehaviour
{
    public static int CurrentCoins = 100;
    public int LocalCoins;
    public GameObject InventoryDisplay;
=======

public class GlobalCash : MonoBehaviour
{
    public static int CurrentCoins;
    public int LocalCoins;
>>>>>>> Stashed changes

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        LocalCoins = CurrentCoins;
        InventoryDisplay.GetComponent<Text>().text = "Coins: " + LocalCoins;
=======
        
>>>>>>> Stashed changes
    }
}
