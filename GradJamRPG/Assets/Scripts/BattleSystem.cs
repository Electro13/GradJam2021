using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

/**
 * Player can currently only use 1 type of attack
 * 
 */
public class BattleSystem : MonoBehaviour
{
    public Text dialogueText;

    Player_Battle playerStats;
    Enemy_Battle enemyStats;


    // Start is called before the first frame update

    public BattleState state;

    void Start()
    {
        state = BattleState.START;

        // Turn base loop, continues until executing is completed (Battle is over)
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        //Get the  stats of the fighters, find them by tag and get their statistics component.
        playerStats = GameObject.FindWithTag("Player").GetComponent<Player_Battle>();
        enemyStats = GameObject.FindWithTag("Enemy").GetComponent<Enemy_Battle>();
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
    IEnumerator PlayerAttack()
    {
        //Check the player's current status effect
        playerStats.CheckStatusEffects();
        Debug.Log(playerStats.canAttack);
        //If the player can attack due to a status update
        if (playerStats.Attack(Player_Battle.ATTACKTYPE.single))
        {
            //Update HUD etc...
            dialogueText.text = "Attack landed";
            yield return new WaitForSeconds(2f);

            if (enemyStats.isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
        else
        {
            // Player's can not attack due to a status update, check the current players status update.
            dialogueText.text = "Player's turn has ended due to " + playerStats.currentEffect + "!";
            yield return new WaitForSeconds(2f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
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

        StartCoroutine(PlayerAttack());
    }
}
