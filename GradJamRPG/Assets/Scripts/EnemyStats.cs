using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Entity
{
    PlayerStats player;
    
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
        player = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }


    public void Attack(int dmg, Entity target)
    {
       target.TakeDamage(dmg);
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