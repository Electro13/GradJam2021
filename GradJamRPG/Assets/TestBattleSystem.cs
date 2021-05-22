using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/**
 * Player can currently only use 1 type of attack
 * 
 */
public class TestBattleSystem : MonoBehaviour
{
    public Text dialogueText;

    PlayerStats playerStats;
    EnemyStats enemyStats;

    public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
    // Start is called before the first frame update

    public BattleState state;

    public Transform playerPosition;
    public Transform[] enemyPositions;


    public GameObject[] enemyPrefabs;
    public List<EnemyStats> enemies;


    public List<GameObject> turnOrder;


    void Start()
    {
        state = BattleState.START;

        
        //Add enemies to scene and store their values
        //for(int i = 0; i < enemyPrefabs.Length; i++)
        //{
        //   EnemyStats enemy = Instantiate(enemyPrefabs[i], enemyPositions[i]).GetComponent<EnemyStats>();

        //   enemies.Add(enemy);
        //}

        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();

        playerStats.transform.position = playerPosition.position;

        // Turn base loop, continues until executing is completed (Battle is over)
        StartCoroutine(SetupBattle());


    }

    IEnumerator SetupBattle()
    {
        //Get the  stats of the fighters, find them by tag and get their statistics component.

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            EnemyStats enemy = Instantiate(enemyPrefabs[i], enemyPositions[i].position, Quaternion.Euler(0, -90, 0)).GetComponent<EnemyStats>();

            enemies.Add(enemy);
        }

        //Update the battle message.
        dialogueText.text = "A new foe has appeared!";


        //Wait 2 seconds, and proceed to next sequence (Enumeration). Also updating current state.
        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    void PlayerTurn()
    {

        dialogueText.text = "Choose an action:";

    }
    IEnumerator EnemyTurn()
    {

        dialogueText.text = "Opponent's Turn!";
        yield return new WaitForSeconds(1f);
        dialogueText.text = "Opponent used Basic Attack #1";
        yield return new WaitForSeconds(2f);
        // Enemies currently do not have status effects to hinder their attacks.

        enemyStats.Attack(10);
        if (playerStats.isDead)
        {
            state = BattleState.LOST;
            //End the battle the player has lost
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }
    IEnumerator PlayerAttack(PlayerStats.ATTACKTYPE attackType)
    {
        //Check the player's current status effect
        playerStats.CheckStatusEffects();
        Debug.Log(playerStats.canAttack);
        //If the player can attack due to a status update

        EnemyStats target = null;

        //Get target enemy unless there is only 1 remaining enemy
        if (enemies.Count > 1)
        {
            dialogueText.text = "Select A Target...";

            while (target == null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.CompareTag("Enemy"))
                        {
                            target = hit.collider.GetComponent<EnemyStats>();
                        }
                        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 2f);
                    }
                    else
                    {
                        Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue, 2f);
                    }
                }
                yield return null;
            }
        }
        else
        {
            target = enemies[0];
        }

        dialogueText.text = "";

        //Attack the targeted enemy
        Vector3 targetLocation = target.transform.position + Vector3.left * 3f + Vector3.up * 0.96f;
        Vector3 dir = (playerStats.transform.position - targetLocation).normalized;

        while ((playerStats.transform.position - targetLocation).magnitude > 1f)
        {
            playerStats.transform.position = targetLocation;
            yield return null;
        }

        playerStats.Attack(attackType, target);
        yield return new WaitForSeconds(1f);

        playerStats.transform.position = playerPosition.position;


        //Check All Enemies HP
        foreach (EnemyStats enemy in enemies)
        {
            if (enemy.isDead)
            {
                enemies.Remove(enemy);
                Destroy(enemy.gameObject);
            }
        }

        //Update HUD etc...
        dialogueText.text = "Attack landed";
        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerSkill(PlayerStats.SKILLS skillType)
    {
        //Check the player's current status effect
        playerStats.CheckStatusEffects();
        Debug.Log(playerStats.canAttack);
        //If the player can attack due to a status update

        EnemyStats target = null;

        //Get target enemy unless there is only 1 remaining enemy
        if (enemies.Count > 1)
        {
            dialogueText.text = "Select A Target...";

            while (target == null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {

                        if (hit.collider.gameObject.CompareTag("Enemy"))
                        {
                            target = hit.collider.GetComponent<EnemyStats>();
                        }
                        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 2f);
                    }
                    else
                    {
                        Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue, 2f);
                    }
                }
                yield return null;
            }
        }
        else
        {
            target = enemies[0];
        }

        print("Using skill " + skillType + " on " + target.transform);

        dialogueText.text = "";

        //Attack the targeted enemy
        Vector3 targetLocation = target.transform.position + Vector3.left * 3f + Vector3.up * 0.96f;
        Vector3 dir = (playerStats.transform.position - targetLocation).normalized;

        while ((playerStats.transform.position - targetLocation).magnitude > 1f)
        {
            playerStats.transform.position = targetLocation;
            yield return null;
        }

        StartCoroutine(playerStats.Skill(skillType, target));
        yield return new WaitForSeconds(1.5f);

        playerStats.transform.position = playerPosition.position;


        //Check All Enemies HP
        foreach (EnemyStats enemy in enemies)
        {
            if (enemy.isDead)
            {
                enemies.Remove(enemy);
                Destroy(enemy.gameObject);
            }
        }

        //Update HUD etc...
        dialogueText.text = "Attack landed";
        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void EndBattle()
    {

        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";

        }
    }
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack(PlayerStats.ATTACKTYPE.single));
    }

    public void OnSkillButton(int skill)
    {
        if (state != BattleState.PLAYERTURN)
            return;

        //Essentially uses the skill that the button is registered too
        StartCoroutine(PlayerSkill((PlayerStats.SKILLS)playerStats.usableSkills[skill]));
    }




}
