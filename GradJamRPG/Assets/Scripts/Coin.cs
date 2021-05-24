using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int amount;

    void Use (PlayerStats player)
    {
        player.gold += amount;
    }
}
