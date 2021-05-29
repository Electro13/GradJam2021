using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitShop : MonoBehaviour
{
    public ThirdPersonMovement Player;
    public GameObject ShopPanel;
    public void ExitShopMode()
    {
        Player.GetComponent<ThirdPersonMovement>().enabled = true;
        ShopPanel.SetActive(false);
    }
}
