using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{

    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("attack", true);
        }

        anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs((Input.GetAxis("Horizontal"))) );
    }
}
