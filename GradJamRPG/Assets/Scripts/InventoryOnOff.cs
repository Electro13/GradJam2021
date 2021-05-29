using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOnOff : MonoBehaviour
{
    public GameObject OurInventory;
    public int InvStatus;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if(InvStatus == 0)
            {
                InvStatus = 1;
                OurInventory.SetActive(true);
            }
            else
            {
                InvStatus = 0;
                OurInventory.SetActive(false);
            }
        }
    }
}
