using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // variables
    public int maxHealth;
    public int currentHealth;
    public bool isDead;
    // used for taking damage (damage multiplier)
    public int heartRate;
    public int strength = 1;

    public bool canAttack = true;

    // ending turn
    public bool endTurn = false;

    // unlocked moves
    public bool canDoubleStrike = false;
    public bool canFireball = false;
    public bool canLightningBolt = false;
    public bool canSpinMove = false;

    // holding skills and attack type
    public SKILLS skills;
    public ATTACKTYPE attackType;
    public STATUSEFFECTS currentEffect;

    EnemyStats enemy;
   // Weapon currentWeapon;

    void Start()
    {
        heartRate = 1;
        currentHealth = maxHealth;
        currentEffect = STATUSEFFECTS.None;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            // Gameover UI
        }

        CheckStatusEffects();
        // attack window 
    }

    public void CheckStatusEffects()
    {
        switch (currentEffect)
        {
            case STATUSEFFECTS.None:
                break;

            case STATUSEFFECTS.Narcolepsy:
                // debug youre asleep!
                canAttack = false;
                endTurn = true;
                break;

            case STATUSEFFECTS.RestlessLeg:
                canAttack = false;
                endTurn = true;
                break;

            case STATUSEFFECTS.NightmareDisorder:
                heartRate = 10;
                break;

            case STATUSEFFECTS.Insomnia:

                break;
        }
    }

    public bool  Attack (ATTACKTYPE attackType)
    {
        if (canAttack)
        {
            // animation 
            switch (skills)
            {
                case SKILLS.DoubleStrike:
                    // call double strike animation
                    break;
            }

            switch (attackType)
            {
                case ATTACKTYPE.single:
                    enemy = GameObject.FindWithTag("Enemy").GetComponent<EnemyStats>();
                    /*currentWeapon = currentWeapon.GetComponent<Weapon>();
                    enemy.TakeDamage(currentWeapon.damage + strength + heartRate);*/
                    break;

                case ATTACKTYPE.aoe:

                    break;
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    // change status effect (recieved from enemy)
    public void ReceiveStatusEffect(string s)
    {
        if (s == "Narcolepsy")
        {
            currentEffect = STATUSEFFECTS.Narcolepsy;
        }
        else if (s == "RestlessLeg")
        {
            currentEffect = STATUSEFFECTS.RestlessLeg;
        }
        else if (s == "NightmareDisorder")
        {
            currentEffect = STATUSEFFECTS.NightmareDisorder;
        }
        else if (s == "Insomnia")
        {
            currentEffect = STATUSEFFECTS.Insomnia;
        }
        else
        {
            currentEffect = STATUSEFFECTS.None;
        }

    }

   public void TakeDamage(int dmg)
    {
        currentHealth -= (dmg + heartRate);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
    }

    public int GetCurrentHP()
    {
        return currentHealth;
    }

    public int GetMaxtHP()
    {
        return maxHealth;
    }

    public enum STATUSEFFECTS
    {
        None,
        Narcolepsy,
        RestlessLeg,
        NightmareDisorder,
        Insomnia
    }

    public enum SKILLS
    {
        DoubleStrike,
        SliceandDice,
        FireBall,
        ShoulderBash,
        SpinMove,
        LightningBolt,
        Counter,
        Lullaby
    }

    public enum ATTACKTYPE
    {
        single,
        aoe
    }
}