using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    public GameObject[] items;
    public Vector3 force;

    public void DropCurrentItem(int id)
    {
        if (items[id])
        {
            GameObject drop = Instantiate(items[id], this.transform.position, this.transform.rotation);
            //drop = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("Error!");
            return;
        }
    }
}
