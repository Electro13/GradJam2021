using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop01Access : MonoBehaviour
{
    public GameObject ShopInventory;
    public GameObject Item01Text;
    public GameObject Item02Text;
    public GameObject Item03Text;
    public GameObject Item04Text;
    public GameObject ItemCompletion;
    public GameObject CompleteText;

    void OnTriggerEnter()
    {
        ShopInventory.SetActive(true);
        Screen.lockCursor = false;
        GlobalShop.shopNumber = 1;
        Item01Text.GetComponent<Text>().text = "" + GlobalShop.Item01;
        Item02Text.GetComponent<Text>().text = "" + GlobalShop.Item02;
        Item03Text.GetComponent<Text>().text = "" + GlobalShop.Item03;
        Item04Text.GetComponent<Text>().text = "" + GlobalShop.Item04;
    }

    public void Item01()
    {
        ItemCompletion.SetActive(false);
        CompleteText.GetComponent<Text>().text = "Are you sure you want to buy " + GlobalShop.Item01 + "?";
    }
    
    public void Item02()
    {
        ItemCompletion.SetActive(false);
        CompleteText.GetComponent<Text>().text = "Are you sure you want to buy " + GlobalShop.Item02 + "?";
    } 
    
    public void Item03()
    {
        ItemCompletion.SetActive(false);
        CompleteText.GetComponent<Text>().text = "Are you sure you want to buy " + GlobalShop.Item03 + "?";
    }
    
    public void Item04()
    {
        ItemCompletion.SetActive(false);
        CompleteText.GetComponent<Text>().text = "Are you sure you want to buy " + GlobalShop.Item04 + "?";
    }

    public void CancelTransaction()
    {
        ItemCompletion.SetActive(false);
    }

}
