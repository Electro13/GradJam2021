using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public bool isDead;
    //Base damage stat
    public int strength = 1;
    //Max damage you can do including weapon and effect multipliers
    public int potentialStrength; 

    public bool canAttack = true;

    public ATTACKTYPE attackType;

    public List<Effect> currentEffects;


    public Animator animator;

    protected virtual void Start()
    {
        currentEffects = new List<Effect>();
        isDead = true;
    }

    public enum ATTACKTYPE
    {
        single,
        aoe
    }

    public enum STATUSEFFECTS
    {
        None,
        Narcolepsy,
        RestlessLeg,
        NightmareDisorder,
        Insomnia,
        Paralyzed,
        Burning
    }

    public struct Effect
    {
        //Amount of turns this debuff will last
        public int amount;
        public STATUSEFFECTS status;

        public Effect(STATUSEFFECTS status_, int amount_)
        {
            amount = amount_;
            status = status_;
        }

        public static Effect operator +(Effect a, Effect b)
        {
            return new Effect(a.status, a.amount + b.amount);
        }

        public static Effect operator -(Effect a, int b)
        {
            return new Effect(a.status, a.amount - b);
        }

    }  

   public virtual void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // change status effect (recieved from enemy)
    public void GiveStatusEffect(STATUSEFFECTS statusEffect, int amount)
    {
        Effect newEffect = new Effect();
        newEffect.amount = amount;
        newEffect.status = statusEffect;

        for(int i = 0; i < currentEffects.Count; i++)
        {
            if (currentEffects[i].status.Equals(newEffect.status))
            {
                currentEffects[i] += newEffect;
                return;
            }
        }

        currentEffects.Add(newEffect);
    }

    public void ReduceAllEffects()
    {
        for (int i = 0; i < currentEffects.Count; i++)
        {
            //looks weird but this reduces the duration of the effect by 1
            currentEffects[i] -= 1;

            //If the duration is 0 or less remove the debuff
            if(currentEffects[i].amount <= 0)
            {
                currentEffects.Remove(currentEffects[i]);
            }
        }
    }

    public int GetCurrentHP()
    {
        return currentHealth;
    }

    public int GetMaxtHP()
    {
        return maxHealth;
    }

    private void Die()
    {
        isDead = true;
    }   
}
