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


    void OnMouseDown()
    {
        Instantiate(destroyedPot, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
            Instantiate(destroyedPot, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {

     
    }
}