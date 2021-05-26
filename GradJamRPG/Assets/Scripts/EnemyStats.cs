using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyStats : Entity
{
    public TextMeshProUGUI healthText;
    public Vector3 position;

    public int xp;

    public enum ATTACKPATTERN
    {
        Attack,
        Debuff,
        Buff
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        currentHealth = maxHealth;

        healthText.SetText(currentHealth.ToString());
    }


    public void Attack(int dmg, Entity target)
    {
       target.TakeDamage(dmg);
    }

    public override void TakeDamage(int damage)
    {    
        currentHealth -= damage;       
        if (currentHealth <= 0)
        {
            animator.SetTrigger("death");
            Die();
        }

        healthText.SetText(currentHealth.ToString());
    }
}