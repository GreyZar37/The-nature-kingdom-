using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class meleeFolowStateAlly : AllyMeleBase
{

    public override void onEnter(AllyMeleManager machineScript)
    {
       // machineScript.currentFollowObj = machineScript.guardingSpot;
    }

    public override void onUpdate(AllyMeleManager machineScript)
    {
        machineScript.anim.SetFloat("Velocity", Mathf.Abs(machineScript.rb.velocity.x));

        if (machineScript.currentFollowObj != machineScript.guardingSpot)
        {
            if (machineScript.DistanceToTarget > machineScript.sight)
            {
                machineScript.currentFollowObj = machineScript.guardingSpot;
            }
        }


        Collider2D hit = Physics2D.OverlapCircle(machineScript.transform.position, machineScript.sight, machineScript.EnemyMask);
        if (hit != null)
        {
            if (hit.transform.tag == "Enemy")
            {
                machineScript.currentFollowObj = hit.gameObject.transform;
            }
         
        }


        if (machineScript.DistanceToTarget <= machineScript.attackDistance)
        {

            machineScript.rb.velocity = Vector2.zero;

            if (machineScript.currentFollowObj != machineScript.guardingSpot)
            {
                machineScript.SwitchState(machineScript.combatState);
            }
        }
        else
        {
            machineScript.rb.velocity = new Vector2(machineScript.direction * machineScript.speed, machineScript.rb.velocity.y);
        }
        
         
        
        machineScript.flip();
    }
    
}
