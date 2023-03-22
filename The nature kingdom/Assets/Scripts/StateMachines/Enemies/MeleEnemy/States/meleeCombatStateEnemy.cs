using System.Collections;
using UnityEngine;

public class meleeCombatStateEnemy : EnemyMeleBase
{
    bool attacking;
    int dodgeNum;
    float cooldown = 1f;


    float dashPower = 20;
    bool canDash;
    bool isDashing;
    float dashingTime = 0.1f;
    float DashingCooldown = 0.5f;

    bool pressed;

    public override void onEnter(EnemyMeleManager machineScript)
    {
    }

    public override void onUpdate(EnemyMeleManager machineScript)
    {
        machineScript.anim.SetFloat("Velocity", Mathf.Abs(machineScript.rb.velocity.x));



        if (isDashing)
        {
            return;
        }
        int RNG = Random.Range(0, 6);

        if (Input.GetMouseButtonDown(0) && !pressed && isDashing == false)
        {
            machineScript.StartCoroutine(dogeNumAdd());
        }


        if (isDashing == false)
        {
            if(dodgeNum == Random.Range(2, 4))
            {
                machineScript.StopAllCoroutines();
                resetStats(machineScript);
                Debug.Log("dwad");
                machineScript.StartCoroutine(dash(machineScript));
                dodgeNum = 0;
            }

        }





        if (machineScript.DistanceToTarget > machineScript.sight / 2 && attacking == false && !machineScript.block)
        {
            machineScript.SwitchState(machineScript.followState);
        }
        else
        {

            if (attacking == false && machineScript.block == false && RNG < 4 && isDashing == false)
            {
                machineScript.flip();
                machineScript.StartCoroutine(attack(machineScript));
            }
            if (machineScript.block == false && attacking == false && RNG > 4 && isDashing == false)
            {
                machineScript.flip();

                machineScript.StartCoroutine(block(machineScript));
            }
        }


    }

    IEnumerator attack(EnemyMeleManager machineScript)
    {
        attacking = true;
        machineScript.anim.SetBool("Attacking", true);
        yield return new WaitForSeconds(machineScript.attakcCooldown / 2);

        Collider2D hit = Physics2D.OverlapCircle(machineScript.transform.position, machineScript.sight / 2, machineScript.mobs);
        if (hit != null)
        {
            if (hit.transform.tag == "Player")
            {
            }
            else if (hit.transform.tag == "Ally")
            {
            }


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
    IEnumerator dogeNumAdd()
    {
        pressed = true;
        dodgeNum++;
        yield return new WaitForSeconds(cooldown);
        pressed = false;
    }

    IEnumerator dash(EnemyMeleManager machineScript)
    {
        canDash = false;
        isDashing = true;

        machineScript.rb.velocity = new Vector2(-(dashPower * machineScript.direction), 0f);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        machineScript.rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(DashingCooldown);
        canDash = true;
    }

    void resetStats(EnemyMeleManager machineScript)
    {
        attacking = false;
        machineScript.block = false;
        machineScript.anim.SetBool("Blocking", false);
        machineScript.anim.SetBool("Attacking", false);
        pressed = false;

    }
}
