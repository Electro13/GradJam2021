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
    // Update is called once per frame
    void Update()
    {

    }
}