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

    public GameObject Fireball;
    public Transform hand;

    public int gold;

    public int maxEnergy;
    public int energy;

    protected override void Start()
    {
        base.Start();

        heartRate = 80;
        currentHealth = maxHealth; //Will equal player prefs soon
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
                    //FindObjectOfType<AudioManager>().Play("Bash");
                    //Apply stun effect for 3 turns maybe do damage??
                    target.GiveStatusEffect(STATUSEFFECTS.Paralyzed, 1);
                    target.GiveStatusEffect(STATUSEFFECTS.Fear, 2);
                    target.GiveStatusEffect(STATUSEFFECTS.RestlessLeg, 3);
                    target.GiveStatusEffect(STATUSEFFECTS.Adrenaline, 10);
                    break;

                case SKILLS.FireBall:
                    animator.SetTrigger("cast");
                    //FindObjectOfType<AudioManager>().Play("Fireball");
                    GameObject newFireball = Instantiate(Fireball, hand);
                    yield return new WaitForSeconds(0.4f);

                    newFireball.transform.parent = null;

                    float mag = (newFireball.transform.position - target.transform.position).magnitude;
                    int things = 0;

                    while (things < 10)
                    {
                        float curMag = (newFireball.transform.position - target.transform.position).magnitude;
                        curMag = Mathf.Abs(curMag - mag - 0.1f);

                        newFireball.transform.position = Vector3.Lerp(newFireball.transform.position, target.transform.position + Vector3.up * 1f + Vector3.left * 1f, curMag/mag);
                        things++;
                        yield return new  WaitForSeconds(0.01f);
                    }
                    newFireball.GetComponent<Animator>().SetTrigger("explode");
                    yield return new WaitForSeconds(0.4f);
                    Destroy(newFireball);

                    target.TakeDamage(6);
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
        //FindObjectOfType<AudioManager>().Play("Hit");
        animator.SetTrigger("stagger");
        if (currentHealth <= 0)
        {
            Die();
        }
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
        Lullaby,
        Pressure,
        
    }
}