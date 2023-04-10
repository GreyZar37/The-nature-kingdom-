using System.Collections;
using UnityEngine;

public class meleeCombatStateAlly : AllyMeleBase
{
    bool attacking;
    int dodgeNum;
    float cooldown = 0.5f;


    float dashPower = 5;
    bool canDash;
    float dashingTime = 0.5f;
    float DashingCooldown = 0.5f;

    int lastHealth;

    Coroutine attackingRoutine;
    Coroutine blockingRoutine;

    GameObject currentEnemy;

    bool pressed;
    public override void onEnter(AllyMeleManager machineScript)
    {
        lastHealth = machineScript.Health;
    }

    public override void onUpdate(AllyMeleManager machineScript)
    {
        dodgeSystem(machineScript);
        if (machineScript.isDashing)
        {
            Debug.Log("Dahsing");
            machineScript.rb.velocity = new Vector2(-(dashPower * machineScript.direction), 0f);

        }


        machineScript.anim.SetFloat("Velocity", Mathf.Abs(machineScript.rb.velocity.x));

        if (machineScript.stuned)
        {
            resetStats(machineScript);

            if (attackingRoutine != null)
            machineScript.StopCoroutine(attackingRoutine);
            if (blockingRoutine != null)
                machineScript.StopCoroutine(blockingRoutine);

        }

        int RNG = Random.Range(0, 6);


      


        if ((machineScript.DistanceToTarget > machineScript.sight / 2  || machineScript.currentFollowObj == machineScript.guardingSpot) && attacking == false && !machineScript.block && machineScript.stuned == false)
        {
            machineScript.SwitchState(machineScript.followState);
        }
   
        else
        {  
            if (machineScript.block == false && attacking == false && RNG > 4 && machineScript.isDashing == false && machineScript.stuned == false)
            {

                machineScript.flip();
                blockingRoutine = machineScript.StartCoroutine(block(machineScript));
            } if (attacking == false && machineScript.block == false && RNG < 4 && machineScript.isDashing == false && machineScript.stuned == false)
            {

                machineScript.flip();
                attackingRoutine = machineScript.StartCoroutine(attack(machineScript));
            }
        }


    }

    IEnumerator attack(AllyMeleManager machineScript)
    {
        attacking = true;
        machineScript.anim.SetBool("Attacking", true);
        AudioManager.PlaySound(machineScript.attackSounds, 1);

        yield return new WaitForSeconds(machineScript.attakcCooldown / 2);

        Collider2D hit = Physics2D.OverlapCircle(machineScript.attackPos.position, machineScript.sight / 2, machineScript.EnemyMask);
        if (hit != null && !machineScript.block && !machineScript.stuned)
        {
         

            if (hit.transform.tag == "Enemy")
            {
                currentEnemy = hit.gameObject;
                currentEnemy.GetComponent<EnemyMeleManager>().takeDamage(machineScript.damage);

          
            }
        }

        if (currentEnemy != null)
        {
            if (currentEnemy.GetComponent<DestroyObject>() != null)
            {
                machineScript.currentFollowObj = machineScript.guardingSpot;

            }

        }
        else
        {
            machineScript.currentFollowObj = machineScript.guardingSpot;

        }

        yield return new WaitForSeconds(machineScript.attakcCooldown / 2);
        machineScript.anim.SetBool("Attacking", false);
        attacking = false;
    }


    IEnumerator block(AllyMeleManager machineScript)
    {
        yield return new WaitForSeconds(machineScript.blockCooldown);

        machineScript.block = true;
        machineScript.anim.SetBool("Blocking", true);

        yield return new WaitForSeconds(machineScript.blockTime);

        machineScript.anim.SetBool("Blocking", false);
        machineScript.block = false;

    }
    IEnumerator dogeNumAdd()
    {
        pressed = true;

        dodgeNum++;
        yield return new WaitForSeconds(cooldown);
        pressed = false;
    }

    IEnumerator dash(AllyMeleManager machineScript)
    {
        canDash = false;
        machineScript.isDashing = true;

        yield return new WaitForSeconds(dashingTime);
        machineScript.isDashing = false;
        machineScript.rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(DashingCooldown);
        canDash = true;
    }

    void resetStats(AllyMeleManager machineScript)
    {
        attacking = false;
        machineScript.block = false;
        machineScript.anim.SetBool("Blocking", false);
        machineScript.anim.SetBool("Attacking", false);
        pressed = false;

    }

    public void dodgeSystem(AllyMeleManager machineScript)
    {
        if (machineScript.isDashing)
        {
            return;
        }


        if (canDash)
        {
            Debug.Log("ice");
            machineScript.StartCoroutine(dash(machineScript));

            if (attackingRoutine != null)
                machineScript.StopCoroutine(attackingRoutine);
            if (blockingRoutine != null)
                machineScript.StopCoroutine(blockingRoutine);
            resetStats(machineScript);

        }


    }

    
}

