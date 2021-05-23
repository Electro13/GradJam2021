using System.Collections;
using UnityEngine;

public class PlayerStats : Entity
{
    public bool endTurn;

    // unlocked moves
    public bool canDoubleStrike = false;
    public bool canFireball = false;
    public bool canLightningBolt = false;
    public bool canSpinMove = false;

    public int heartRate;

    // holding skills
    public SKILLS skills;

    //Ex if 1st skill is Spin move
    //usableSkills[0] = 5 or SKILLS.SpinMove
    public int[] usableSkills;

    //public Weapon currentWeapon;


    protected override void Start()
    {
        base.Start();

        heartRate = 80;
        currentHealth = maxHealth; //Will equal player prefs soon
    }

    public void CheckStatusEffects()
    {
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

                case STATUSEFFECTS.NightmareDisorder:
                    heartRate = 10;
                    break;

                case STATUSEFFECTS.Insomnia:
                    break;
                case STATUSEFFECTS.Paralyzed:
                    canAttack = false;
                    break;
            }
        }
    }

    public bool Attack(ATTACKTYPE attackType, EnemyStats target)
    {
        if (canAttack)
        {
            switch (attackType)
            {
                case ATTACKTYPE.single:
                    //enemy = GameObject.FindWithTag("Enemy").GetComponent<EnemyStats>();
                    target.TakeDamage(strength);

                    animator.SetTrigger("attack");
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

    public IEnumerator Skill(SKILLS skillType, Entity target)
    {
        if (canAttack)
        {
            // animation 
            switch (skillType)
            {
                case SKILLS.DoubleStrike:
                    // call double strike animation
                    animator.SetTrigger("doubleStrike");
                    //Timed with the animation - 
                    yield return new WaitForSeconds(0.5f);
                    target.TakeDamage(strength);
                    yield return new WaitForSeconds(0.3f);
                    target.TakeDamage(strength);
                    break;

                case SKILLS.ShoulderBash:
                    animator.SetTrigger("shoulderBash");
                    //Apply stun effect for 3 turns maybe do damage??
                    target.GiveStatusEffect(STATUSEFFECTS.Paralyzed, 3);
                    break;
                case SKILLS.FireBall:
                    break;
                case SKILLS.LightningBolt:
                    break;
            }
        }

        yield return null;
    }

   

    //Increase our Heart Rate when taking damage
    public override void TakeDamage(int dmg)
    {
        currentHealth += dmg;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
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
}