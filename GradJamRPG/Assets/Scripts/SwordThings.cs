using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordThings : MonoBehaviour
{

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Pot"))
        {
            print("i hit a pot");
            other.GetComponent<Destructable>().Break();
        }

        if (other.tag.Equals("Enemy"))
        {
            StartCoroutine(FindObjectOfType<OverWorldGameManager>().StartBattle(other.GetComponent<OverworldAIController>(), true, 5));
        }
    }
}
