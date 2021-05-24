using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OverworldAIController : MonoBehaviour
{

    NavMeshAgent agent;
    public Animator animator;

    ///Radius of the attack range
    public float attackRange;

    Vector3 startingPosition;

    //Radius of the walk range
    public float walkRange;

    public Vector3 targetLocation;
    public Transform targetPlayer;

    public LayerMask aggroLayerMask;
    Collider[] withinAggroColliders;
    bool aggroSearch = true;

    public GameObject[] encounteredEnemies;

    public bool isAlive;

    public enum States
    {
        IDLE,
        WALKING,
        CHASING,
        SEARCHING
    }

    public States state;

    // Start is called before the first frame update
    void Start()
    {        
          agent = GetComponent<NavMeshAgent>();
      animator.GetComponent<Animator>();
        startingPosition = transform.position;

        isAlive = true;

        StartCoroutine(StateHandling());
    }


    //Every so often checks if a player is in view
    //If there is start chasing the player
    private void FixedUpdate()
    {
        if (aggroSearch && isAlive)
        {
            withinAggroColliders = Physics.OverlapSphere(transform.position, attackRange, aggroLayerMask);
            if (withinAggroColliders.Length > 0)
            {
                animator.SetTrigger("Hop");
                targetPlayer = withinAggroColliders[0].transform;
                state = States.CHASING;

                //We need this incase the enemy just went into the idle state and is waiting 2 seconds to decide what to do
                StopAllCoroutines();
                StartCoroutine(StateHandling());

                aggroSearch = false;
            }
        }
    }


    //Core loop that handles all AI movements
    IEnumerator StateHandling()
    {
        while(isAlive) 
        {
            switch (state)
            {
                case States.IDLE:

                    //set IDLE animation

                    yield return new WaitForSeconds(2f);

                    //OVER 9000!
                    {
                        /*int random = Random.Range(0, 10000);

                        if(random > 9000)
                        {
                            if (FindNewLocation())
                            {
                                agent.CalculatePath(targetLocation, path);
                                agent.SetPath(path);
                                state = States.WALKING;
                            }
                        }
                        */
                    }

                    //If we can find a location walk to it
                    if (FindNewLocation())
                    {
                        agent.destination = targetLocation;
                        state = States.WALKING;
                        //Set WALKING aniamtion
                        animator.SetBool("isWalking", true);
                    }
                    break;

                case States.WALKING:
                    //If we reach destination switch to idle
                    if (transform.position.x == targetLocation.x && transform.position.z == targetLocation.z)
                    {
                        state = States.IDLE;
                        animator.SetBool("isWalking", false);
                    }
                    break;

                //When player is in sight pathfind to the player
                case States.CHASING:
                    animator.SetBool("isWalking", true);
                    //Pathfind to target
                    agent.destination = targetPlayer.position;

                    //If target leaves attack range switch stop chasing
                    if ((targetPlayer.position - transform.position).magnitude > attackRange + 1)
                        state = States.SEARCHING;                    
                    break;

                //If player leaves the attackrange, enemy should look around for a second or two before returning to its original position
                case States.SEARCHING:

                    animator.SetBool("isWalking", false);
                    yield return new WaitForFixedUpdate();

                    //Enable aggro
                    aggroSearch = true;

                    //Stop moving
                    agent.destination = transform.position;


                    //Return to idle for now
                    state = States.IDLE;
                    
                    break;
            }

            yield return null;
        }
    }

    bool FindNewLocation()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRange;

        randomDirection += startingPosition;
        NavMeshHit hit;

        if(!NavMesh.SamplePosition(randomDirection, out hit, walkRange, 1)){
            Debug.LogWarning(gameObject + "Can not find point on navMesh");
            return false;
        }

        targetLocation = hit.position;

        if((targetLocation - transform.position).magnitude < 2)
            return FindNewLocation();        

        return true;
    }
    public void Die()
    {
        animator.SetTrigger("death");

        Destroy(gameObject, 2f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startingPosition, walkRange);
    }
}
