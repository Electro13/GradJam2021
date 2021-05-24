using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{

    public GameObject destroyedPot;


    // Start is called before the first frame update
    void Start()
    {

    }


    public void Break()
    {
        Instantiate(destroyedPot, transform.position, transform.rotation);
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("sword")){
            Break();
        }
    }
}