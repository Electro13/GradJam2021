using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHP;
    public int currentHP;
    public bool isDead = false;
    PlayerStats player;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    public void Attack(int dmg)
    {
        player.TakeDamage(dmg);
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
    }

    public int GetMaxHP() { return maxHP; }
    public int GetCurrentHP() { return currentHP; }

}