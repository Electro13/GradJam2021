using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OverWorldGameManager : MonoBehaviour
{
    public TestBattleSystem battleSystem;

    public Camera overworldCamera;
    public Camera battleCamera;

    public GameObject playerObject;
    Vector3 lastPos;

    public GameObject battleHud;
    OverworldAIController lastEnemy;

    public Animator transitionAnim;
    private void Start()
    {
        Physics.IgnoreLayerCollision(6, 6, true);
    }

    public IEnumerator StartBattle(OverworldAIController enemy, bool firstStrike, int damage) 
    {
        lastEnemy = enemy;

        lastEnemy.GetComponent<NavMeshAgent>().enabled = false;
        enemy.GetComponent<BoxCollider>().enabled = false;
        enemy.isAlive = false;

        playerObject.GetComponent<ThirdPersonMovement>().enabled = false;
        yield return new WaitForSeconds(.8f);

        //Turn on transition animation
        transitionAnim.SetTrigger("open");

        yield return new WaitForSeconds(.3f);

        //Change camera to battle camera
        overworldCamera.enabled = false;
        battleCamera.enabled = true;

        //Give battle system enemies to spawn
        battleSystem.enemyPrefabs = enemy.encounteredEnemies;
        playerObject.SetActive(false);

        transitionAnim.SetTrigger("close"); 

        //Start battle system
        if (firstStrike)
        {
            StartCoroutine(battleSystem.SetupBattle(damage));
        }
        else
        {
            StartCoroutine(battleSystem.SetupBattle());
        }

        //Turn off colliders on the player so we cannot join another battle
        lastPos = playerObject.transform.position;
        playerObject.GetComponent<CapsuleCollider>().enabled = false;

        battleHud.SetActive(true);
    
    }

    public IEnumerator EndBattle()
    {
        //Play scene transtition
        transitionAnim.SetTrigger("open");

        yield return new WaitForSeconds(.3f);

        battleCamera.enabled = false;
        overworldCamera.enabled = true;

        playerObject.GetComponent<ThirdPersonMovement>().enabled = true;
        playerObject.GetComponent<CapsuleCollider>().enabled = true;
        playerObject.transform.position = lastPos;

        playerObject.SetActive(true);

        battleHud.SetActive(false);

        transitionAnim.SetTrigger("close");


        lastEnemy.Die();
        lastEnemy.animator.SetTrigger("death");
    }





}
