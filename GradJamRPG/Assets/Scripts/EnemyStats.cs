using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyStats : Entity
{
    public TextMeshProUGUI healthText;
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
            Die();
        }

        healthText.SetText(currentHealth.ToString());
    }

    public void CheckStatusEffects()
    {
        potentialStrength = strength;

        foreach (Effect effect in currentEffects)
        {
            switch (effect.status)
            {
                case STATUSEFFECTS.None:
                    break;

                case STATUSEFFECTS.Narcolepsy:
                    // debug youre asleep!
                    canAttack = false;
                    break;

                case STATUSEFFECTS.RestlessLeg:
                    canAttack = false;
                    break;

                case STATUSEFFECTS.Insomnia:
                    break;
                case STATUSEFFECTS.Paralyzed:
                    canAttack = false;
                    break;
            }
        }
    }
}