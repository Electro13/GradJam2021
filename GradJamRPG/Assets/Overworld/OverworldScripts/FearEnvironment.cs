using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearEnvironment : MonoBehaviour
{
    public float radius;
    public int fearLvl;

    Player_Battle player;

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Player found");
            player = c.gameObject.GetComponent<Player_Battle>();
            StartCoroutine(IncrementHeartRate());
        }
    }

    IEnumerator IncrementHeartRate()
    {
        Debug.Log("executing coroutine");
        yield return new WaitForSeconds(5f);
        player.IncreaseHeartRate(fearLvl);
        Debug.Log(player.heartRate);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}