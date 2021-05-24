using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destroyedPot;

<<<<<<< Updated upstream

    // Start is called before the first frame update
    void Start()
    {

    }


    public void Break()
=======
    void OnMouseDown()
>>>>>>> Stashed changes
    {
        Instantiate(destroyedPot, transform.position, transform.rotation);
        Destroy(gameObject);
    }
<<<<<<< Updated upstream


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("sword")){
            Break();
        }
    }
=======
>>>>>>> Stashed changes
}