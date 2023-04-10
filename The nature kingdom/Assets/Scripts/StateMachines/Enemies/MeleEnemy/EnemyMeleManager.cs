using System.Collections;
using UnityEngine;

public class EnemyMeleManager : Enemy
{
    public meleeFolowStateEnemy followState = new meleeFolowStateEnemy();
    public meleeCombatStateEnemy combatState = new meleeCombatStateEnemy();
    public meleeIdleStateEnemy idleState = new meleeIdleStateEnemy();

    EnemyMeleBase currentState;

    public override void Start()
    {
        base.Start();

        currentState = followState;
        currentState.onEnter(this);
    }

    // Update is called once per frame
    public override void Update()
    {

        base.Update();
        currentState.onUpdate(this);
    }



    public override void takeDamage(int damage)
    {
        if (!isDashing && !block)
        {
            base.takeDamage(damage);
            if (!stuned)
            {
                StartCoroutine(stun());
            }
        }

           

      
    }

    public void SwitchState(EnemyMeleBase state)
    {
        currentState = state;
        currentState.onEnter(this);
    }

    public void flip()
    {
        if (direction > 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    IEnumerator stun()
    {
        stuned = true;


        yield return new WaitForSeconds(stunedTimer);



        stuned = false;

    }

   

}
