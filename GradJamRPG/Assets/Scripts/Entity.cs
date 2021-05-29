using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public StatusEffectPanel[] effectUI;

    public Image[] statusEffectIcons;

    public Animator animator;

    public GameObject ParalyzedEffect;
    public GameObject model;

    protected virtual void Start()
    {
        currentEffects = new List<Effect>();
        isDead = false;
    }

    public enum ATTACKTYPE
    {
        single,
        aoe
    }

    public enum STATUSEFFECTS
    {
        Paralyzed,
        Fear,
        RestlessLeg,
        Adrenaline,
        None
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
                effectUI[i].Set(currentEffects[i].amount);
                effectUI[i].effect = currentEffects[i];
                return;
            }
        }

        currentEffects.Add(newEffect);

        int index = currentEffects.IndexOf(newEffect);
        effectUI[index].SetImage(statusEffectIcons[Convert.ToInt32(statusEffect)]);
        effectUI[index].Set(amount);
        effectUI[index].effect = newEffect;
    }

    public void ReduceAllEffects()
    {
        for (int i = 0; i < currentEffects.Count; i++)
        {
            if (currentEffects[i].status == STATUSEFFECTS.Adrenaline)
                break;

            //looks weird but this reduces the duration of the effect by 1
            currentEffects[i] -= 1;
            effectUI[i].Set(currentEffects[i].amount);
            effectUI[i].effect = currentEffects[i];

            //If the duration is 0 or less remove the debuff
            if(currentEffects[i].amount <= 0)
            {
                currentEffects.Remove(currentEffects[i]);
                effectUI[i].effect.status = STATUSEFFECTS.None;

                ReOrderEffects();
            }
        }        
    }

    public void CheckStatusEffects()
    {
        canAttack = true;
        int adrenaline = 0;
        potentialStrength = strength;

        foreach (Effect effect in currentEffects)
        {
            switch (effect.status)
            {
                case STATUSEFFECTS.None:
                    break;
                case STATUSEFFECTS.Paralyzed:
                    canAttack = false;
                    Instantiate(ParalyzedEffect, model.transform.position + Vector3.down * 0.5f, Quaternion.identity);
                    break;
                case STATUSEFFECTS.Fear:
                    potentialStrength = Mathf.RoundToInt(potentialStrength * 0.8f);
                    break;
                case STATUSEFFECTS.RestlessLeg:
                    TakeDamage(effect.amount);
                    break;
                case STATUSEFFECTS.Adrenaline:
                    adrenaline = effect.amount;
                    break;       
            }
        }

        potentialStrength += adrenaline;
    }

    void ReOrderEffects()
    {
        //Reset panels to nothing
        foreach(StatusEffectPanel panel in effectUI)
        {
            panel.Set(0);
        }

        //Re add effects to panel
        for(int i = 0; i < currentEffects.Count; i++)
        {
            effectUI[i].SetImage(statusEffectIcons[Convert.ToInt32(currentEffects[i].status)]);
            effectUI[i].Set(currentEffects[i].amount);
            effectUI[i].effect = currentEffects[i];
        }
    }

    public void CheckPostStatusEffects()
    {
        foreach (Effect effect in currentEffects)
        {
            switch (effect.status)
            {
                case STATUSEFFECTS.Adrenaline:
                    potentialStrength += effect.amount;
                    break;
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

    protected void Die()
    {
        isDead = true;
    }   
}
