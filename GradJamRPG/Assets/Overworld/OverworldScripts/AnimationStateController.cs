using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{

    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        

      

       

    }

    // Update is called once per frame
    void Update()
    {
        bool interactKey = (Input.GetKeyDown("e"));

        if (interactKey)
        {
            anim.SetTrigger("attack");
        }

        anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs((Input.GetAxis("Horizontal"))) );




    }
}
