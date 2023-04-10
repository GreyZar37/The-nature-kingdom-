using System.Collections;
using UnityEngine;

[System.Flags]
enum test
{
    layerMobs = 1, LayerPlayer = 2
}
public class meleeCombatStateEnemy : EnemyMeleBase
{
    bool attacking;
    int dodgeNum;


    float dashPower = 5;
    bool canDash = true;
    float dashingTime = .5f;
    float DashingCooldown;

    Coroutine attackingRoutine;
    Coroutine blockingRoutine;
    GameObject currentEnemy;


    test thisTest = test.layerMobs | test.LayerPlayer;

    bool pressed;
    public override void onEnter(EnemyMeleManager machineScript)
    {
        DashingCooldown = Random.Range(2, 10);
        
    }

    public override void onUpdate(EnemyMeleManager machineScript)
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



        if (machineScript.DistanceToTarget > machineScript.sight / 2 && attacking == false && !machineScript.block && machineScript.stuned == false && !machineScript.isDashing)
        {
            machineScript.SwitchState(machineScript.followState);
        }
        else
        {

            if (machineScript.block == false && attacking == false && RNG > 4 && machineScript.isDashing == false && machineScript.stuned == false)
            {

                machineScript.flip();
                blockingRoutine = machineScript.StartCoroutine(block(machineScript));
            }
            if (attacking == false && machineScript.block == false && RNG < 4 && machineScript.isDashing == false && machineScript.stuned == false)
            {

                machineScript.flip();
                attackingRoutine = machineScript.StartCoroutine(attack(machineScript));



            }
        }


    }

    IEnumerator attack(EnemyMeleManager machineScript)
    {
        attacking = true;
        machineScript.anim.SetBool("Attacking", true);
        AudioManager.PlaySound(machineScript.attackSounds, 1);
        yield return new WaitForSeconds(machineScript.attakcCooldown / 2);

        Collider2D hit = Physics2D.OverlapCircle(machineScript.attackPos.position, machineScript.sight / 2, machineScript.mobs);
        if (hit != null && !machineScript.block && !machineScript.stuned)
        {
            if (hit.transform.tag == "Player")
            {
                currentEnemy = hit.gameObject;
                hit.GetComponent<PlayerCombat>().TakeDamage(machineScript.damage);
            }
            else if (hit.transform.tag == "Ally")
            {
                currentEnemy = hit.gameObject;
                currentEnemy.GetComponent<AllyMeleManager>().takeDamage(machineScript.damage);
            }


        }
        if (currentEnemy != null)
        {
            if (currentEnemy.GetComponent<DestroyObject>() != null)
            {
                machineScript.currentFollowObj = machineScript.PlayerBase;
            }

        }
        else
        {
            machineScript.currentFollowObj = machineScript.PlayerBase;
        }

        yield return new WaitForSeconds(machineScript.attakcCooldown / 2);
        machineScript.anim.SetBool("Attacking", false);
        attacking = false;
    }


    IEnumerator block(EnemyMeleManager machineScript)
    {
        yield return new WaitForSeconds(machineScript.blockCooldown);

        machineScript.block = true;
        machineScript.anim.SetBool("Blocking", true);

        yield return new WaitForSeconds(machineScript.blockTime);

        machineScript.anim.SetBool("Blocking", false);
        machineScript.block = false;

    }
  

    IEnumerator dash(EnemyMeleManager machineScript)
    {
        canDash = false;
        machineScript.isDashing = true;

        yield return new WaitForSeconds(dashingTime);
        machineScript.isDashing = false;
        machineScript.rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(DashingCooldown);
        DashingCooldown = Random.Range(2, 10);
        canDash = true;
    }

    void resetStats(EnemyMeleManager machineScript)
    {
        attacking = false;
        machineScript.block = false;
        machineScript.anim.SetBool("Blocking", false);
        machineScript.anim.SetBool("Attacking", false);

    }

    public void dodgeSystem(EnemyMeleManager machineScript)
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
