using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class meleeFolowStateEnemy : EnemyMeleBase
{

    public override void onEnter(EnemyMeleManager machineScript)
    {
        machineScript.currentFollowObj = machineScript.PlayerBase ;

    }

    public override void onUpdate(EnemyMeleManager machineScript)
    {
        machineScript.anim.SetFloat("Velocity", Mathf.Abs(machineScript.rb.velocity.x));

        if (machineScript.currentFollowObj != machineScript.PlayerBase)
        {
            if (machineScript.DistanceToTarget > machineScript.sight - 0.1)
            {
                machineScript.currentFollowObj = machineScript.PlayerBase;
            }
        }

        Collider2D hit = Physics2D.OverlapCircle(machineScript.transform.position, machineScript.sight, machineScript.mobs);
        if (hit != null)
        {
            if (hit.transform.tag == "Player")
            {
                machineScript.currentFollowObj = hit.gameObject;
            }
            else if (hit.transform.tag == "Ally")
            {
                machineScript.currentFollowObj = hit.gameObject;
            }


        }
         

        if (machineScript.DistanceToTarget <= machineScript.attackDistance)
        {
            machineScript.rb.velocity = Vector2.zero;
            machineScript.SwitchState(machineScript.combatState);
        }
        else
        {
            machineScript.rb.velocity = new Vector2(machineScript.direction * machineScript.speed, machineScript.rb.velocity.y);
        }

        machineScript.flip();
    }
    
}
