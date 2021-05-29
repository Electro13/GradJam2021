using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam; 

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public OverWorldGameManager gm;

    public bool isGrounded;

    private void FixedUpdate()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 1.2f))
        {
            Debug.DrawRay(transform.position, Vector3.down * 1.2f, Color.blue);
        }
        else
        {
            transform.Translate(Vector3.down * 9.81f * Time.deltaTime);
            Debug.DrawRay(transform.position, Vector3.down * 1.2f, Color.yellow);
        }
    }

    void Update()
    {
      
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y ;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle , 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;


            controller.Move(moveDir.normalized * speed * Time.deltaTime);


        }          

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            StartCoroutine(gm.StartBattle(other.GetComponent<OverworldAIController>(), false, 0));
        }        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Item"))
        {
            if (Input.GetKeyDown("e"))
            {
                print("picking up scroll");
                other.GetComponent<Pickup>().Use(gm.battleSystem.playerStats);
            }
        }
    }
}
